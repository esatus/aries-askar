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
    }

    public static class EcdhEsExtensions
    {
        public static async Task<IntPtr> DeriveKeyAsync(this EcdhEs ecdhEs, KeyAlg keyAlg, IntPtr ephemeralKey, IntPtr receiverKey, bool receive)
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

        public static async Task<EncryptedBuffer> SenderWrapKeyAsync(this EcdhEs ecdhEs, KeyAlg keyAlg, IntPtr ephemeralKey, IntPtr receiverKey, IntPtr otherKey, byte[] nonce = null)
        {
            IntPtr derived = await ecdhEs.DeriveKeyAsync(keyAlg, ephemeralKey, receiverKey, false);
             (byte[] algId, byte[] apu, byte[] apv) = await KeyApi.WrapKeyAsync(derived, otherKey, nonce);
            /**
            EncryptedBuffer encBuffer = new();
            ecdhEs.AlgId = ByteBuffer.Create(algId).DecodeToString();
            ecdhEs.Apu = ByteBuffer.Create(apu).DecodeToString();
            ecdhEs.Apv = ByteBuffer.Create(apv).DecodeToString();
            return await encBuffer.Decode(algId, ErrorApi, apv);**/
            return new EncryptedBuffer();
        }
    }
}
