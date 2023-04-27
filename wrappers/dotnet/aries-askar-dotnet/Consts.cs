namespace aries_askar_dotnet
{
    public static class Consts
    {
#if __IOS__
        public const string ARIES_ASKAR_LIB_NAME = "__Internal";
#else
        public const string ARIES_ASKAR_LIB_NAME = "aries_askar";
#endif
    }
}