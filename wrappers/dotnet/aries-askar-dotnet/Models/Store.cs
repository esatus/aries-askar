using System;

namespace aries_askar_dotnet.Models
{
    /// <summary>
    /// Store instance for holding the store handle from the backend. Also holds the specUri of the store and a session instance.
    /// </summary>
    public class Store
    {
        public IntPtr storeHandle { get; set; }
        public Session session { get; set; } = null;
        public string specUri { get; set; }

        public Store(IntPtr handle, string uri)
        {
            storeHandle = handle;
            specUri = uri;
        }
    }
}