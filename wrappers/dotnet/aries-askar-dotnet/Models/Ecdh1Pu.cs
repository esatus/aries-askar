using aries_askar_dotnet.AriesAskar;
using System;
using System.Text;
using System.Threading.Tasks;
using static aries_askar_dotnet.Models.Structures;

namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// An instantiation of the ECDH-1PU key derivation
    /// </summary>
    public class Ecdh1Pu
    {
        public string AlgId { get; set; }
        public string Apu { get; set; }
        public string Apv { get; set; }

        public Ecdh1Pu(string algId, string apu, string apv)
        {
            AlgId = algId;
            Apu = apu;
            Apv = apv;
        }
    }

    /// <summary>
    /// Extension methods for ECDH-1PU class. Contains direct encryption/decryption of messages and wrapping/unwrapping of a key. 
    /// </summary>
    public static class Ecdh1PUExtensions
    {
        /// <summary>
        /// Derive an ECDH-1PU shared key for authenticated encryption
        /// </summary>
        /// <param name="ecdh1Pu">The <see cref="Ecdh1Pu"/> instance.</param>
        /// <param name="keyAlg">The key algorithm of the key as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">The ephemeral key handle as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The sender key handle as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key handle as <see cref="IntPtr"/>.</param>
        /// <param name="receive">The receive flag as <see cref="bool"/>, indicating receive or send. True for receive, false for send.</param>
        /// <param name="ccTag">The ccTag as <see cref="byte"/>[]; default null.</param>
        /// <returns>A new key handle as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> DeriveKeyAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg keyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            bool receive,
            byte[] ccTag = null)
        {
            return await KeyApi.DeriveEcdh1puAsync(
                keyAlg,
                ephemeralKey,
                senderKey,
                receiverKey,
                ByteBuffer.Create(ecdh1Pu.AlgId).Decode(),
                ByteBuffer.Create(ecdh1Pu.Apu).Decode(),
                ByteBuffer.Create(ecdh1Pu.Apv).Decode(),
                ccTag,
                Convert.ToByte(receive));
        }

        /// <summary>
        /// Perform a AEAD message encryption with a ECDH-1PU key derivation of the sender, receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdh1Pu">The <see cref="Ecdh1Pu"/> instance.</param>
        /// <param name="encKeyAlg">The encoding key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">A ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The sender key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="message">The message to encrypt as <see cref="string"/>.</param>
        /// <param name="nonce">A nonce as <see cref="byte"/> array.</param>
        /// <param name="aad">The associated data as <see cref="string"/>.</param>
        /// <returns>The triple of ciphertext (first), tag(second) and nonce(third) as <see cref="byte"/>[].</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<(byte[], byte[], byte[])> EncryptDirectAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            string message,
            byte[] nonce = null,
            string aad = null)
        {
            IntPtr derivedKey = await ecdh1Pu.DeriveKeyAsync(encKeyAlg, ephemeralKey, senderKey, receiverKey, false, null);
            (byte[] ciphertext, byte[] tagBytes, byte[] nonceBytes) = await KeyApi.EncryptKeyWithAeadAsync(derivedKey, message, nonce, aad);
            return (ciphertext, tagBytes, nonceBytes);
        }

        /// <summary>
        /// Perform a AEAD message decryption with a ECDH-1PU key derivation of the sender, receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdh1Pu">The <see cref="Ecdh1Pu"/> instance.</param>
        /// <param name="encKeyAlg">The encoding key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">A ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The sender key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="ciphertext">The encryption ciphertext as <see cref="byte"/>[].</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte"/>[].</param>
        /// <param name="tag">The tag ciphertext as <see cref="byte"/>[].</param>
        /// <param name="aad">The associated data as <see cref="string"/>.</param>
        /// <returns>The decrypted message as <see cref="string"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<string> DecryptDirectAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            byte[] ciphertext,
            byte[] nonce,
            byte[] tag,
            string aad = null)
        {
            UTF8Encoding Decoder = new UTF8Encoding(true, true);
            IntPtr derivedKey = await ecdh1Pu.DeriveKeyAsync(encKeyAlg, ephemeralKey, senderKey, receiverKey, true, null);
            return Decoder.GetString(await KeyApi.DecryptKeyWithAeadAsync(derivedKey, ciphertext, nonce, tag, aad));
        }

        /// <summary>
        /// Wrap another key using a ECDH-1PU key derivation of the sender, receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdh1Pu">The <see cref="Ecdh1Pu"/> instance.</param>
        /// <param name="wrapKeyAlg">The wrapping key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">An ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The sender key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="cek">A content-encryption key as <see cref="IntPtr"/>.</param>
        /// <param name="ccTag">A ccTag as <see cref="byte"/> array; default null.</param>
        /// <returns>The triple of ciphertext (first), tag(second) and nonce(third) as <see cref="byte"/>[].</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<(byte[], byte[], byte[])> SenderWrapKeyAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg wrapKeyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            IntPtr cek,
            byte[] ccTag)
        {
            IntPtr derivedKey = await ecdh1Pu.DeriveKeyAsync(wrapKeyAlg, ephemeralKey, senderKey, receiverKey, false, ccTag);
            (byte[] ciphertext, byte[] tagBytes, byte[] nonceBytes) = await KeyApi.WrapKeyAsync(derivedKey, cek, null);
            return (ciphertext, tagBytes, nonceBytes);
        }

        /// <summary>
        /// Unwrap another key using a ECDH-1PU key derivation of the sender, receiver and ephemeral key.
        /// </summary>
        /// <param name="ecdh1Pu">The <see cref="Ecdh1Pu"/> instance.</param>
        /// <param name="wrapKeyAlg">The wrapping key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="encKeyAlg">The encoding key algorithm as <see cref="KeyAlg"/>.</param>
        /// <param name="ephemeralKey">An ephemeral key as <see cref="IntPtr"/>.</param>
        /// <param name="senderKey">The sender key as <see cref="IntPtr"/>.</param>
        /// <param name="receiverKey">The receiver key as <see cref="IntPtr"/>.</param>
        /// <param name="ccTag">A ccTag as <see cref="byte"/> array; default null.</param>
        /// <param name="ciphertext">The encryption ciphertext as <see cref="byte"/>[].</param>
        /// <param name="nonce">The encryption nonce as <see cref="byte"/>[].</param>
        /// <param name="tag">The tag ciphertext as <see cref="byte"/>[].</param>
        /// <returns>The key handle as <see cref="IntPtr"/>.</returns>
        /// <exception cref="AriesAskarException">Throws a AriesAskarException with corresponding error code from the sdk, when providing invalid input parameter. 
        /// </exception>
        public static async Task<IntPtr> ReceiverUnwrapKeyAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg wrapKeyAlg,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            byte[] ccTag,
            byte[] ciphertext,
            byte[] nonce = null,
            byte[] tag = null)
        {
            IntPtr derivedKey = await ecdh1Pu.DeriveKeyAsync(wrapKeyAlg, ephemeralKey, senderKey, receiverKey, true, ccTag);
            return await KeyApi.UnwrapKeyAsync(
                derivedKey,
                encKeyAlg,
                ciphertext,
                nonce,
                tag);
        }
    }
}