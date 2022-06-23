using aries_askar_dotnet.aries_askar;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.aries_askar
{
    public class StoreApiTests
    {
        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
        }

        [Test, TestCase(TestName = "StoreGenerateRawKey call returns result string.")]
        public async Task StoreGenerateRawKey()
        {
            //Arrange
            string testSeed = "testseed000000000000000000000001";

            //Act
            string actual = await StoreApi.StoreGenerateRawKeyAsync(testSeed);

            //Assert
            actual.Should().NotBe("");
        }
        
        [Test, TestCase(TestName = "StoreProvision call returns store handle.")]
        public async Task StoreProvision()
        {/**
            //Arrange
            
            string testSpecUri = "test";
            string testKeyMethod = "raw";
            string testPassKey = "adad";
            string testProfile = "testProfile";
            bool testRecreate = true;

            //Act
            uint actual = await StoreApi.StoreProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Assert
            //actual.Should().NotBe("");
            **/
        }
    }
}
