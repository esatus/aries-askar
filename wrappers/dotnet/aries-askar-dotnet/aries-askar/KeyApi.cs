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

        public static async Task<uint> CreateKeyFromSecretBytesAsync(
            string alg,
            ByteBuffer secret)
        {
            uint localKeyHandle = 0;
            int errorCode = NativeMethods.askar_key_from_secret_bytes(
                FfiStr.Create(alg),
                secret,
                ref localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        public static async Task<SecretBuffer> GetSecretBytesFromKeyAsync(
            uint localKeyHandle)
        {
            SecretBuffer output = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_get_secret_bytes(
                localKeyHandle,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return output;
        }

        public static async Task<uint> ConvertKeyAsync(
            uint localKeyHandle,
            string alg)
        {
            uint outLocalKeyHandle = 0;
            int errorCode = NativeMethods.askar_key_convert(
                localKeyHandle,
                FfiStr.Create(alg),
                ref outLocalKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return outLocalKeyHandle;
        }

        public static async Task<uint> FromKeyExchange(
            string alg,
            uint skHandle,
            uint pkHandle)
        {
            uint outLocalKeyHandle = 0;
            int errorCode = NativeMethods.askar_key_from_key_exchange(
                FfiStr.Create(alg),
                skHandle,
                pkHandle,
                ref outLocalKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return outLocalKeyHandle;
        }

        public static async Task FreeKeyHandle(
            uint localKeyHandle)
        {
            int errorCode = NativeMethods.askar_key_free(
                localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
        }

        public static async Task GetAlgorithmFromKey(
            uint localKeyHandle)
        {
            string alg = "";
            int errorCode = NativeMethods.askar_key_get_algorithm(
                localKeyHandle,
                alg);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
        }

        public static async Task<byte> GetEphemeralFromkeyAsync(
            uint localKeyHandle)
        {
            byte outEphemeral = new();
            int errorCode = NativeMethods.askar_key_get_ephemeral(
                localKeyHandle,
                ref outEphemeral);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return outEphemeral;
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
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return jwk;
        }

        public static async Task<SecretBuffer> GetJwkSecretFromKeyAsync(
            uint localKeyHandle)
        {
            SecretBuffer output = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_get_jwk_secret(
                localKeyHandle,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<string> GetJwkThumbprintFromKeyAsync(
            uint localKeyHandle,
            string alg)
        {
            string output = "";
            int errorCode = NativeMethods.askar_key_get_jwk_thumbprint(
                localKeyHandle,
                FfiStr.Create(alg),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<SecretBuffer> CreateAEADRandomNonceForKeyAsync(
            uint localKeyHandle)
        {
            SecretBuffer output = new() { len = 0, data = null };
            int errorCode = NativeMethods.askar_key_aead_random_nonce(
                localKeyHandle,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<AeadParams> GetAEADParamsFromKeyAsync(
            uint localKeyHandle)
        {
            AeadParams output = new() { nonce_length = 0, tag_length= 0};
            int errorCode = NativeMethods.askar_key_aead_get_params(
                localKeyHandle,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<uint> GetAEADPaddingFromKeyAsync(
            uint localKeyHandle,
            long msgLen)
        {
            uint output = 0;
            int errorCode = NativeMethods.askar_key_aead_get_padding(
                localKeyHandle,
                msgLen,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<EncryptedBuffer> GetAEADEncryptFromKeyAsync(
            uint localKeyHandle,
            ByteBuffer message, 
            ByteBuffer nonce,
            ByteBuffer aad)
        {
            EncryptedBuffer output = new()
            {
                buffer = new SecretBuffer() { len = 0, data = null },
                nonce_pos = 0,
                tag_pos = 0
            };
            int errorCode = NativeMethods.askar_key_aead_encrypt(
                localKeyHandle,
                message,
                nonce,
                aad,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<SecretBuffer> GetAEADDecryptFromKeyAsync(
            uint localKeyHandle,
            ByteBuffer ciphertext,
            ByteBuffer nonce,
            ByteBuffer tag,
            ByteBuffer aad)
        {
            SecretBuffer output = new SecretBuffer() { len = 0, data = null };
            
            int errorCode = NativeMethods.askar_key_aead_decrypt(
                localKeyHandle,
                ciphertext,
                nonce,
                tag,
                aad,
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<SecretBuffer> GetSignMessageFromKeyAsync(
            uint localKeyHandle,
            ByteBuffer message,
            string sigType)
        {
            SecretBuffer output = new SecretBuffer() { len = 0, data = null };

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
            uint localKeyHandle,
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
            uint localKeyHandle,
            uint otherLocalKeyHandle,
            ByteBuffer nonce)
        {
            EncryptedBuffer output = new()
            {
                buffer = new SecretBuffer() { len = 0, data = null },
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

        public static async Task<uint> UnwrapKeyAsync(
            uint localKeyHandle,
            string alg, 
            ByteBuffer ciphertext,
            ByteBuffer nonce,
            ByteBuffer tag)
        {
            uint output = 0;

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

        public static async Task<SecretBuffer> CreateCryptoBoxRandomNonceAsync()
        {
            SecretBuffer output = new SecretBuffer() { len = 0, data = null };

            int errorCode = NativeMethods.askar_key_crypto_box_random_nonce(
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return output;
        }

        public static async Task<SecretBuffer> CryptoBoxAsync(
            uint recipKey,
            uint senderKey,
            ByteBuffer message, 
            ByteBuffer nonce)
        {
            SecretBuffer output = new SecretBuffer() { len = 0, data = null };

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
            uint recipKey,
            uint senderKey,
            ByteBuffer message,
            ByteBuffer nonce)
        {
            SecretBuffer output = new SecretBuffer() { len = 0, data = null };

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
            uint localKeyHandle,
            ByteBuffer message)
        {
            SecretBuffer output = new SecretBuffer() { len = 0, data = null };

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
            uint localKeyHandle,
            ByteBuffer ciphertext)
        {
            SecretBuffer output = new SecretBuffer() { len = 0, data = null };

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

        public static async Task<uint> DeriveEcdhEsAsync(
            string alg,
            uint ephemKey,
            uint recipKey,
            ByteBuffer algId,
            ByteBuffer apu,
            ByteBuffer apv,
            byte receive)
        {
            uint output = 0;

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

        public static async Task<uint> DeriveEcdh1puAsync(
            string alg,
            uint ephemKey,
            uint senderKey,
            uint recipKey,
            ByteBuffer algId,
            ByteBuffer apu,
            ByteBuffer apv,
            ByteBuffer ccTag,
            byte receive)
        {
            uint output = 0;

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
    }
}
