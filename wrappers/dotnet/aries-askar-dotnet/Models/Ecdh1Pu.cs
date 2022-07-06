using aries_askar_dotnet.AriesAskar;
using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.Models
{
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

    public static class Ecdh1PUExtensions
    {
        //Returns a keyHandle
        public static async Task<IntPtr> DeriveKeyAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg keyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            bool receive,
            string ccTag = null)
        {
            return await KeyApi.DeriveEcdh1puAsync(
                keyAlg.ToKeyAlgString(),
                ephemeralKey,
                senderKey,
                receiverKey,
                ByteBuffer.Create(ecdh1Pu.AlgId).Decode(),
                ByteBuffer.Create(ecdh1Pu.Apu).Decode(),
                ByteBuffer.Create(ecdh1Pu.Apv).Decode(),
                ccTag,
                Convert.ToByte(receive));
        }

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

        public static async Task<byte[]> DecryptDirectAsync(
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
            IntPtr derivedKey = await ecdh1Pu.DeriveKeyAsync(encKeyAlg, ephemeralKey, senderKey, receiverKey, true, null);
            return await KeyApi.DecryptKeyWithAeadAsync(derivedKey, ciphertext, nonce, tag, aad);
        }

        public static async Task<(byte[], byte[], byte[])> SenderWrapKeyAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg wrapKeyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            IntPtr cek,
            string ccTag,
            byte[] nonce = null)
        {
            IntPtr derivedKey = await ecdh1Pu.DeriveKeyAsync(wrapKeyAlg, ephemeralKey, senderKey, receiverKey, false, ccTag);
            (byte[] ciphertext, byte[] tagBytes, byte[] nonceBytes) = await KeyApi.WrapKeyAsync(derivedKey, cek, nonce);
            return (ciphertext, tagBytes, nonceBytes);
        }

        //Returns a keyHandle
        public static async Task<IntPtr> ReceiverUnwrapKeyAsync(
            this Ecdh1Pu ecdh1Pu,
            KeyAlg wrapKeyAlg,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr senderKey,
            IntPtr receiverKey,
            string ccTag,
            byte[] ciphertext,
            byte[] nonce = null,
            byte[] tag = null)
        {
            IntPtr derivedKey = await ecdh1Pu.DeriveKeyAsync(wrapKeyAlg, ephemeralKey, senderKey, receiverKey, true, ccTag);
            return await KeyApi.UnwrapKeyAsync(
                derivedKey,
                encKeyAlg.ToKeyAlgString(),
                ciphertext,
                nonce,
                tag);
        }
    }
}
