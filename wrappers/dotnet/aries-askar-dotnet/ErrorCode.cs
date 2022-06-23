namespace aries_askar_dotnet
{
    public static class ErrorCodeConverter
    {
        public static string ToErrorCodeString(this ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.Success => "Success",
                ErrorCode.Backend => "Backend",
                ErrorCode.Busy => "Busy",
                ErrorCode.Duplicate => "Duplicate",
                ErrorCode.Encryption => "Encryption",
                ErrorCode.Input => "Input",
                ErrorCode.NotFound => "NotFound",
                ErrorCode.Unexpected => "Unexpected",
                ErrorCode.Unsupported => "Unsupported",
                ErrorCode.Custom => "Custom",
                _ => "Unknown error code"
            };
        }
    }
    public enum ErrorCode
    {
        Success = 0,
        Backend = 1,
        Busy = 2,
        Duplicate = 3,
        Encryption = 4,
        Input = 5,
        NotFound = 6,
        Unexpected = 7,
        Unsupported = 8,
        Custom = 100,
    }
}
