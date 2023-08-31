using System.Web;
using GLib;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using WebKit;

namespace BlazorWebKit;

public class GtkSharpWebViewManager : Microsoft.AspNetCore.Components.WebView.WebViewManager {

    const string _scheme = "app";
    readonly static Uri _baseUri = new Uri($"{_scheme}://localhost/");

    public GtkSharpWebViewManager(WebView webView, IServiceProvider serviceProvider)
        : base(serviceProvider, Dispatcher.CreateDefault(), _baseUri
            , new PhysicalFileProvider(serviceProvider.GetRequiredService<BlazorWebViewOptions>().ContentRoot)
            , new()
            , serviceProvider.GetRequiredService<BlazorWebViewOptions>().RelativeHostPath) {
        var options = serviceProvider.GetRequiredService<BlazorWebViewOptions>();
        _relativeHostPath = options.RelativeHostPath;
        _rootComponent = options.RootComponent;
        _logger = serviceProvider.GetService<ILogger<BlazorWebView>>();

        WebView = webView;

        // This is necessary to automatically serve the files in the `_framework` virtual folder.
        // Using `file://` will cause the webview to look for the `_framework` files on the file system,
        // and it won't find them.
        WebView.Context.RegisterUriScheme(_scheme, HandleUriScheme);

        Dispatcher.InvokeAsync(async () => {
            await AddRootComponentAsync(_rootComponent, "#app", ParameterView.Empty);
        });

        var script = new UserScript(
            """
            window.__receiveMessageCallbacks = [];

            window.__dispatchMessageCallback = function(message) {
                window.__receiveMessageCallbacks.forEach(function(callback) { callback(message); });
            };

            window.external = {
                sendMessage: function(message) {
                    window.webkit.messageHandlers.webview.postMessage(message);
                },
                receiveMessage: function(callback) {
                    window.__receiveMessageCallbacks.push(callback);
                }
            };
            """,
            UserContentInjectedFrames.AllFrames,
            UserScriptInjectionTime.Start,
            null, null);

        WebView.UserContentManager.AddScript(script);

        WebView.UserContentManager.RegisterScriptMessageHandler("webview");

        WebView.UserContentManager.ScriptMessageReceived += (o, args) => {
            var jsValue = args.JsResult.JsValue;

            if (jsValue.IsString) {

                var s = jsValue.ToString();

                if (s is not null) {
                    _logger?.LogDebug($"Received message `{s}`");

                    try {
                        MessageReceived(_baseUri, s);
                    } finally { }
                }
            }
        };

        Navigate("/");
    }

    public WebView WebView { get; init; }

    readonly string _relativeHostPath;
    readonly Type _rootComponent;
    readonly ILogger<BlazorWebView>? _logger;

    void HandleUriScheme(URISchemeRequest request) {
        if (request.Scheme != _scheme) {
            throw new Exception($"Invalid scheme \"{request.Scheme}\"");
        }

        var uri = request.Uri;

        if (request.Path == "/") {
            uri += _relativeHostPath;
        }

        _logger?.LogInformation($"Fetching \"{uri}\"");

        if (TryGetResponseContent(uri, false, out int statusCode, out string statusMessage, out Stream content, out IDictionary<string, string> headers)) {

            using var memoryStream = new MemoryStream();
            var length = (int)content.Length;
            content.CopyTo(memoryStream, length);
            var buffer = memoryStream.GetBuffer();
            Array.Resize(ref buffer, length);
            using var inputStream = InputStream.NewFromData(buffer);

            request.Finish(inputStream, length, headers["Content-Type"]);

        } else {
            throw new Exception($"Failed to serve \"{uri}\". {statusCode} - {statusMessage}");
        }
    }

    protected override void NavigateCore(Uri absoluteUri) {
        _logger?.LogInformation($"Navigating to \"{absoluteUri}\"");

        WebView.LoadUri(absoluteUri.ToString());
    }

    protected override void SendMessage(string message) {
        _logger?.LogDebug($"Dispatching `{message}`");

        var script = $"__dispatchMessageCallback(\"{HttpUtility.JavaScriptStringEncode(message)}\")";

        WebView.RunJavascript(script);
    }

}