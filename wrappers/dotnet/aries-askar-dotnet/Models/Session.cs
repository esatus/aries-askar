using System;

namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Session instance for holding the session handle, store handle from the backend. Also holds the session profile and whether it's a session or transaction.
    /// </summary>
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
