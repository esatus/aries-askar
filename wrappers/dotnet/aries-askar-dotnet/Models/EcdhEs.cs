using aries_askar_dotnet.aries_askar;
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
        public static async Task<IntPtr> DeriveKey(this EcdhEs ecdhEs, KeyAlg keyAlg, IntPtr ephemeralKey, IntPtr receiverKey, bool receive)
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
    }
}
