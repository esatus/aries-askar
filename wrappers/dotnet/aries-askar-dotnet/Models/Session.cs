using System;

namespace aries_askar_dotnet.Models
{
    public class Session
    {
        public uint storeHandle { get; set; }
        public uint sessionHandle { get; set; }
        public string sessionProfile { get; set; }
        public bool isTransaction { get; set; }

        public Session(uint stoHandle, uint sessHandle, string profile, bool isTxn)
        {
            storeHandle = stoHandle;
            sessionHandle = sessHandle;
            sessionProfile = profile;
            isTransaction = isTxn;
        }
    }
}
