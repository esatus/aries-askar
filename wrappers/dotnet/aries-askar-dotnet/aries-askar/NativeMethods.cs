using System.Runtime.InteropServices;

namespace aries_askar_dotnet.aries_askar
{
	internal static class NativeMethods
    {
		#region Error
		[DllImport(Consts.ARIES_ASKAR_LIB_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		internal static extern string askar_get_current_error(ref string error_json_p);
		#endregion
	}
}