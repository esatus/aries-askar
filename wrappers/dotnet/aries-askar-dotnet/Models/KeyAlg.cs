namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converts the value of KeyAlg to the corresponding string representation for the backend.
    /// </summary>
    public static class KeyAlgConverter
    {
        public static string ToKeyAlgString(this KeyAlg keyAlg)
        {
            switch (keyAlg)
            {
                case KeyAlg.A128GCM: return "a128gcm";
                case KeyAlg.A256GCM: return "a256gcm";
                case KeyAlg.A128CBC_HS256: return "a128cbchs256";
                case KeyAlg.A256CBC_HS512: return "a256cbchs512";
                case KeyAlg.A128KW: return "a128kw";
                case KeyAlg.A256KW: return "a256kw";
                case KeyAlg.BLS12_381_G1: return "bls12381g1";
                case KeyAlg.BLS12_381_G2: return "bls12381g2";
                case KeyAlg.BLS12_381_G1G2: return "bls12381g1g2";
                case KeyAlg.C20P: return "c20p";
                case KeyAlg.XC20P: return "xc20p";
                case KeyAlg.ED25519: return "ed25519";
                case KeyAlg.X25519: return "x25519";
                case KeyAlg.K256: return "k256";
                case KeyAlg.P256: return "p256";
                default: return null;
            };
        }

        public static string ToJwkCrvString(this KeyAlg keyAlg)
        {
            switch (keyAlg)
            {
                case KeyAlg.A128GCM: return "A128GCM";
                case KeyAlg.A256GCM: return "A256GCM";
                case KeyAlg.A128CBC_HS256: return "A128CBC_HS256";
                case KeyAlg.A256CBC_HS512: return "A256CBC_HS512";
                case KeyAlg.A128KW: return "A128KW";
                case KeyAlg.A256KW: return "A256KW";
                case KeyAlg.BLS12_381_G1: return "BLS12381_G1";
                case KeyAlg.BLS12_381_G2: return "BLS12381_G2";
                case KeyAlg.BLS12_381_G1G2: return "BLS12381_G1G2";
                case KeyAlg.C20P: return "C20P";
                case KeyAlg.XC20P: return "XC20P";
                case KeyAlg.ED25519: return "ED25519";
                case KeyAlg.X25519: return "X25519";
                case KeyAlg.K256: return "K256";
                case KeyAlg.P256: return "P256";
                default: return null;
            };
        }
    }

    /// <summary>
    /// Supported key algorithms.
    /// </summary>
    public enum KeyAlg
    {
        /// AES
        A128GCM,
        A256GCM,
        A128CBC_HS256,
        A256CBC_HS512,
        A128KW,
        A256KW,
        /// BLS12-381
        BLS12_381_G1,
        BLS12_381_G2,
        BLS12_381_G1G2,
        /// (X)ChaCha20-Poly1305
        C20P,
        XC20P,
        /// Curve25519 signing key
        ED25519,
        /// Curve25519 diffie-hellman key exchange key
        X25519,
        /// Elliptic Curve key for signing or key exchange
        K256,
        P256,
        /// None
        NONE = -1
    }
}