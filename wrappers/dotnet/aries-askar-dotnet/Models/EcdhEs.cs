using aries_askar_dotnet.AriesAskar;
using System;
using System.Text;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

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
        /// <param name="ecdhEs">The <see cref="EcdhEs"/> instance.</param>
        /// <param name="keyAlg"></param>
        /// <param name="ephemeralKey"></param>
        /// <param name="receiverKey"></param>
        /// <param name="receive"></param>
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
                keyAlg.ToKeyAlgString(),
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
        /// <param name="ecdhEs">The <see cref="EcdhEs"/> instance.</param>
        /// <param name="encKeyAlg">The encoding key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">A ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The message to encrypt as <see cref="string"/>.</param>
        /// <param name="nonce">A nonce as <see cref="byte"/> array.</param>
        /// <param name="aad">The associated data as <see cref="string"/>.</param>
        /// <returns>The ciphertext, nonce and tag as <see cref="byte"/> arrays.</returns>
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
        /// <param name="ecdhEs">The <see cref="EcdhEs"/> instance.</param>
        /// <param name="encKeyAlg">The encoding key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">A ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="ciphertext"></param>
        /// <param name="nonce"></param>
        /// <param name="tag"></param>
        /// <param name="aad">The associated data as <see cref="string"/>.</param>
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
            UTF8Encoding Decoder = new(true, true);
            IntPtr derivedKey = await ecdhEs.DeriveKeyAsync(encKeyAlg, ephemeralKey, receiverKey, true);
            return Decoder.GetString(await KeyApi.DecryptKeyWithAeadAsync(derivedKey, ciphertext, nonce, tag, aad));
        }

        /// <summary>
        /// Wrap another key using a ECDH-ES key derivation of the receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdhEs">The <see cref="EcdhEs"/> instance.</param>
        /// <param name="wrapKeyAlg">The wrapping key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">A ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="cek">A content-encryption key as <see cref="IntPtr"/>.</param>
        /// <returns>The ciphertext, nonce and tag as <see cref="byte"/> arrays.</returns>
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
        /// <param name="ecdhEs">The <see cref="EcdhEs"/> instance.</param>
        /// <param name="wrapKeyAlg">The wrapping key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="encKeyAlg">The encoding key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">A ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="ciphertext"></param>
        /// <param name="nonce"></param>
        /// <param name="tag"></param>
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
                encKeyAlg.ToKeyAlgString(), 
                ciphertext, 
                nonce, 
                tag);
        }
    }
}
