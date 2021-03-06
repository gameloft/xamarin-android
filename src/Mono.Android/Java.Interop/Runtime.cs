using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Android.Runtime;

namespace Java.Interop {

	public static class Runtime {

		public static List<WeakReference> GetSurfacedObjects ()
		{
			return Java.Lang.Object.GetSurfacedObjects_ForDiagnosticsOnly ();
		}

		[DllImport ("__Internal", CallingConvention = CallingConvention.Cdecl)]
		static extern int _monodroid_max_gref_get ();

		public static int MaxGlobalReferenceCount {
			get {return _monodroid_max_gref_get ();}
		}

		[DllImport ("__Internal", CallingConvention = CallingConvention.Cdecl)]
		static extern int _monodroid_gref_get ();

		public static int GlobalReferenceCount {
			get {return _monodroid_gref_get ();}
		}

		public static int LocalReferenceCount {
#if JAVA_INTEROP
			get {return JniEnvironment.LocalReferenceCount;}
#else   // !JAVA_INTEROP
			get {return JNIEnv.lref_count;}
#endif  // !JAVA_INTEROP
		}

		public static bool IsGCUserPeer (IJavaObject value)
		{
			if (value == null)
				return false;
			return IsGCUserPeer (value.Handle);
		}

		public static bool IsGCUserPeer (IntPtr value)
		{
			return JNIEnv.IsGCUserPeer (value);
		}
	}
}
