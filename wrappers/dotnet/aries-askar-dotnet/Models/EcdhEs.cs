using aries_askar_dotnet.AriesAskar;
using System;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet.Models
{
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

    public static class EcdhEsExtensions
    {
        //Returns a keyHandle
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

        public static async Task<byte[]> DecryptDirectAsync(
            this EcdhEs ecdhEs,
            KeyAlg encKeyAlg,
            IntPtr ephemeralKey,
            IntPtr receiverKey,
            byte[] ciphertext,
            byte[] nonce,
            byte[] tag,
            string aad = null)
        {
            IntPtr derivedKey = await ecdhEs.DeriveKeyAsync(encKeyAlg, ephemeralKey, receiverKey, true);
            return await KeyApi.DecryptKeyWithAeadAsync(derivedKey, ciphertext, nonce, tag, aad);
        }

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

        //Returns a keyHandle
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
