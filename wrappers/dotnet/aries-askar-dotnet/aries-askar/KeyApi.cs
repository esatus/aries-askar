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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        public static async Task<uint> CreateKeyFromSeedAsync(
            KeyAlg keyAlg,
            string seed,
            SeedMethod method)
        {
            uint localKeyHandle = 0;
            int errorCode = NativeMethods.askar_key_from_seed(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ByteBuffer.Create(seed),
                FfiStr.Create(method.ToSeedMethodString()),
                ref localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        public static async Task<uint> CreateKeyFromJwkAsync(
            string jwkJson)
        {
            uint localKeyHandle = 0;
            int errorCode = NativeMethods.askar_key_from_jwk(
                ByteBuffer.Create(jwkJson),
                ref localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        public static async Task<uint> CreateKeyFromPublicBytesAsync(
            KeyAlg keyAlg,
            string publicBytes)
        {
            uint localKeyHandle = 0;
            int errorCode = NativeMethods.askar_key_from_public_bytes(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ByteBuffer.Create(publicBytes),
                ref localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        public static async Task<SecretBuffer> GetPublicBytesFromKeyAsync(
            uint localKeyHandle)
        {
            SecretBuffer secret = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_get_public_bytes(
                localKeyHandle,
                ref secret);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return secret;
        }

        public static async Task<string> GetJwkPublicFromKeyAsync(
            uint localKeyHandle,
            KeyAlg keyAlg)
        {
            string jwk = "";
            int errorCode = NativeMethods.askar_key_get_jwk_public(
                localKeyHandle,
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ref jwk);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return jwk;
        }
    }
}
