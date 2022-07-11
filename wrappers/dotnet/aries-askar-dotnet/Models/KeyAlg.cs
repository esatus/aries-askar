namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converts the value of KeyAlg to the corresponding string representation for the backend.
    /// </summary>
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
                _ => null
            };
        }

        public static string ToJwkCrvString(this KeyAlg keyAlg)
        {
            return keyAlg switch
            {
                KeyAlg.A128GCM => "A128GCM",
                KeyAlg.A256GCM => "A256GCM",
                KeyAlg.A128CBC_HS256 => "A128CBC_HS256",
                KeyAlg.A256CBC_HS512 => "A256CBC_HS512",
                KeyAlg.A128KW => "A128KW",
                KeyAlg.A256KW => "A256KW",
                KeyAlg.BLS12_381_G1 => "BLS12381_G1",
                KeyAlg.BLS12_381_G2 => "BLS12381_G2",
                KeyAlg.BLS12_381_G1G2 => "BLS12381_G1G2",
                KeyAlg.C20P => "C20P",
                KeyAlg.XC20P => "XC20P",
                KeyAlg.ED25519 => "ED25519",
                KeyAlg.X25519 => "X25519",
                KeyAlg.K256 => "K256",
                KeyAlg.P256 => "P256",
                _ => null
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
