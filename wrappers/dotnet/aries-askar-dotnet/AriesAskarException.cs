using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace aries_askar_dotnet
{
    public class AriesAskarException : Exception
    {
        public AriesAskarException(string message) : base(message)
        {
        }

        public AriesAskarException(string message, Exception inner) : base(message, inner)
        {
        }

        public static AriesAskarException FromSdkError(string message)
        {
            string msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(message)["message"];
            string errCode = JsonConvert.DeserializeObject<Dictionary<string, string>>(message)["code"];
            int errCodeInt;
            if (int.TryParse(errCode, out errCodeInt))
            {
                return new AriesAskarException(
                    $"'{ErrorCodeConverter.ToErrorCodeString((ErrorCode)errCodeInt)}' error occured with ErrorCode '{errCode}' : {msg}.");
            }
            else
            {
                return new AriesAskarException("An unknown error code was received.");
            }
        }
    }
}
