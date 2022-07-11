using aries_askar_dotnet.Models;
using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    public class KeyApi
    {
        #region Create
        /// <summary>
        /// Creates a new key.
        /// </summary>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <param name="ephemeral">The <see cref="bool"/> used to create the key.</param>
        /// <returns>The handle of the created key as <see cref="IntPtr"/>.</returns>
        public static async Task<IntPtr> CreateKeyAsync(
            KeyAlg keyAlg,
            bool ephemeral)
        {
            int ephemeralAsInt = ephemeral ? 1 : 0;
            IntPtr localKeyHandle = new();
            int errorCode = NativeMethods.askar_key_generate(
                FfiStr.Create(keyAlg.ToKeyAlgString()),
                (byte)ephemeralAsInt,
                ref localKeyHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }

            return localKeyHandle;
        }

        /// <summary>
        /// Creates a new key from a seed.
        /// </summary>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <param name="seed">The seed <see cref="string"/> used to create the key.</param>
        /// <param name="method">The <see cref="SeedMethod"/> used to create the key.</param>
        /// <returns>The handle of the created key as <see cref="IntPtr"/>.</returns>
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

        /// <summary>
        /// Creates a new key from a jwk.
        /// </summary>
        /// <param name="jwkJson">The jwk as json <see cref="string"/> used to create the key.</param>
        /// <returns>The handle of the created key as <see cref="IntPtr"/>.</returns>
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

        /// <summary>
        /// Creates a new key from public bytes.
        /// </summary>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <param name="publicBytes">The public bytes as <see cref="byte[]"/> used to create the key.</param>
        /// <returns>The handle of the created key as <see cref="IntPtr"/>.</returns>
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

        /// <summary>
        /// Creates a new key from secret bytes.
        /// </summary>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <param name="secretBytes">The secret bytes <see cref="byte[]"/> used to create the key.</param>
        /// <returns>The handle of the created key as <see cref="IntPtr"/>.</returns>
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

        /// <summary>
        /// Creates a new key from exchange.
        /// </summary>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <param name="secretKeyHandle">The secret key handle as <see cref="IntPtr"/> used to create the key.</param>
        /// <param name="publicKeyHandle">The public key handle as <see cref="IntPtr"/> used to create the key.</param>
        /// <returns>The handle of the created key as <see cref="IntPtr"/>.</returns>
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
        /// <summary>
        /// Gets the public bytes from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The public bytes of a key as <see cref="byte[]"/>.</returns>
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

        /// <summary>
        /// Gets the secret bytes from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The secret bytes of a key as <see cref="byte[]"/>.</returns>
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

        /// <summary>
        /// Gets the creation key algorithm from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The creation key algorithm from a key as <see cref="string"/>.</returns>
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

        /// <summary>
        /// Gets the ephemeral setting from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The ephemeral setting as <see cref="bool"/>.</returns>
        public static async Task<bool> GetEphemeralFromKeyAsync(
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

            return Convert.ToBoolean(ephemeral);
        }

        /// <summary>
        /// Gets the jwk public from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <returns>The jwk public as <see cref="string"/>.</returns>
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

        /// <summary>
        /// Gets the jwk secret from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The jwk secret as <see cref="string"/>.</returns>
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

        /// <summary>
        /// Gets the jwk thumbprint from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <returns>The jwk thumbprint as <see cref="string"/>.</returns>
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
        /// <summary>
        /// Creates an aead random nonce from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The created nonce as <see cref="byte[]"/>.</returns>
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

        /// <summary>
        /// Gets the aead params from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The aead params as a pair of <see cref="uint"/>. 
        /// First is nonce length, second is tag length.</returns>
        public static async Task<(uint, uint)> GetAeadParamsFromKeyAsync(
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

            return (aeadParams.nonce_length, aeadParams.tag_length);
        }

        /// <summary>
        /// Gets the aead padding from a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="msgLen">The length of the message as <see cref="long"/>.</param>
        /// <returns>The aead padding as <see cref="int"/>.</returns>
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

        /// <summary>
        /// Encrypts a message with aead.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The encryption message as <see cref="string"/>.</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte[]"/>.</param>
        /// <param name="aad">The encryption aad as <see cref="string"/>.</param>
        /// <returns>The encrypted message as a pair of <see cref="byte[]"/>. 
        /// First is value. Second is tag. Third is nonce.</returns>
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

        /// <summary>
        /// Decrypts a message with aead.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="ciphertext">The encryption ciphertext as <see cref="byte[]"/>.</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte[]"/>.</param>
        /// <param name="tag">The encryption tag as <see cref="byte[]"/>.</param>
        /// <param name="aad">The encryption aad as <see cref="string"/>.</param>
        /// <returns>The decrypted message as <see cref="byte[]"/>.</returns>
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
        /// <summary>
        /// Creates a random nonce for creating a crypto box.
        /// </summary>
        /// <returns>The nonce as <see cref="byte[]"/>.</returns>
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

        /// <summary>
        /// Creates a crypto box.
        /// </summary>
        /// <param name="recipKey">The handle of the recipient key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The handle of the sender key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The encryption message as <see cref="string"/>.</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte[]"/>.</param>
        /// <returns>The box as <see cref="byte[]"/>.</returns>
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

        /// <summary>
        /// Opens a crypto box.
        /// </summary>
        /// <param name="recipKey">The handle of the recipient key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The handle of the sender key as <see cref="IntPtr"/>.</param>
        /// <param name="encrypted">The encrypted message as <see cref="byte[]"/>.</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte[]"/>.</param>
        /// <returns>The opened box as <see cref="byte[]"/>.</returns>
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

        /// <summary>
        /// Seals a crypto box.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The encryption message as <see cref="string"/>.</param>
        /// <returns>The sealed box as <see cref="byte[]"/>.</returns>
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

        /// <summary>
        /// Opens a sealed crypto box.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="ciphertext">The encryption ciphertext as <see cref="byte[]"/>.</param>
        /// <returns>The box as <see cref="byte[]"/>.</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns></returns>
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
