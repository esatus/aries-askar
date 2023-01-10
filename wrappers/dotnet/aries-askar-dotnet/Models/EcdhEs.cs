using aries_askar_dotnet.AriesAskar;
using System;
using System.Text;
using System.Threading.Tasks;
using static aries_askar_dotnet.Models.Structures;

namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// An instantiation of the ECDH-ES key derivation 
    /// </summary>
    public class EcdhEs
    {
        public string AlgId { get; set; }
        public string Apu { get; set; }
        public string Apv { get; set; }

        public EcdhEs(string algId, string apu, string apv)
        {
            AlgId = algId;
            Apu = apu;
            Apv = apv;
        }
    }

    /// <summary>
    /// Extension methods for ECDH-ES class. Contains direct encryption/decryption of messages and wrapping/unwrapping of a key. 
    /// </summary>
    public static class EcdhEsExtensions
    {
        /// <summary>
        /// Derive an ECDH-ES shared key for anonymous encryption.
        /// </summary>
        /// <param name="ecdhEs">The ecdhEs instance.</param>
        /// <param name="keyAlg">The key algorithm of the key.</param>
        /// <param name="ephemeralKey">The ephemeral key handle.</param>
        /// <param name="receiverKey">The receiver key handle.</param>
        /// <param name="receive">The receive flag as boolean, indicating receive or send. True for receive, false for send.</param>
        /// <returns>A new key handle as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> DeriveKeyAsync(
            this EcdhEs ecdhEs,
            KeyAlg keyAlg,
            IntPtr ephemeralKey,
            IntPtr receiverKey,
            bool receive)
        {
            return await KeyApi.DeriveEcdhEsAsync(
                keyAlg,
                ephemeralKey,
                receiverKey,
                ByteBuffer.Create(ecdhEs.AlgId).Decode(),
                ByteBuffer.Create(ecdhEs.Apu).Decode(),
                ByteBuffer.Create(ecdhEs.Apv).Decode(),
                Convert.ToByte(receive));
        }

        /// <summary>
        /// Perform a AEAD message encryption with a ECDH-ES key derivation of the receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdhEs">The ecdhEs instance.</param>
        /// <param name="encKeyAlg">The encoding key algorithm.</param>
        /// <param name="ephemeralKey">A ephemeral key.</param>
        /// <param name="receiverKey">The receiver key.</param>
        /// <param name="message">The message to encrypt.</param>
        /// <param name="nonce">A nonce; default null.</param>
        /// <param name="aad">The associated data; default null.</param>
        /// <returns>The triple of ciphertext (first), tag(second) and nonce(third) as <see cref="byte"/>[].</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<(byte[], byte[], byte[])> EncryptDirectAsync(
            this EcdhEs ecdhEs,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr receiverKey,
            string message,
            byte[] nonce = null,
            string aad = null)
        {
            IntPtr derivedKey = await ecdhEs.DeriveKeyAsync(encKeyAlg, ephemeralKey, receiverKey, false);
            (byte[] ciphertext, byte[] tagBytes, byte[] nonceBytes) = await KeyApi.EncryptKeyWithAeadAsync(derivedKey, message, nonce, aad);
            return (ciphertext, tagBytes, nonceBytes);
        }

        /// <summary>
        /// Perform a AEAD message decryption with a ECDH-ES key derivation of the receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdhEs">The ecdhEs instance.</param>
        /// <param name="encKeyAlg">The encoding key algorithm.</param>
        /// <param name="ephemeralKey">A ephemeral key.</param>
        /// <param name="receiverKey">The receiver key.</param>
        /// <param name="ciphertext">The encryption ciphertext.</param>
        /// <param name="nonce">The encryption nonce.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="aad">The associated data; default null.</param>
        /// <returns>The decrypted message as <see cref="string"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> DecryptDirectAsync(
            this EcdhEs ecdhEs,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr receiverKey,
            byte[] ciphertext,
            byte[] nonce,
            byte[] tag,
            string aad = null)
        {
            UTF8Encoding Decoder = new UTF8Encoding(true, true);
            IntPtr derivedKey = await ecdhEs.DeriveKeyAsync(encKeyAlg, ephemeralKey, receiverKey, true);
            return Decoder.GetString(await KeyApi.DecryptKeyWithAeadAsync(derivedKey, ciphertext, nonce, tag, aad));
        }

        /// <summary>
        /// Wrap another key using a ECDH-ES key derivation of the receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdhEs">The ecdhEs instance.</param>
        /// <param name="wrapKeyAlg">The wrapping key algorithm.</param>
        /// <param name="ephemeralKey">A ephemeral key.</param>
        /// <param name="receiverKey">The receiver key.</param>
        /// <param name="cek">A content-encryption key.</param>
        /// <returns>The triple of ciphertext (first), tag(second) and nonce(third) as <see cref="byte"/>[].</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<(byte[], byte[], byte[])> SenderWrapKeyAsync(
            this EcdhEs ecdhEs,
            KeyAlg wrapKeyAlg,
            IntPtr ephemeralKey,
            IntPtr receiverKey,
            IntPtr cek)
        {
            IntPtr derivedKey = await ecdhEs.DeriveKeyAsync(wrapKeyAlg, ephemeralKey, receiverKey, false);
            (byte[] ciphertext, byte[] tagBytes, byte[] nonceBytes) = await KeyApi.WrapKeyAsync(derivedKey, cek, null);
            return (ciphertext, tagBytes, nonceBytes);
        }

        /// <summary>
        /// Unwrap a key using a ECDH-ES key derivation of the receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdhEs">The ecdhEs instance.</param>
        /// <param name="wrapKeyAlg">The wrapping key algorithm.</param>
        /// <param name="encKeyAlg">The encoding key algorithm.</param>
        /// <param name="ephemeralKey">A ephemeral key.</param>
        /// <param name="receiverKey">The receiver key.</param>
        /// <param name="ciphertext">The encryption ciphertext.</param>
        /// <param name="nonce">The encryption nonce; default null.</param>
        /// <param name="tag">The tag; default null.</param>
        /// <returns>The key handle as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> ReceiverUnwrapKeyAsync(
            this EcdhEs ecdhEs,
            KeyAlg wrapKeyAlg,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr receiverKey,
            byte[] ciphertext,
            byte[] nonce = null,
            byte[] tag = null)
        {
            IntPtr derivedKey = await ecdhEs.DeriveKeyAsync(wrapKeyAlg, ephemeralKey, receiverKey, true);
            return await KeyApi.UnwrapKeyAsync(
                derivedKey,
                encKeyAlg,
                ciphertext,
                nonce,
                tag);
        }
    }
}