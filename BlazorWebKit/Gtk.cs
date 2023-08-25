using System.Runtime.InteropServices;

namespace BlazorWebKit;

internal static class Gtk {
    public const string FilePath = "gtk";

    [DllImport (FilePath)]
    // missing in GtkSharp; also not found in *-api.xml's
    // https://docs.gtk.org/gobject/func.signal_connect_data.html
    public static extern ulong g_signal_connect_data (IntPtr instance, string detailed_signal, IntPtr c_handler, IntPtr data, IntPtr destroy_data, GLib.ConnectFlags connect_flags);

    public static ulong g_signal_connect (IntPtr instance, string detailed_signal, IntPtr c_handler, IntPtr data) {
        return g_signal_connect_data (instance, detailed_signal, c_handler, data, IntPtr.Zero, (GLib.ConnectFlags)0);
    }

    [DllImport (FilePath)]
    // missing in GtkSharp: found in GioSharp-api.xml but not in Code
    // class MemoryInputStream
    // https://docs.gtk.org/gio/ctor.MemoryInputStream.new_from_data.html
    public static extern IntPtr g_memory_input_stream_new_from_data (byte[] data, uint len, IntPtr destroy);
}