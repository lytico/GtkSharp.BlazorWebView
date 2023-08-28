using System.Runtime.InteropServices;
using WebKit;

namespace BlazorWebKit;

internal static partial class WebKit {

    // https://webkitgtk.org/reference/webkit2gtk/2.9.4/webkit2gtk-4.0-WebKitUserContent.html#webkit-user-script-new

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr d_webkit_user_script_new(string source, UserContentInjectedFrames injected_frames, UserScriptInjectionTime injection_time, IntPtr[] allow_list, IntPtr[] block_list);

    internal static d_webkit_user_script_new webkit_user_script_new = FuncLoader.LoadFunction<d_webkit_user_script_new>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_user_script_new"));

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_webkit_user_content_manager_add_script(IntPtr raw, IntPtr script);

    internal static d_webkit_user_content_manager_add_script webkit_user_content_manager_add_script = FuncLoader.LoadFunction<d_webkit_user_content_manager_add_script>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_user_content_manager_add_script"));

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_webkit_user_script_unref(IntPtr raw);

    internal static d_webkit_user_script_unref webkit_user_script_unref = FuncLoader.LoadFunction<d_webkit_user_script_unref>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_user_script_unref"));

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool d_webkit_user_content_manager_register_script_message_handler(IntPtr raw, string name);

    internal static d_webkit_user_content_manager_register_script_message_handler webkit_user_content_manager_register_script_message_handler = FuncLoader.LoadFunction<d_webkit_user_content_manager_register_script_message_handler>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_user_content_manager_register_script_message_handler"));

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_webkit_javascript_result_unref(IntPtr raw);

    internal static d_webkit_javascript_result_unref webkit_javascript_result_unref = FuncLoader.LoadFunction<d_webkit_javascript_result_unref>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_javascript_result_unref"));

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr d_webkit_javascript_result_get_js_value(IntPtr raw);

    internal static d_webkit_javascript_result_get_js_value webkit_javascript_result_get_js_value = FuncLoader.LoadFunction<d_webkit_javascript_result_get_js_value>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_javascript_result_get_js_value"));

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool d_jsc_value_is_string(IntPtr raw);

    internal static d_jsc_value_is_string jsc_value_is_string = FuncLoader.LoadFunction<d_jsc_value_is_string>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "jsc_value_is_string"));

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr d_jsc_value_to_string(IntPtr raw);

    internal static d_jsc_value_to_string jsc_value_to_string = FuncLoader.LoadFunction<d_jsc_value_to_string>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "jsc_value_to_string"));

}