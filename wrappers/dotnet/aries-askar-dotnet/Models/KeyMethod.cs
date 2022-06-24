namespace aries_askar_dotnet.Models
{
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

    public enum KeyMethod
    {
        KDF_ARGON2I,
        RAW,
        NONE
    }
}
