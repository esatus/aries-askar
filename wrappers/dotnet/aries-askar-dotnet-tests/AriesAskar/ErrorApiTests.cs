using aries_askar_dotnet.AriesAskar;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class ErrorApiTests
    {
        [Test]
        [TestCase(TestName = "GetCurrentErrorAsync() returns the JSON string of an empty error.")]
        public async Task GetCurrentErrorAsync()
        {
            //Arrange
            //Act
            string expected = "{\"code\":0,\"message\":null}";
            string actual = await ErrorApi.GetCurrentErrorAsync();

            //Assert
            _ = actual.Should().Be(expected);
        }
    }
}
