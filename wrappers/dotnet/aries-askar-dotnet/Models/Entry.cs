using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aries_askar_dotnet.Models
{
    public class Entry
    {
        public uint entryHandle {get;set;}

        public Entry(uint handle)
        {
            entryHandle = handle;
        }
    }
}
