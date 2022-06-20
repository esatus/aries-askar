using aries_askar_dotnet.aries_askar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.aries_askar
{
    public class KeyApiTests
    {
        [Test, TestCase(TestName = "BuildCredDefRequest call returns request handle.")]
        public async Task BuildCredDefRequestWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128KW;
            byte testEphemeral = 5;

            //Act
            uint actual = await KeyApi.GenerateKeyAsync(
                keyAlg,
                testEphemeral);

            //Assert
            _ = actual.Should().NotBe(0);
        }
    }
}
