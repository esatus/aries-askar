using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.aries_askar
{
    public class SecretApi
    {
        public static async Task<bool> BufferFree(SecretBuffer buffer)
        {
            int errorCode = NativeMethods.askar_buffer_free(buffer);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            else
                return true;
        }
    }
}
