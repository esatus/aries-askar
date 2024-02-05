using aries_askar_dotnet.AriesAskar;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aries_askar_dotnet.Models.Structures;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class SecretApiTests
    {
        [Test, TestCase(TestName = "SetBufferFreeAsync() call returns errorcode 0.")]
        public async Task SetBufferFreeAsyncWorks()
        {
            //Arrange
            string testText = "testmessage";

            ByteBuffer testBuffer = ByteBuffer.Create(testText);            

             await SecretApi.SetBufferFreeAsync(testBuffer);

            //Assert
            _ = testBuffer.len.Should().Be(0);
           
        }
    }
}
