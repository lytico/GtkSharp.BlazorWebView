// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

using JavaScriptCore;

namespace WebKit {

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class JavascriptResult : GLib.Opaque {

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr d_webkit_javascript_result_get_type();
		static d_webkit_javascript_result_get_type webkit_javascript_result_get_type = FuncLoader.LoadFunction<d_webkit_javascript_result_get_type>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_javascript_result_get_type"));

		public static GLib.GType GType { 
			get {
				IntPtr raw_ret = webkit_javascript_result_get_type();
				GLib.GType ret = new GLib.GType(raw_ret);
				return ret;
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr d_webkit_javascript_result_get_js_value(IntPtr raw);
		static d_webkit_javascript_result_get_js_value webkit_javascript_result_get_js_value = FuncLoader.LoadFunction<d_webkit_javascript_result_get_js_value>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_javascript_result_get_js_value"));

		public JavaScriptCore.Value JsValue { 
			get {
				IntPtr raw_ret = webkit_javascript_result_get_js_value(Handle);
				JavaScriptCore.Value ret = new Value(raw_ret);
				return ret;
			}
		}

		public JavascriptResult(IntPtr raw) : base(raw) {}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate IntPtr d_webkit_javascript_result_ref(IntPtr raw);
		static d_webkit_javascript_result_ref webkit_javascript_result_ref = FuncLoader.LoadFunction<d_webkit_javascript_result_ref>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_javascript_result_ref"));

		protected override void Ref (IntPtr raw)
		{
			if (!Owned) {
				webkit_javascript_result_ref (raw);
				Owned = true;
			}
		}

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		delegate void d_webkit_javascript_result_unref(IntPtr raw);
		static d_webkit_javascript_result_unref webkit_javascript_result_unref = FuncLoader.LoadFunction<d_webkit_javascript_result_unref>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Webkit), "webkit_javascript_result_unref"));

		protected override void Unref (IntPtr raw)
		{
			if (Owned) {
				webkit_javascript_result_unref (raw);
				Owned = false;
			}
		}

		class FinalizerInfo {
			IntPtr handle;
			public uint timeoutHandlerId;

			public FinalizerInfo (IntPtr handle)
			{
				this.handle = handle;
			}

			public bool Handler ()
			{
				webkit_javascript_result_unref (handle);
				GLib.Timeout.Remove(timeoutHandlerId);
				return false;
			}
		}

		~JavascriptResult ()
		{
			if (!Owned)
				return;
			FinalizerInfo info = new FinalizerInfo (Handle);
			info.timeoutHandlerId = GLib.Timeout.Add (50, new GLib.TimeoutHandler (info.Handler));
		}


		// Internal representation of the wrapped structure ABI.
		static GLib.AbiStruct _abi_info = null;
		static public GLib.AbiStruct abi_info {
			get {
				if (_abi_info == null)
					_abi_info = new GLib.AbiStruct (new List<GLib.AbiField>{ 
					});

				return _abi_info;
			}
		}


		// End of the ABI representation.

#endregion
	}
}