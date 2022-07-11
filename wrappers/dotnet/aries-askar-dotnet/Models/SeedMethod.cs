namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Converts the value of SeedMethod to the corresponding string representation for the backend.
    /// </summary>
    public static class SeedMethodConverter
    {
        public static string ToSeedMethodString(this SeedMethod keyAlg)
        {
            return keyAlg switch
            {
                SeedMethod.BlsKeyGen => "bls_keygen",
                _ => null
            };
        }
    }

    public enum SeedMethod
    {
        BlsKeyGen
    }
}
