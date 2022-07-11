namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converts the value of SignatureType to the corresponding string representation for the backend.
    /// </summary>
    public static class SignatureTypeConverter
    {
        public static string ToSigTypeString(this SignatureType sigType)
        {
            return sigType switch
            {
                SignatureType.EdDSA => "EDDSA",
                SignatureType.ES256 => "ES256",
                SignatureType.ES256K => "ES256K",
                _ => null
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
