namespace aries_askar_dotnet
{
    /// <summary>
    /// Converts the value of ErrorCode to the corresponding string representation for the backend.
    /// </summary>
    public static class ErrorCodeConverter
    {
        public static string ToErrorCodeString(this ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.Success:
                    return "Success";
                case ErrorCode.Backend:
                    return "Backend";
                case ErrorCode.Busy:
                    return "Busy";
                case ErrorCode.Duplicate:
                    return "Duplicate";
                case ErrorCode.Encryption:
                    return "Encryption";
                case ErrorCode.Input:
                    return "Input";
                case ErrorCode.NotFound:
                    return "NotFound";
                case ErrorCode.Unexpected:
                    return "Unexpected";
                case ErrorCode.Unsupported:
                    return "Unsupported";
                case ErrorCode.Wrapper:
                    return "Wrapper";
                case ErrorCode.Custom:
                    return "Custom";
                default:
                    return "Unknown error code";
            }
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