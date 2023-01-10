using System;

namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converter for <see cref ="KeyMethod"/> and its corresponding string representation for the backend.
    /// </summary>
    public static class KeyMethodConverter
    {
        /// <summary>
        /// Converts the value of <see cref ="KeyMethod"/> to the corresponding string representation for the backend.
        /// </summary>
        public static string ToKeyMethodString(this KeyMethod keyMethod)
        {
            return keyMethod switch
            {
                KeyMethod.KDF_ARGON2I => "kdf:argon2i",
                KeyMethod.RAW => "raw",
                KeyMethod.NONE => "none",
                _ => null,
            };
        }

        /// <summary>
        /// Converts the string representation to the value of <see cref ="KeyMethod"/>.
        /// </summary>
        /// <param name="keyMethodString">string representation of the key method.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Throws when <paramref name="keyMethodString"/> is invalid key method.</exception>
        public static KeyMethod ToKeyMethod(string keyMethodString)
        {
            keyMethodString = keyMethodString.ToLower();
            return keyMethodString switch
            {
                "kdf:argon2i" => KeyMethod.KDF_ARGON2I,
                "raw" => KeyMethod.RAW,
                "none" => KeyMethod.NONE,
                _ => throw new ArgumentException($"Invalid argument provided for keyMethod string: {keyMethodString}"),
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