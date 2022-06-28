namespace aries_askar_dotnet.Models
{
    public class KeyEntryList
    {
        public uint keyEntryListHandle { get; set; }

        public KeyEntryList(uint handle)
        {
            keyEntryListHandle = handle;
        }
    }
}
