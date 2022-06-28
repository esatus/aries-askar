using System;

namespace aries_askar_dotnet.Models
{
    public class KeyEntry
    {
        public IntPtr keyEntryHandle { get; set; }
        public long pos { get; set; }

        public KeyEntry(IntPtr handle, long index)
        {
            keyEntryHandle = handle;
            pos = index;
        }
    }
}
