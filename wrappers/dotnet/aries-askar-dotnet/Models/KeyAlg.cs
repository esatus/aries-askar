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
            }
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
            switch (keyAlgString)
            {
                case "a128gcm": return KeyAlg.A128GCM;
                case "a256gcm": return KeyAlg.A256GCM;
                case "a128cbchs256": return KeyAlg.A128CBC_HS256;
                case "a256cbchs512": return KeyAlg.A256CBC_HS512;
                case "a128kw": return KeyAlg.A128KW;
                case "a256kw": return KeyAlg.A256KW;
                case "bls12381g1": return KeyAlg.BLS12_381_G1;
                case "bls12381g2": return KeyAlg.BLS12_381_G2;
                case "bls12381g1g2": return KeyAlg.BLS12_381_G1G2;
                case "c20p": return KeyAlg.C20P;
                case "xc20p": return KeyAlg.XC20P;
                case "ed25519": return KeyAlg.ED25519;
                case "x25519": return KeyAlg.X25519;
                case "k256": return KeyAlg.K256;
                case "p256": return KeyAlg.P256;
                default: throw new ArgumentException($"Invalid argument provided for keyAlg string: {keyAlgString}");
            }
        }

        /// <summary>
        /// Converts the value of <see cref ="KeyAlg"/> to the corresponding string representation for the backend.
        /// </summary>
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
            }
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
            switch (keyAlgString)
            {
                case "A128GCM": return KeyAlg.A128GCM;
                case "A256GCM": return KeyAlg.A256GCM;
                case "A128CBC_HS256": return KeyAlg.A128CBC_HS256;
                case "A256CBC_HS512": return KeyAlg.A256CBC_HS512;
                case "A128KW": return KeyAlg.A128KW;
                case "A256KW": return KeyAlg.A256KW;
                case "BLS12381_G1": return KeyAlg.BLS12_381_G1;
                case "BLS12381_G2": return KeyAlg.BLS12_381_G2;
                case "BLS12381_G1G2": return KeyAlg.BLS12_381_G1G2;
                case "C20P": return KeyAlg.C20P;
                case "XC20P": return KeyAlg.XC20P;
                case "ED25519": return KeyAlg.ED25519;
                case "X25519": return KeyAlg.X25519;
                case "K256": return KeyAlg.K256;
                case "P256": return KeyAlg.P256;
                default: throw new System.ArgumentException($"Invalid argument provided for keyAlg string: {keyAlgString}");
            }
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