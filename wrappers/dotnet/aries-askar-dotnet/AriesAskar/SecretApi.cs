using aries_askar_dotnet.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static aries_askar_dotnet.Models.Structures;

namespace aries_askar_dotnet.AriesAskar
{
    public static class SecretApi
    {
        public static Task SetBufferFreeAsync(ByteBuffer secretBytes)
        {
            NativeMethods.askar_buffer_free(secretBytes);
            return Task.CompletedTask;
        }
    }
}
