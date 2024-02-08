using System;
using System.Collections.Generic;
using System.Text;
using static aries_askar_dotnet.Models.Structures;
using System.Threading.Tasks;

namespace aries_askar_dotnet.AriesAskar
{
    public class ModApi
    {
        public static Task TerminateAsync()
        {
            NativeMethods.askar_terminate();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets current askar version.
        /// </summary>
        /// <returns>Currently used version of askar (Format: <c>x.x.x</c>).</returns>
        public static async Task<string> GetVersionAsync()
        {
            return await Task.FromResult<string>(NativeMethods.askar_version());
        }
    }
}
