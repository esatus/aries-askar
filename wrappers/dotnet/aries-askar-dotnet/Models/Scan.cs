using System.Collections.Generic;

namespace aries_askar_dotnet.Models
{
    public class Scan
    {
        public uint scanHandle { get; set; }
        public uint storeHandle { get; set; }
        public List<object> parameters { get; set; }
        //public IterEntryList buffer { get; set; }

        public Scan(uint handle, uint stohandle, List<object> parameterList) //IterEntryList entryList, List<string> parameterList)
        {
            scanHandle = handle;
            storeHandle = stohandle;
            parameters = parameterList;
            //buffer = entryList;
        }
    }
}
