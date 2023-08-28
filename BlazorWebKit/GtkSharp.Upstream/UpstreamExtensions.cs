using System.Runtime.InteropServices;
using WebKit;

namespace WebKitUpStream;

public static class UpstreamExtensions {

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void d_webkit_user_content_manager_add_script(IntPtr raw, IntPtr script);

    internal static d_webkit_user_content_manager_add_script webkit_user_content_manager_add_script = FuncLoader.LoadFunction<d_webkit_user_content_manager_add_script>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_user_content_manager_add_script"));

    public static void AddScript(this UserContentManager it, UserScript script) {

        webkit_user_content_manager_add_script(it.Handle, script == null ? IntPtr.Zero : script.Handle);

    }

}