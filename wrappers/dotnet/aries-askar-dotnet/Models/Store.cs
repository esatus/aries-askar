using System;

namespace aries_askar_dotnet.Models
{
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
