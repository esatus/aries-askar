using aries_askar_dotnet.aries_askar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet_tests.aries_askar
{
    public class KeyApiTests
    {
        private uint _keyHandle = 0;
        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;

            //Act
            _keyHandle = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = keyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));
        }

        [Test, TestCase(TestName = "GenerateKeyAsync call returns request handle.")]
        public async Task GenerateKeyAsyncWorks()
        {
            //Arrange
            byte testEphemeral = 5;

            //Act
            foreach (KeyAlg keyAlg in Enum.GetValues(typeof(KeyAlg)))
            {
                uint actual = await KeyApi.GenerateKeyAsync(
                    keyAlg,
                    testEphemeral);

                //Assert
                _ = actual.Should().NotBe(0);
            }
        }

        [Test, TestCase(TestName = "CreateKeyFromSeedAsync call returns request handle.")]
        public async Task CreateKeyFromSeedAsyncWorks()
        {
            //Arrange
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;

            //Act
            foreach (KeyAlg keyAlg in Enum.GetValues(typeof(KeyAlg)))
            {
                uint actual = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);

                //Assert
                _ = actual.Should().NotBe(0);
            }
        }

        [Test, TestCase(TestName = "CreateKeyFromJwkAsync call returns request handle.")]
        public async Task CreateKeyFromJwkAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;

            //Act
            uint actual = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = keyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Assert
            _ = actual.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "CreateKeyFromPublicBytesAsync call returns request handle.")]
        public async Task CreateKeyFromPublicBytesAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            string bytesAsString = "testPublicBytes";
            byte[] publicBytes = Encoding.ASCII.GetBytes(bytesAsString);

            //Act
            uint actual = await KeyApi.CreateKeyFromPublicBytesAsync(
                keyAlg,
                bytesAsString);

            //Assert
            _ = actual.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetPublicBytesFromKeyAsync call returns request handle.")]
        public async Task GetPublicBytesFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            //string bytesAsString = "testBytes";
            //byte[] publicBytes = Encoding.ASCII.GetBytes(bytesAsString);
            //uint handle = await KeyApi.CreateKeyFromPublicBytesAsync(
            //    keyAlg,
            //    bytesAsString);
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            uint handle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);

            //Act
            SecretBuffer actual = await KeyApi.GetPublicBytesFromKeyAsync(
                handle);

            //Assert
            _ = actual.len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetJwkPublicFromKeyAsync call returns request handle.")]
        public async Task GetJwkPublicFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;

            //Act
            string actual = await KeyApi.GetJwkPublicFromKeyAsync(
                _keyHandle,
                keyAlg);

            //Assert
            _ = actual.Should().NotBe("");
        }
    }
}
