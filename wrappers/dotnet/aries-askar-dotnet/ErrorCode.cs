namespace aries_askar_dotnet
{
    /// <summary>
    /// Converts the value of ErrorCode to the corresponding string representation for the backend.
    /// </summary>
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
                ErrorCode.Wrapper => "Wrapper",
                ErrorCode.Custom => "Custom",
                _ => "Unknown error code"
            };
        }
    }

    /// <summary>
    /// The error codes defined in the backend.
    /// </summary>
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
        Wrapper = 99,
        Custom = 100,
    }
}