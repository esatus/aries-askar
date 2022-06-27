namespace aries_askar_dotnet.Models
{
    public class Store
    {
        public uint storeHandle { get; set; }
        public Session session { get; set; } = null;
        public string specUri { get; set; }

        public Store(uint handle, string uri)
        {
            storeHandle = handle;
            specUri = uri;
        }
    }
}
