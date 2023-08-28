using WebKit;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using System.Web;
using GLib;
using WebKitUpStream;
using Settings = WebKit.Settings;

namespace BlazorWebKit;

using static GLib;
using static WebKit;

public class BlazorWebView : WebView
{
    class WebViewManager : Microsoft.AspNetCore.Components.WebView.WebViewManager
    {

        delegate void WebMessageHandler(IntPtr contentManager, IntPtr jsResult, IntPtr arg);

        const string _scheme = "app";
        readonly static Uri _baseUri = new Uri($"{_scheme}://localhost/");

        public WebViewManager(WebView webView, IServiceProvider serviceProvider)
            : base(serviceProvider, Dispatcher.CreateDefault(), _baseUri
            , new PhysicalFileProvider(serviceProvider.GetRequiredService<BlazorWebViewOptions>().ContentRoot)
            , new()
            , serviceProvider.GetRequiredService<BlazorWebViewOptions>().RelativeHostPath)
        {
            var options = serviceProvider.GetRequiredService<BlazorWebViewOptions>();
            _relativeHostPath = options.RelativeHostPath;
            _rootComponent = options.RootComponent;
            _logger = serviceProvider.GetService<ILogger<BlazorWebView>>();

            WebView = webView;
            HandleWebMessageDelegate = HandleWebMessage;

            // This is necessary to automatically serve the files in the `_framework` virtual folder.
            // Using `file://` will cause the webview to look for the `_framework` files on the file system,
            // and it won't find them.
            WebView.Context.RegisterUriScheme(_scheme, HandleUriScheme);

            Dispatcher.InvokeAsync(async () =>
            {
                await AddRootComponentAsync(_rootComponent, "#app", ParameterView.Empty);
            });

            var script = new global::WebKit.UserScript(
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
 
            g_signal_connect_data(WebView.UserContentManager.Handle, "script-message-received::webview",
                Marshal.GetFunctionPointerForDelegate(HandleWebMessageDelegate), 
                IntPtr.Zero,IntPtr.Zero, (global::GLib.ConnectFlags)0);

            WebView.UserContentManager.RegisterScriptMessageHandler("webview");

            Navigate("/");
        }

        public WebView WebView { get; init; }
        readonly WebMessageHandler HandleWebMessageDelegate;
        readonly string _relativeHostPath;
        readonly Type _rootComponent;
        readonly ILogger<BlazorWebView>? _logger;

        void HandleUriScheme(URISchemeRequest request)
        {
            if (request.Scheme != _scheme)
            {
                throw new Exception($"Invalid scheme \"{request.Scheme}\"");
            }

            var uri = request.Uri;
            if (request.Path == "/")
            {
                uri += _relativeHostPath;
            }

            _logger?.LogInformation($"Fetching \"{uri}\"");

            if (TryGetResponseContent(uri, false, out int statusCode, out string statusMessage, out Stream content, out IDictionary<string, string> headers))
            {
                using(var ms = new MemoryStream())
                {
                    content.CopyTo(ms);

                    // TODO: use MemoryInputStream after https://github.com/GtkSharp/GtkSharp/pull/412 is merged & published
                    var streamPtr = g_memory_input_stream_new_from_data(ms.GetBuffer(), (uint)ms.Length, IntPtr.Zero);
                    var inputStream = new global::GLib.InputStream(streamPtr);
                    request.Finish(inputStream, ms.Length, headers["Content-Type"]);
                }
            }
            else
            {
                throw new Exception($"Failed to serve \"{uri}\". {statusCode} - {statusMessage}");
            }
        }

        void HandleWebMessage(IntPtr contentManager, IntPtr jsResultHandle, IntPtr arg) {
            
            var jsResult = new JavascriptResult(jsResultHandle);
        
            var jsValue = jsResult.JsValue;
            if (jsValue.IsString) 
            {
                
                var s = jsValue.ToString();
                if (s is not null)
                {
                    _logger?.LogDebug($"Received message `{s}`");

                    try
                    {
                        MessageReceived(_baseUri, s);
                    }
                    finally
                    {
                    }
                }
            }

        }

        protected override void NavigateCore(Uri absoluteUri)
        {
            _logger?.LogInformation($"Navigating to \"{absoluteUri}\"");

            WebView.LoadUri(absoluteUri.ToString());
        }

        protected override void SendMessage(string message)
        {
            _logger?.LogDebug($"Dispatching `{message}`");

            var script = $"__dispatchMessageCallback(\"{HttpUtility.JavaScriptStringEncode(message)}\")";

           WebView.RunJavascript(script);
        }
    }

    public BlazorWebView(IServiceProvider serviceProvider)
        : base ()
    {
        _manager = new WebViewManager(this, serviceProvider);
    }

    public BlazorWebView(IntPtr raw, IServiceProvider serviceProvider)
        : base (raw)
    {
        _manager = new WebViewManager(this, serviceProvider);
    }

    public BlazorWebView(WebContext context, IServiceProvider serviceProvider)
        : base (context)
    {
        _manager = new WebViewManager(this, serviceProvider);
    }

    public BlazorWebView(WebView web_view, IServiceProvider serviceProvider)
        : base (web_view)
    {
        _manager = new WebViewManager(this, serviceProvider);
    }

    public BlazorWebView(Settings settings, IServiceProvider serviceProvider)
        : base (settings)
    {
        _manager = new WebViewManager(this, serviceProvider);
    }

    public BlazorWebView(UserContentManager user_content_manager, IServiceProvider serviceProvider)
        : base (user_content_manager)
    {
        _manager = new WebViewManager(this, serviceProvider);
    }

    readonly WebViewManager _manager;
}