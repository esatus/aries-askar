﻿using System;

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
            switch (sigType)
            {
                case SignatureType.EdDSA: return "EDDSA";
                case SignatureType.ES256: return "ES256";
                case SignatureType.ES256K: return "ES256K";
                default: return null;
            }
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
            switch (sigTypeString)
            {
                case "EDDSA": return SignatureType.EdDSA;
                case "ES256": return SignatureType.ES256;
                case "ES256K": return SignatureType.ES256K;
                default: throw new ArgumentException($"Invalid argument provided for signatureType string: {sigTypeString}");
            }
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