using System;

namespace aries_askar_dotnet.Models
{
    public class KeyEntryList
    {
        public IntPtr keyEntryListHandle { get; set; }

        public KeyEntryList(IntPtr handle)
        {
            keyEntryListHandle = handle;
        }
    }
}
