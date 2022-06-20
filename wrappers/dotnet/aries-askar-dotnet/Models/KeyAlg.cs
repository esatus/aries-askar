namespace aries_askar_dotnet.Models
{
    public static class KeyAlgConverter
    {
        public static string ToKeyAlgString(this KeyAlg keyAlg)
        {
            return keyAlg switch
            {
                KeyAlg.A128GCM => "a128gcm",
                KeyAlg.A256GCM => "a256gcm",
                KeyAlg.A128CBC_HS256 => "a128cbchs256",
                KeyAlg.A256CBC_HS512 => "a256cbchs512",
                KeyAlg.A128KW => "a128kw",
                KeyAlg.A256KW => "a256kw",
                KeyAlg.BLS12_381_G1 => "bls12381g1",
                KeyAlg.BLS12_381_G2 => "bls12381g2",
                KeyAlg.BLS12_381_G1G2 => "bls12381g1g2",
                KeyAlg.C20P => "c20p",
                KeyAlg.XC20P => "xc20p",
                KeyAlg.ED25519 => "ed25519",
                KeyAlg.X25519 => "x25519",
                KeyAlg.K256 => "k256",
                KeyAlg.P256 => "p256",
                _ => null,
            };
        }
    }

    public enum KeyAlg
    {
        A128GCM,
        A256GCM,
        A128CBC_HS256,
        A256CBC_HS512,
        A128KW,
        A256KW,
        BLS12_381_G1,
        BLS12_381_G2,
        BLS12_381_G1G2,
        C20P,
        XC20P,
        ED25519,
        X25519,
        K256,
        P256
    }
}
