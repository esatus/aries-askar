using System;
using System.Collections.Generic;

namespace aries_askar_dotnet.Models
{
    public class Scan
    {
        public IntPtr scanHandle { get; set; }
        public IntPtr storeHandle { get; set; }
        public List<object> parameters { get; set; }
        //public IterEntryList buffer { get; set; }

        public Scan(IntPtr handle, IntPtr stohandle, List<object> parameterList) //IterEntryList entryList, List<string> parameterList)
        {
            scanHandle = handle;
            storeHandle = stohandle;
            parameters = parameterList;
            //buffer = entryList;
        }
    }
}
