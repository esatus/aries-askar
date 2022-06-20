using aries_askar_dotnet.Models;
using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.aries_askar
{
    public class KeyApi
    {
        public static async Task<uint> GenerateKeyAsync(
            KeyAlg keyAlg,
            byte ephemeral)
        {
            uint localKeyHandle = 0;
            int errorCode = NativeMethods.askar_key_generate(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ephemeral,
                ref localKeyHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return localKeyHandle;
        }
    }
}
