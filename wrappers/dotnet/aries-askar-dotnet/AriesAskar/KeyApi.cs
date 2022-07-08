using aries_askar_dotnet.Models;
using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.AriesAskar
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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
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

            if(errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        public static async Task<IntPtr> CreateKeyFromPublicBytesAsync(
            KeyAlg keyAlg,
            byte[] publicBytes)
        {
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_from_public_bytes(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ByteBuffer.Create(publicBytes),
                ref localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        public static async Task<IntPtr> CreateKeyFromSecretBytesAsync(
            KeyAlg keyAlg,
            byte[] secretBytes)
        {
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_from_secret_bytes(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ByteBuffer.Create(secretBytes),
                ref localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }
        #endregion

        #region Get
        public static async Task<byte[]> GetPublicBytesFromKeyAsync(
            IntPtr localKeyHandle)
        {
            ByteBuffer secret = new() { len = 0, value = new IntPtr() };
            int errorCode = NativeMethods.askar_key_get_public_bytes(
                localKeyHandle,
                ref secret);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return secret.Decode();
        }

        public static async Task<byte[]> GetSecretBytesFromKeyAsync(
            IntPtr localKeyHandle)
        {
            ByteBuffer secret = new() { len = 0, value = new IntPtr() };
            int errorCode = NativeMethods.askar_key_get_secret_bytes(
                localKeyHandle,
                ref secret);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return secret.Decode();
        }

        public static async Task<string> GetAlgorithmFromKeyAsync(
            IntPtr localKeyHandle)
        {
            string keyAlg = "";
            int errorCode = NativeMethods.askar_key_get_algorithm(
                localKeyHandle,
                ref keyAlg);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return jwk;
        }

        public static async Task<byte[]> GetJwkSecretFromKeyAsync(
            IntPtr localKeyHandle)
        {
            ByteBuffer secret = new() { len = 0, value = new IntPtr() };
            int errorCode = NativeMethods.askar_key_get_jwk_secret(
                localKeyHandle,
                ref secret);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return secret.Decode();
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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return thumbprint;
        }
        #endregion

        #region aead
        public static async Task<byte[]> GetAeadRandomNonceFromKeyAsync(
            IntPtr localKeyHandle)
        {
            ByteBuffer secret = new() { len = 0, value = new IntPtr() };
            int errorCode = NativeMethods.askar_key_aead_random_nonce(
                localKeyHandle,
                ref secret);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return secret.Decode();
        }

        public static async Task<AeadParams> GetAeadParamsFromKeyAsync(
            IntPtr localKeyHandle)
        {
            AeadParams aeadParams = new() { nonce_length = 0, tag_length = 0 };
            int errorCode = NativeMethods.askar_key_aead_get_params(
                localKeyHandle,
                ref aeadParams);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
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

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return padding;
        }

        public static async Task<(byte[], byte[], byte[])> EncryptKeyWithAeadAsync(
            IntPtr localKeyHandle,
            string message,
            byte[] nonce,
            string aad)
        {
            EncryptedBuffer encrypted = new()
            {
                buffer = new ByteBuffer(){ len = 0, value = new IntPtr() },
                nonce_pos = 0,
                tag_pos = 0
            };
            int errorCode = NativeMethods.askar_key_aead_encrypt(
                localKeyHandle,
                ByteBuffer.Create(message),
                ByteBuffer.Create(nonce),
                ByteBuffer.Create(aad),
                ref encrypted);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return encrypted.Decode();
        }

        public static async Task<byte[]> DecryptKeyWithAeadAsync(
            IntPtr localKeyHandle,
            byte[] ciphertext,
            byte[] nonce,
            byte[] tag,
            string aad)
        {
            ByteBuffer aead = new() { len = 0, value = new IntPtr() };
            int errorCode = NativeMethods.askar_key_aead_decrypt(
                localKeyHandle,
                ByteBuffer.Create(ciphertext),
                ByteBuffer.Create(nonce),
                ByteBuffer.Create(tag),
                ByteBuffer.Create(aad),
                ref aead);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return aead.Decode();
        }
        #endregion

        #region Crypto
        public static async Task<byte[]> CreateCryptoBoxRandomNonceAsync()
        {
            ByteBuffer nonce = new() { len = 0, value = new IntPtr() };
            int errorCode = NativeMethods.askar_key_crypto_box_random_nonce(
                ref nonce);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return nonce.Decode();
        }

        public static async Task<byte[]> CryptoBoxAsync(
            IntPtr recipKey,
            IntPtr senderKey,
            string message,
            byte[] nonce)
        {
            ByteBuffer output = new() { len = 0, value = new IntPtr() };

            int errorCode = NativeMethods.askar_key_crypto_box(
                recipKey,
                senderKey,
                ByteBuffer.Create(message),
                ByteBuffer.Create(nonce),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output.Decode();
        }

        public static async Task<byte[]> OpenCryptoBoxAsync(
            IntPtr recipKey,
            IntPtr senderKey,
            byte[] encrypted,
            byte[] nonce)
        {
            ByteBuffer output = new() { len = 0, value = new IntPtr() };

            int errorCode = NativeMethods.askar_key_crypto_box_open(
                recipKey,
                senderKey,
                ByteBuffer.Create(encrypted),
                ByteBuffer.Create(nonce),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output.Decode();
        }

        public static async Task<byte[]> SealCryptoBoxAsync(
            IntPtr localKeyHandle,
            string message)
        {
            ByteBuffer output = new() { len = 0, value = new IntPtr() };

            int errorCode = NativeMethods.askar_key_crypto_box_seal(
                localKeyHandle,
                ByteBuffer.Create(message),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output.Decode();
        }

        public static async Task<byte[]> OpenSealCryptoBoxAsync(
            IntPtr localKeyHandle,
            byte[] ciphertext)
        {
            ByteBuffer output = new() { len = 0, value = new IntPtr() };

            int errorCode = NativeMethods.askar_key_crypto_box_seal_open(
                localKeyHandle,
                ByteBuffer.Create(ciphertext),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output.Decode();
        }
        #endregion

        #region Utils
        public static async Task<IntPtr> ConvertKeyAsync(
            IntPtr inputHandle,
            KeyAlg keyAlg)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_convert(
                inputHandle,
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task FreeKeyAsync(
            IntPtr inputHandle)
        {
            NativeMethods.askar_key_free(inputHandle);
        }

        /// <summary>
        /// Standard signature output for ed25519 is EdDSA
        /// Elliptic curve DSA using P-256 and SHA-256 ES256
        /// Elliptic curve DSA using K-256 and SHA-256 ES256K
        /// </summary>
        /// <param name="localKeyHandle"></param>
        /// <param name="message"></param>
        /// <param name="sigType"></param>
        /// <returns></returns>
        public static async Task<byte[]> SignMessageFromKeyAsync(
            IntPtr localKeyHandle,
            byte[] message,
            string sigType)
        {
            ByteBuffer output = new() { len = 0, value = new IntPtr() };

            int errorCode = NativeMethods.askar_key_sign_message(
                localKeyHandle,
                ByteBuffer.Create(message),
                FfiStr.Create(sigType),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output.Decode();
        }

        public static async Task<bool> VerifySignatureFromKeyAsync(
            IntPtr localKeyHandle,
            byte[] message,
            byte[] signature,
            string sigType)
        {
            byte output = new();

            int errorCode = NativeMethods.askar_key_verify_signature(
                localKeyHandle,
                ByteBuffer.Create(message),
                ByteBuffer.Create(signature),
                FfiStr.Create(sigType),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return Convert.ToBoolean(output);
        }

        public static async Task<(byte[], byte[], byte[])> WrapKeyAsync(
            IntPtr localKeyHandle,
            IntPtr otherLocalKeyHandle,
            byte[] nonce = null)
        {
            EncryptedBuffer output = new()
            {
                buffer = new() { len = 0, value = new IntPtr() },
                nonce_pos = 0,
                tag_pos = 0
            };

            int errorCode = NativeMethods.askar_key_wrap_key(
                localKeyHandle,
                otherLocalKeyHandle,
                ByteBuffer.Create(nonce),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output.Decode();
        }

        public static async Task<IntPtr> UnwrapKeyAsync(
            IntPtr localKeyHandle,
            string alg,
            byte[] ciphertext,
            byte[] nonce,
            byte[] tag)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_unwrap_key(
                localKeyHandle,
                FfiStr.Create(alg),
                ByteBuffer.Create(ciphertext),
                ByteBuffer.Create(nonce),
                ByteBuffer.Create(tag),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<IntPtr> DeriveEcdhEsAsync(
            string alg,
            IntPtr ephemKey,
            IntPtr recipKey,
            byte[] algId,
            byte[] apu,
            byte[] apv,
            byte receive)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_derive_ecdh_es(
                FfiStr.Create(alg),
                ephemKey,
                recipKey,
                ByteBuffer.Create(algId),
                ByteBuffer.Create(apu),
                ByteBuffer.Create(apv),
                receive,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<IntPtr> DeriveEcdh1puAsync(
            string alg,
            IntPtr ephemKey,
            IntPtr senderKey,
            IntPtr recipKey,
            byte[] algId,
            byte[] apu,
            byte[] apv,
            byte[] ccTag,
            byte receive)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_derive_ecdh_1pu(
                FfiStr.Create(alg),
                ephemKey,
                senderKey,
                recipKey,
                ByteBuffer.Create(algId),
                ByteBuffer.Create(apu),
                ByteBuffer.Create(apv),
                ByteBuffer.Create(ccTag),
                receive,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }
        #endregion
    }
}
