using System;

namespace aries_askar_dotnet.Models
{
    public class Session
    {
        public IntPtr storeHandle { get; set; }
        public IntPtr sessionHandle { get; set; }
        public string sessionProfile { get; set; }
        public bool isTransaction { get; set; }

        public Session(IntPtr stoHandle, IntPtr sessHandle, string profile, bool isTxn)
        {
            storeHandle = stoHandle;
            sessionHandle = sessHandle;
            sessionProfile = profile;
            isTransaction = isTxn;
        }
    }
}
