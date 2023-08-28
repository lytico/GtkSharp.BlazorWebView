using GLib;
using WebKit;

namespace BlazorWebKit;

// this file is used to prototype upstream (=GtkSharp) methods
internal static partial class WebKit {
    
}


static class UserScript {
        
    public static global::WebKit.UserScript New (string source, UserContentInjectedFrames injected_frames, UserScriptInjectionTime injection_time,
        string[]? allow_list, string[]? block_list) {
        // https://webkitgtk.org/reference/webkit2gtk/2.9.4/webkit2gtk-4.0-WebKitUserContent.html#webkit-user-script-new

        IntPtr[] native_allow_list = allow_list == null ? Array.Empty<IntPtr> () : global::GLib.Marshaller.StringArrayToNullTermPointer (allow_list);
        IntPtr[] native_block_list = block_list == null ? Array.Empty<IntPtr> () : global::GLib.Marshaller.StringArrayToNullTermPointer (block_list);
            
        var raw = WebKit.webkit_user_script_new (source, injected_frames, injection_time, native_allow_list, native_block_list);
            
        Marshaller.Free (native_allow_list);
        Marshaller.Free (native_block_list);

        return global::WebKit.UserScript.New (raw);
    }
}