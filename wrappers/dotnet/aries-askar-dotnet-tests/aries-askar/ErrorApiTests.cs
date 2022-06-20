using aries_askar_dotnet.aries_askar;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.aries_askar
{
    public class ErrorApiTests
    {
        [Test]
        [TestCase(TestName = "GetCurrentErrorAsync returns the json of an empty error.")]
        public async Task GetCurrentError()
        {
            //Arrange

            //Act
            string expected = "{\"code\":0,\"message\":null}";
            string actual = await ErrorApi.GetCurrentErrorAsync();

            //Assert
            actual.Should().Be(expected);
        }
    }
}