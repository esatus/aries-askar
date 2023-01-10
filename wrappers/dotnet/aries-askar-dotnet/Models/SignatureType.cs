using System;

namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converter for <see cref ="SignatureType"/> and its corresponding string representation for the backend.
    /// </summary>
    public static class SignatureTypeConverter
    {
        /// <summary>
        /// Converts the value of <see cref ="SignatureType"/> to the corresponding string representation for the backend.
        /// </summary>
        public static string ToSigTypeString(this SignatureType sigType)
        {
            return sigType switch
            {
                SignatureType.EdDSA => "EDDSA",
                SignatureType.ES256 => "ES256",
                SignatureType.ES256K => "ES256K",
                _ => null,
            };
        }

        /// <summary>
        /// Converts the string representation to the value of <see cref ="SignatureType"/>.
        /// </summary>
        /// <param name="sigTypeString">string representation of the signature type.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Throws when <paramref name="sigTypeString"/> is invalid signature type.</exception>
        public static SignatureType ToSigType(string sigTypeString)
        {
            sigTypeString = sigTypeString.ToUpper();
            return sigTypeString switch
            {
                "EDDSA" => SignatureType.EdDSA,
                "ES256" => SignatureType.ES256,
                "ES256K" => SignatureType.ES256K,
                _ => throw new ArgumentException($"Invalid argument provided for signatureType string: {sigTypeString}"),
            };
        }
    }
    /// <summary>
    /// Supported signature types.
    /// </summary>
    public enum SignatureType
    {
        /// Standard signature output for ed25519
        EdDSA,
        /// Elliptic curve DSA using P-256 and SHA-256
        ES256,
        /// Elliptic curve DSA using K-256 and SHA-256
        ES256K,
    }
}