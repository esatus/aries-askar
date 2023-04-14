namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converts the value of SeedMethod to the corresponding string representation for the backend.
    /// </summary>
    public static class SeedMethodConverter
    {
        public static string ToSeedMethodString(this SeedMethod keyAlg)
        {
            switch (keyAlg)
            {
                case SeedMethod.BlsKeyGen:
                    return "bls_keygen";
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Supported seed methods.
    /// </summary>
    public enum SeedMethod
    {
        BlsKeyGen
    }
}