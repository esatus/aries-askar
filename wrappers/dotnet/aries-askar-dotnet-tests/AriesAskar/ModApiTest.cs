using aries_askar_dotnet.AriesAskar;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class ModApiTest
    {
        #region Tests for GetVersionAsync
        [Test, TestCase(TestName = "GetVersionAsync() returns a string that is not empty.")]
        public async Task GetVersion()
        {
            //Arrange

            //Act
            string actual = await ModApi.GetVersionAsync();

            //Assert
            actual.Should().NotBeEmpty();
        }
        #endregion
    }
}
