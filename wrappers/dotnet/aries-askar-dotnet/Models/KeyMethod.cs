namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converts the value of KeyMethod to the corresponding string representation for the backend.
    /// </summary>
    public static class KeyMethodConverter
    {
        public static string ToKeyMethodString(this KeyMethod keyMethod)
        {
            return keyMethod switch
            {
                KeyMethod.KDF_ARGON2I => "kdf:argon2i",
                KeyMethod.RAW => "raw",
                KeyMethod.NONE => "none",
                _ => null
            };
        }
    }
    /// <summary>
    /// Supported methods for generating or referencing a new store key
    /// </summary>
    public enum KeyMethod
    {
        /// Derive a new wrapping key using a key derivation function
        KDF_ARGON2I,
        /// Wrap using an externally-managed raw key
        RAW,
        /// No wrapping key in effect
        NONE
    }
}