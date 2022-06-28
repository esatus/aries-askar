using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aries_askar_dotnet.Models
{
    public class EntryList
    {
        public uint entryListHandle { get; set; }

        public EntryList(uint handle)
        {
            entryListHandle = handle;
        }
    }
}
