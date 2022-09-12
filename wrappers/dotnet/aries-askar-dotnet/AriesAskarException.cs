using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace aries_askar_dotnet
{
    public class AriesAskarException : Exception
    {
        public ErrorCode errorCode;

        public AriesAskarException(string message, ErrorCode code) : base(message)
        {
            errorCode = code;
        }

        public AriesAskarException(string message, Exception inner) : base(message, inner)
        {
        }

        public static AriesAskarException FromWrapperError(ErrorCode errorCode, string message)
        {
            return new AriesAskarException($"'{errorCode.ToErrorCodeString()}' error occured with ErrorCode '{(int)errorCode}' : {message}.", errorCode);
        }

        public static AriesAskarException FromSdkError(string message)
        {
            string msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(message)["message"];
            string errCode = JsonConvert.DeserializeObject<Dictionary<string, string>>(message)["code"];
            return int.TryParse(errCode, out int errCodeInt)
                ? new AriesAskarException(
                    $"'{((ErrorCode)errCodeInt).ToErrorCodeString()}' error occured with ErrorCode '{errCode}' : {msg}.", (ErrorCode)errCodeInt)
                : new AriesAskarException("An unknown error code was received.", (ErrorCode)errCodeInt);
        }
    }
}