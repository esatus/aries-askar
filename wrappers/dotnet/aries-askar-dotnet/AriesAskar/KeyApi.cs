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
        /// Creates a new random key or keypair.
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
        /// Creates a new deterministic key or keypair from a seed.
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
        /// Creates a new key or keypair from a jwk.
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
        /// Create a public key from its byte representation
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
        /// Creates a new secret key or keypair from its byte representation
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
        /// Derive a new key from a Diffie-Hellman exchange between this keypair and a public key
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
        /// Gets the key creation algorithm of a key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The key creation algorithm from a key as <see cref="string"/>.</returns>
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
        /// Gets the ephemeral setting of a key.
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
        /// Gets the public jwk representation for this key or keypair
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <returns>The public jwk representation as <see cref="string"/>.</returns>
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
        /// Gets the private jwk representation for this private key or keypair
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <returns>The private jwk representation as <see cref="byte"/>[].</returns>
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
        /// Gets the jwk thumbprint for this key or keypair
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
        /// Gets the aead parameter lengths for a key.
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
        /// Calculates and gets the padding required for a message.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="msgLen">The length of the message as <see cref="long"/>.</param>
        /// <returns>The required padding for a message as <see cref="int"/>.</returns>
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
        /// Perform aead message encryption with this encryption key.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The encryption message as <see cref="string"/>.</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte[]"/>.</param>
        /// <param name="aad">The encryption aad as <see cref="string"/>.</param>
        /// <returns>The encrypted message as a triple of <see cref="byte[]"/>. 
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
        /// Perform aead message decryption with this encryption key.
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
        /// Creates a random nonce for crypto box.
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
        /// Encrypt a message with crypto_box and a detached nonce.
        /// </summary>
        /// <param name="recipKey">The handle of the recipient key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The handle of the sender key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The message to encrypt as <see cref="string"/>.</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte[]"/>.</param>
        /// <returns>The encrypted message as <see cref="byte[]"/>.</returns>
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
        /// Decrypt a message with crypto_box and a detached nonce.
        /// </summary>
        /// <param name="recipKey">The handle of the recipient key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The handle of the sender key as <see cref="IntPtr"/>.</param>
        /// <param name="encrypted">The encrypted message as <see cref="byte[]"/>.</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte[]"/>.</param>
        /// <returns>The decrypted message as <see cref="string"/>.</returns>
        public static async Task<string> OpenCryptoBoxAsync(
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
            return output.DecodeToString();
        }

        /// <summary>
        /// Perform message encryption equivalent to libsodium's `crypto_box_seal`.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The message to encrypt as <see cref="string"/>.</param>
        /// <returns>The encrypted message as <see cref="byte[]"/>.</returns>
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
        /// Perform message decryption equivalent to libsodium's `crypto_box_seal_open`.
        /// </summary>
        /// <param name="localKeyHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="ciphertext">The encryption ciphertext as <see cref="byte[]"/>.</param>
        /// <returns>The decrypted message as <see cref="string"/>.</returns>
        public static async Task<string> OpenSealCryptoBoxAsync(
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
            return output.DecodeToString();
        }
        #endregion

        #region Utils
        /// <summary>
        /// Map this key or keypair to its equivalent for another key algorithm
        /// </summary>
        /// <param name="inputHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        /// <param name="keyAlg">The <see cref="KeyAlg"/> used to create the key.</param>
        /// <returns>The mapped key as <see cref="IntPtr"/>.</returns>
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
        /// Free the key object in the backend.
        /// </summary>
        /// <param name="inputHandle">The handle of the key as <see cref="IntPtr"/>.</param>
        public static async Task FreeKeyAsync(
            IntPtr inputHandle)
        {
            NativeMethods.askar_key_free(inputHandle);
        }

        /// <summary>
        /// Sign a message with this private signing key.
        /// Standard signature output for ed25519 is EdDSA, for Elliptic curve DSA using P-256 and SHA-256 is ES256, 
        /// for Elliptic curve DSA using K-256 and SHA-256 is signature type ES256K.
        /// </summary>
        /// <param name="localKeyHandle">The signing key handle as <see cref="IntPtr"/>.</param>
        /// <param name="message">The message as <see cref="byte"/>[].</param>
        /// <param name="sigType">The signature type as <see cref="SignatureType"/>.</param>
        /// <returns>The signature as <see cref="byte"/>[].</returns>
        public static async Task<byte[]> SignMessageFromKeyAsync(
            IntPtr localKeyHandle,
            byte[] message,
            SignatureType sigType)
        {
            ByteBuffer output = new() { len = 0, value = new IntPtr() };

            int errorCode = NativeMethods.askar_key_sign_message(
                localKeyHandle,
                ByteBuffer.Create(message),
                FfiStr.Create(sigType.ToSigTypeString()),
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
        /// Verify a message signature with this private signing key or public verification key
        /// Standard signature output for ed25519 is EdDSA, for Elliptic curve DSA using P-256 and SHA-256 is ES256, 
        /// for Elliptic curve DSA using K-256 and SHA-256 is signature type ES256K.
        /// </summary>
        /// <param name="localKeyHandle">The signing key / public verification key handle as <see cref="IntPtr"/>.</param>
        /// <param name="message">The message as <see cref="byte"/>[].</param>
        /// <param name="signature">The signature as <see cref="byte"/>[].</param>
        /// <param name="sigType">The signature type as <see cref="SignatureType"/>.</param>
        /// <returns>The result of the verify call as <see cref="bool"/> flag, true if verification was successfull else false.</returns>
        public static async Task<bool> VerifySignatureFromKeyAsync(
            IntPtr localKeyHandle,
            byte[] message,
            byte[] signature,
            SignatureType sigType)
        {
            byte output = new();

            int errorCode = NativeMethods.askar_key_verify_signature(
                localKeyHandle,
                ByteBuffer.Create(message),
                ByteBuffer.Create(signature),
                FfiStr.Create(sigType.ToSigTypeString()),
                ref output);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                Console.WriteLine(error);
                throw AriesAskarException.FromSdkError(error);
            }
            return Convert.ToBoolean(output);
        }

        /// <summary>
        /// Wrap another key using this key.
        /// </summary>
        /// <param name="localKeyHandle">The key handle as <see cref="IntPtr"/>.</param>
        /// <param name="otherLocalKeyHandle">The key handle of the key to be wrapped as <see cref="IntPtr"/>.</param>
        /// <param name="nonce">A nonce as <see cref="byte"/>[]; default null.</param>
        /// <returns>The triple of the encryption ciphertext (first), tag(second) and nonce(third) as <see cref="byte"/>[].</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
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

        /// <summary>
        /// Unwrap a key using this key.
        /// </summary>
        /// <param name="localKeyHandle">The key handle as <see cref="IntPtr"/>.</param>
        /// <param name="alg">The key algorithm of the this key as <see cref="KeyAlg"/>.</param>
        /// <param name="ciphertext">The encryption ciphertext as <see cref="byte"/>[].</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte"/>[].</param>
        /// <param name="tag">The encryption tag as <see cref="byte"/>[].</param>
        /// <returns>The key handle of the unwrapped key as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> UnwrapKeyAsync(
            IntPtr localKeyHandle,
            KeyAlg alg,
            byte[] ciphertext,
            byte[] nonce,
            byte[] tag)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_unwrap_key(
                localKeyHandle,
                FfiStr.Create(alg.ToKeyAlgString()),
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

        /// <summary>
        /// Derive an ECDH-ES shared key for anonymous encryption
        /// </summary>
        /// <param name="alg">The key algorithm of the key as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemKey">The ephemeral key handle as <see cref="IntPtr"/>.</param>
        /// <param name="recipKey">The recipient key handle as <see cref="IntPtr"/>.</param>
        /// <param name="algId">The algorithm id as <see cref="byte"/>[].</param>
        /// <param name="apu">The apu (sender identity) as <see cref="byte"/>[].</param>
        /// <param name="apv">The apv (receiver identity) as <see cref="byte"/>[].</param>
        /// <param name="receive">The receive flag as <see cref="bool"/>, indicating receive or send. True for receive, false for send.</param>
        /// <returns>The key handle of the derived key as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> DeriveEcdhEsAsync(
            KeyAlg alg,
            IntPtr ephemKey,
            IntPtr recipKey,
            byte[] algId,
            byte[] apu,
            byte[] apv,
            byte receive)
        {
            IntPtr output = new();

            int errorCode = NativeMethods.askar_key_derive_ecdh_es(
                FfiStr.Create(alg.ToKeyAlgString()),
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

        /// <summary>
        /// Derive an ECDH-1PU shared key for authenticated encryption
        /// </summary>
        /// <param name="alg">The key algorithm of the key as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemKey">The ephemeral key handle as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The sender key handle as <see cref="IntPtr"/>.</param>
        /// <param name="recipKey">The recipient key handle as <see cref="IntPtr"/>.</param>
        /// <param name="algId">The algorithm id as <see cref="byte"/>[].</param>
        /// <param name="apu">The apu (sender identity) as <see cref="byte"/>[].</param>
        /// <param name="apv">The apv (receiver identity) as <see cref="byte"/>[].</param>
        /// <param name="ccTag">The ccTag as <see cref="byte"/>[].</param>
        /// <param name="receive">The receive flag as <see cref="bool"/>, indicating receive or send. True for receive, false for send.</param>
        /// <returns>The key handle of the derived key as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> DeriveEcdh1puAsync(
            KeyAlg alg,
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
                FfiStr.Create(alg.ToKeyAlgString()),
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
