using System;
using System.Collections.Generic;
using System.Text;

namespace aries_askar_dotnet.Models
{
    public class CustomLogger
    {
        public IntPtr Logger {  get; set; }

        public CustomLogger(IntPtr customLogger) 
        {
            Logger = customLogger; 
        }
    }
}
