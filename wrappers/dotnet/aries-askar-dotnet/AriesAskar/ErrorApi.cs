using System.Threading.Tasks;

namespace aries_askar_dotnet.AriesAskar
{
    public static class ErrorApi
    {
        /// <summary>
        /// Get the current error with <see cref="ErrorCode"/> and message, which was thrown from the backend.
        /// </summary>
        /// <returns>The current error json with <see cref="ErrorCode"/> and error message in the format {"code":[int],"message":[string]}.</returns>
        public static Task<string> GetCurrentErrorAsync()
        {
            string result = "";
            NativeMethods.askar_get_current_error(ref result);
            return Task.FromResult(result);
        }
    }
}