using System.Runtime.InteropServices;

namespace BlazorWebKit;

internal static class GLib {

    // missing in GtkSharp; also not found in *-api.xml's
    // https://docs.gtk.org/gobject/func.signal_connect_data.html
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate ulong d_g_signal_connect_data(IntPtr instance, string detailed_signal, IntPtr c_handler, IntPtr data, IntPtr destroy_data, global::GLib.ConnectFlags connect_flags);

    internal static d_g_signal_connect_data g_signal_connect_data = FuncLoader.LoadFunction<d_g_signal_connect_data>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Gio), "g_signal_connect_data"));

    // missing in GtkSharp: found in GioSharp-api.xml but not in Code
    // class MemoryInputStream
    // https://docs.gtk.org/gio/ctor.MemoryInputStream.new_from_data.html

    // use MemoryInputStream after https://github.com/GtkSharp/GtkSharp/pull/412 is merged & published
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr d_g_memory_input_stream_new_from_data(byte[] data, uint len, IntPtr destroy);

    internal static d_g_memory_input_stream_new_from_data g_memory_input_stream_new_from_data = FuncLoader.LoadFunction<d_g_memory_input_stream_new_from_data>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Gio), "g_memory_input_stream_new_from_data"));

}