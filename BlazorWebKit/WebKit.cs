using System.Runtime.InteropServices;
using WebKit;

namespace BlazorWebKit;

internal static partial class WebKit
{

    public const string FilePath = "webkit";

    // https://webkitgtk.org/reference/webkit2gtk/2.9.4/webkit2gtk-4.0-WebKitUserContent.html#webkit-user-script-new
    
    [DllImport(FilePath)]
    public static extern IntPtr webkit_user_script_new(string source, UserContentInjectedFrames injected_frames,
        UserScriptInjectionTime injection_time, IntPtr[] allow_list, IntPtr[] block_list);

    [DllImport(FilePath)]
    public static extern void webkit_user_content_manager_add_script(IntPtr manager, IntPtr script);

    [DllImport(FilePath)]
    public static extern void webkit_user_script_unref(IntPtr script);

    [DllImport(FilePath)]
    public static extern bool webkit_user_content_manager_register_script_message_handler(IntPtr manager, string name);

    [DllImport(FilePath)]
    public static extern void webkit_javascript_result_unref(IntPtr js_result);

    [DllImport(FilePath)]
    public static extern IntPtr webkit_javascript_result_get_js_value(IntPtr js_result);

    [DllImport(FilePath)]
    public static extern bool jsc_value_is_string(IntPtr value);

    [DllImport(FilePath)]
    public static extern IntPtr jsc_value_to_string(IntPtr value);
}