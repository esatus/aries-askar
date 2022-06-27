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

        public static async Task<SecretBuffer> CryptoBoxAsync(
            IntPtr recipKey,
            IntPtr senderKey,
            ByteBuffer message,
            ByteBuffer nonce)
        {
            SecretBuffer output = new() { len = 0, data = null };

            int errorCode = NativeMethods.askar_key_crypto_box(
                recipKey,
                senderKey,
                message,
                nonce,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<SecretBuffer> OpenCryptoBoxAsync(
            IntPtr recipKey,
            IntPtr senderKey,
            ByteBuffer message,
            ByteBuffer nonce)
        {
            SecretBuffer output = new() { len = 0, data = null };

            int errorCode = NativeMethods.askar_key_crypto_box_open(
                recipKey,
                senderKey,
                message,
                nonce,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<SecretBuffer> SealCryptoBoxAsync(
            IntPtr localKeyHandle,
            ByteBuffer message)
        {
            SecretBuffer output = new() { len = 0, data = null };

            int errorCode = NativeMethods.askar_key_crypto_box_seal(
                localKeyHandle,
                message,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<SecretBuffer> OpenSealCryptoBoxAsync(
            IntPtr localKeyHandle,
            ByteBuffer ciphertext)
        {
            SecretBuffer output = new() { len = 0, data = null };

            int errorCode = NativeMethods.askar_key_crypto_box_seal_open(
                localKeyHandle,
                ciphertext,
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

        #region Utils
        public static async Task<IntPtr> ConvertKey(
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

        public static async Task FreeKey(
            IntPtr inputHandle)
        {
            int errorCode = NativeMethods.askar_key_free(
                inputHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            };
        }

        public static async Task<SecretBuffer> GetSignMessageFromKeyAsync(
            IntPtr localKeyHandle,
            ByteBuffer message,
            string sigType)
        {
            SecretBuffer output = new() { len = 0, data = null };

            int errorCode = NativeMethods.askar_key_sign_message(
                localKeyHandle,
                message,
                FfiStr.Create(sigType),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<bool> VerifySignatureFromKeyAsync(
            IntPtr localKeyHandle,
            ByteBuffer message,
            ByteBuffer signature,
            string sigType)
        {
            byte output = new();

            int errorCode = NativeMethods.askar_key_verify_signature(
                localKeyHandle,
                message,
                signature,
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

        public static async Task<EncryptedBuffer> WrapKeyAsync(
            IntPtr localKeyHandle,
            IntPtr otherLocalKeyHandle,
            ByteBuffer nonce)
        {
            EncryptedBuffer output = new()
            {
                buffer = new() { len = 0, data = null },
                nonce_pos = 0,
                tag_pos = 0
            };

            int errorCode = NativeMethods.askar_key_wrap_key(
                localKeyHandle,
                otherLocalKeyHandle,
                nonce,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<IntPtr> UnwrapKeyAsync(
            IntPtr localKeyHandle,
            string alg,
            ByteBuffer ciphertext,
            ByteBuffer nonce,
            ByteBuffer tag)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_unwrap_key(
                localKeyHandle,
                FfiStr.Create(alg),
                ciphertext,
                nonce,
                tag,
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
            ByteBuffer algId,
            ByteBuffer apu,
            ByteBuffer apv,
            byte receive)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_derive_ecdh_es(
                FfiStr.Create(alg),
                ephemKey,
                recipKey,
                algId,
                apu,
                apv,
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
            ByteBuffer algId,
            ByteBuffer apu,
            ByteBuffer apv,
            ByteBuffer ccTag,
            byte receive)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_derive_ecdh_1pu(
                FfiStr.Create(alg),
                ephemKey,
                senderKey,
                recipKey,
                algId,
                apu,
                apv,
                ccTag,
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
