using System.Runtime.InteropServices;

namespace BlazorWebKit;

internal static class WebKit
{
    public enum WebKitUserContentInjectedFrames
    {
        WEBKIT_USER_CONTENT_INJECT_ALL_FRAMES = 0,
        WEBKIT_USER_CONTENT_INJECT_TOP_FRAME = 1
    }

    public enum WebKitUserScriptInjectionTime
    {
        WEBKIT_USER_SCRIPT_INJECT_AT_DOCUMENT_START = 0,
        WEBKIT_USER_SCRIPT_INJECT_AT_DOCUMENT_END = 1
    }

    public const string FilePath = "webkit";

    [DllImport(FilePath)]
    public static extern IntPtr webkit_user_script_new(string source, WebKitUserContentInjectedFrames injected_frames,
        WebKitUserScriptInjectionTime injection_time, string? allow_list, string? block_list);

    [DllImport(FilePath)]
    public static extern void webkit_user_content_manager_add_script(IntPtr manager, IntPtr script);

    [DllImport(FilePath)]
    public static extern void webkit_user_script_unref(IntPtr script);

    [DllImport(FilePath)]
    public static extern bool webkit_user_content_manager_register_script_message_handler(IntPtr manager, string name);

    [DllImport(FilePath)]
    public static extern void webkit_web_view_run_javascript (IntPtr web_view, string script, IntPtr cancellable, IntPtr callback, IntPtr user_data);

    [DllImport(FilePath)]
    public static extern void webkit_javascript_result_unref(IntPtr js_result);

    [DllImport(FilePath)]
    public static extern IntPtr webkit_javascript_result_get_js_value(IntPtr js_result);

    [DllImport(FilePath)]
    public static extern bool jsc_value_is_string(IntPtr value);

    [DllImport(FilePath)]
    public static extern IntPtr jsc_value_to_string(IntPtr value);
}