using System.Runtime.InteropServices;

namespace BlazorWebKit;

internal static class Gtk
{
    public const string FilePath = "gtk";

    [DllImport(FilePath)]
    public static extern ulong g_signal_connect_data (IntPtr instance, string detailed_signal, IntPtr c_handler, IntPtr data, IntPtr destroy_data, GLib.ConnectFlags connect_flags);

    public static ulong g_signal_connect(IntPtr instance, string detailed_signal, IntPtr c_handler, IntPtr data)
    {
        return g_signal_connect_data(instance, detailed_signal, c_handler, data, IntPtr.Zero,  (GLib.ConnectFlags)0);
    }

    [DllImport(FilePath)]
    public static extern IntPtr g_memory_input_stream_new_from_data (byte[] data, uint len, IntPtr destroy);

    [DllImport(FilePath)]
    public static extern void g_free (IntPtr o);
}