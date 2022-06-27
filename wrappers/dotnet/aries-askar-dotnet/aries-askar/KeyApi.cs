using aries_askar_dotnet.Models;
using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.aries_askar
{
    public class KeyApi
    {
        #region Create
        public static async Task<IntPtr> CreateKeyAsync(
            KeyAlg keyAlg,
            byte ephemeral)
        {
            IntPtr localKeyHandle = new();
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

        public static async Task<IntPtr> CreateKeyFromSeedAsync(
            KeyAlg keyAlg,
            string seed,
            SeedMethod method)
        {
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_from_seed(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ByteBuffer.Create(seed),
                FfiStr.Create(method.ToSeedMethodString()),
                ref localKeyHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return localKeyHandle;
        }

        public static async Task<IntPtr> CreateKeyFromJwkAsync(
            string jwkJson)
        {
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_from_jwk(
                ByteBuffer.Create(jwkJson),
                ref localKeyHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return localKeyHandle;
        }

        public static async Task<IntPtr> CreateKeyFromPublicBytesAsync(
            KeyAlg keyAlg,
            SecretBuffer publicBytes)
        {
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_from_public_bytes(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ByteBuffer.Create(publicBytes),
                ref localKeyHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return localKeyHandle;
        }

        public static async Task<IntPtr> CreateKeyFromSecretBytesAsync(
            KeyAlg keyAlg,
            SecretBuffer secretBytes)
        {
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_from_secret_bytes(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ByteBuffer.Create(secretBytes),
                ref localKeyHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return localKeyHandle;
        }

        public static async Task<IntPtr> CreateKeyFromKeyExchangeAsync(
            KeyAlg keyAlg,
            IntPtr secretKeyHandle,
            IntPtr publicKeyHandle)
        {
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_from_key_exchange(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                secretKeyHandle,
                publicKeyHandle,
                ref localKeyHandle);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return localKeyHandle;
        }
        #endregion

        #region Get
        public static async Task<SecretBuffer> GetPublicBytesFromKeyAsync(
            IntPtr localKeyHandle)
        {
            SecretBuffer secret = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_get_public_bytes(
                localKeyHandle,
                ref secret);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return secret;
        }

        public static async Task<SecretBuffer> GetSecretBytesFromKeyAsync(
            IntPtr localKeyHandle)
        {
            SecretBuffer secret = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_get_secret_bytes(
                localKeyHandle,
                ref secret);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return secret;
        }

        public static async Task<string> GetAlgorithmFromKeyAsync(
            IntPtr localKeyHandle)
        {
            string keyAlg = "";
            int errorCode = NativeMethods.askar_key_get_algorithm(
                localKeyHandle,
                ref keyAlg);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return keyAlg;
        }

        public static async Task<byte> GetEphemeralFromKeyAsync(
            IntPtr localKeyHandle)
        {
            byte ephemeral = 0;
            int errorCode = NativeMethods.askar_key_get_ephemeral(
                localKeyHandle,
                ref ephemeral);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return ephemeral;
        }

        public static async Task<string> GetJwkPublicFromKeyAsync(
            IntPtr localKeyHandle,
            KeyAlg keyAlg)
        {
            string jwk = "";
            int errorCode = NativeMethods.askar_key_get_jwk_public(
                localKeyHandle,
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ref jwk);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return jwk;
        }

        public static async Task<SecretBuffer> GetJwkSecretFromKeyAsync(
            IntPtr localKeyHandle)
        {
            SecretBuffer secret = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_get_jwk_secret(
                localKeyHandle,
                ref secret);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return secret;
        }

        public static async Task<string> GetJwkThumbprintFromKeyAsync(
            IntPtr localKeyHandle,
            KeyAlg keyAlg)
        {
            string thumbprint = "";
            int errorCode = NativeMethods.askar_key_get_jwk_thumbprint(
                localKeyHandle,
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ref thumbprint);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return thumbprint;
        }
        #endregion

        #region aead
        public static async Task<SecretBuffer> GetAeadRandomNonceFromKeyAsync(
            IntPtr localKeyHandle)
        {
            SecretBuffer secret = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_aead_random_nonce(
                localKeyHandle,
                ref secret);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return secret;
        }

        public static async Task<AeadParams> GetAeadParamsFromKeyAsync(
            IntPtr localKeyHandle)
        {
            AeadParams aeadParams = new() { nonce_length = 0, tag_length = 0 };
            int errorCode = NativeMethods.askar_key_aead_get_params(
                localKeyHandle,
                ref aeadParams);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return aeadParams;
        }

        public static async Task<int> GetAeadPaddingFromKeyAsync(
            IntPtr localKeyHandle,
            long msgLen)
        {
            int padding = 0;
            int errorCode = NativeMethods.askar_key_aead_get_padding(
                localKeyHandle,
                msgLen,
                ref padding);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return padding;
        }

        public static async Task<EncryptedBuffer> EncryptKeyWithAeadAsync(
            IntPtr localKeyHandle,
            string message,
            SecretBuffer nonce,
            string aad)
        {
            EncryptedBuffer encrypted = new() 
            {
                buffer = new SecretBuffer(){ len = 0, data = null },
                nonce_pos = 0,
                tag_pos = 0
            };
            int errorCode = NativeMethods.askar_key_aead_encrypt(
                localKeyHandle,
                ByteBuffer.Create(message),
                ByteBuffer.Create(nonce),
                ByteBuffer.Create(aad),
                ref encrypted);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return encrypted;
        }

        public static async Task<SecretBuffer> DecryptKeyWithAeadAsync(
            IntPtr localKeyHandle,
            string ciphertext,
            SecretBuffer nonce,
            string tag,
            string aad)
        {
            SecretBuffer aead = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_aead_decrypt(
                localKeyHandle,
                ByteBuffer.Create(ciphertext),
                ByteBuffer.Create(nonce),
                ByteBuffer.Create(tag),
                ByteBuffer.Create(aad),
                ref aead);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return aead;
        }
        #endregion

        #region Crypto
        public static async Task<SecretBuffer> CreateCryptoBoxRandomNonceAsync()
        {
            SecretBuffer nonce = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_crypto_box_random_nonce(
                ref nonce);

            if (errorCode != 0)
            {
                string error = "";
                NativeMethods.askar_get_current_error(ref error);
                Console.WriteLine(error);
            }

            return nonce;
        }
        #endregion
    }
}
