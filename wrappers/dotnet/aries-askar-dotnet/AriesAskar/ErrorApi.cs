using System.Threading.Tasks;

namespace aries_askar_dotnet.AriesAskar
{
    public static class ErrorApi
    {
        public static Task<string> GetCurrentErrorAsync()
        {
            string result = "";
            NativeMethods.askar_get_current_error(ref result);
            return Task.FromResult(result);
        }
    }
}