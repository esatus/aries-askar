namespace aries_askar_dotnet.Models
{
    public class KeyEntry
    {
        public uint keyEntryHandle { get; set; }
        public long pos { get; set; }

        public KeyEntry(uint handle, long index)
        {
            keyEntryHandle = handle;
            pos = index;
        }
    }
}
