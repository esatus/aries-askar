using System.Threading.Tasks;

namespace aries_askar_dotnet.aries_askar
{
    public static class Error
    {
        public static Task<string> GetCurrentErrorAsync()
        {
            string result = "";
            NativeMethods.askar_get_current_error(ref result);
            return Task.FromResult(result);
        }
    }
}