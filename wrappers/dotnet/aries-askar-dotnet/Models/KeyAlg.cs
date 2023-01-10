using System;

namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converter for <see cref ="KeyAlg"/> and its corresponding string representation for the backend.
    /// </summary>
    public static class KeyAlgConverter
    {
        /// <summary>
        /// Converts the value of <see cref ="KeyAlg"/> to the corresponding string representation for the backend.
        /// </summary>
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

        /// <summary>
        /// Converts the string representation to the value of <see cref ="KeyAlg"/>.
        /// </summary>
        /// <param name="keyAlgString">string representation of the key algorithm.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Throws when <paramref name="keyAlgString"/> is invalid key algorithm.</exception>
        public static KeyAlg ToKeyAlg(string keyAlgString)
        {
            keyAlgString = keyAlgString.ToLower();
            return keyAlgString switch
            {
                "a128gcm" => KeyAlg.A128GCM,
                "a256gcm" => KeyAlg.A256GCM,
                "a128cbchs256" => KeyAlg.A128CBC_HS256,
                "a256cbchs512" => KeyAlg.A256CBC_HS512,
                "a128kw" => KeyAlg.A128KW,
                "a256kw" => KeyAlg.A256KW,
                "bls12381g1" => KeyAlg.BLS12_381_G1,
                "bls12381g2" => KeyAlg.BLS12_381_G2,
                "bls12381g1g2" => KeyAlg.BLS12_381_G1G2,
                "c20p" => KeyAlg.C20P,
                "xc20p" => KeyAlg.XC20P,
                "ed25519" => KeyAlg.ED25519,
                "x25519" => KeyAlg.X25519,
                "k256" => KeyAlg.K256,
                "p256" => KeyAlg.P256,
                _ => throw new ArgumentException($"Invalid argument provided for keyAlg string: {keyAlgString}"),
            };
        }

        /// <summary>
        /// Converts the value of <see cref ="KeyAlg"/> to the corresponding string representation for the backend.
        /// </summary>
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
                _ => null,
            };
        }

        /// <summary>
        /// Converts the string representation to the value of <see cref ="KeyAlg"/>.
        /// </summary>
        /// <param name="keyAlgString">string representation of the key algorithm.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Throws when <paramref name="keyAlgString"/> is invalid key algorithm.</exception>
        public static KeyAlg ToKeyAlgJwkCrv(string keyAlgString)
        {
            keyAlgString = keyAlgString.ToUpper();
            return keyAlgString switch
            {
                "A128GCM" => KeyAlg.A128GCM,
                "A256GCM" => KeyAlg.A256GCM,
                "A128CBC_HS256" => KeyAlg.A128CBC_HS256,
                "A256CBC_HS512" => KeyAlg.A256CBC_HS512,
                "A128KW" => KeyAlg.A128KW,
                "A256KW" => KeyAlg.A256KW,
                "BLS12381_G1" => KeyAlg.BLS12_381_G1,
                "BLS12381_G2" => KeyAlg.BLS12_381_G2,
                "BLS12381_G1G2" => KeyAlg.BLS12_381_G1G2,
                "C20P" => KeyAlg.C20P,
                "XC20P" => KeyAlg.XC20P,
                "ED25519" => KeyAlg.ED25519,
                "X25519" => KeyAlg.X25519,
                "K256" => KeyAlg.K256,
                "P256" => KeyAlg.P256,
                _ => throw new System.ArgumentException($"Invalid argument provided for keyAlg string: {keyAlgString}"),
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