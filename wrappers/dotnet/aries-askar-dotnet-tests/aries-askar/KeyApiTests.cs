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
        #region Create
        [Test, TestCase(TestName = "CreateKeyAsync call returns request handle.")]
        public async Task CreateKeyAsyncWorks()
        {
            //Arrange
            byte testEphemeral = 5;

            //Act
            foreach (KeyAlg keyAlg in Enum.GetValues(typeof(KeyAlg)))
            {
                IntPtr actual = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

                //Assert
                _ = actual.Should().NotBe(new IntPtr());
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
                IntPtr actual = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);

                //Assert
                _ = actual.Should().NotBe(new IntPtr());
            }
        }

        [Test, TestCase(TestName = "CreateKeyFromJwkAsync call returns request handle.")]
        public async Task CreateKeyFromJwkAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = keyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        [Test, TestCase(TestName = "CreateKeyFromPublicBytesAsync call returns request handle.")]
        public async Task CreateKeyFromPublicBytesAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr pkHandle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);
            SecretBuffer publicBytes = await KeyApi.GetPublicBytesFromKeyAsync(
                pkHandle);

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromPublicBytesAsync(
                keyAlg,
                publicBytes);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        [Test, TestCase(TestName = "CreateKeyFromSecretBytesAsync call returns request handle.")]
        public async Task CreateKeyFromSecretBytesAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr skHandle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);
            SecretBuffer secretBytes = await KeyApi.GetSecretBytesFromKeyAsync(
                skHandle);

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromSecretBytesAsync(
                keyAlg,
                secretBytes);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        /*[Test, TestCase(TestName = "CreateKeyFromKeyExchangeAsync call returns request handle.")]
        public async Task CreateKeyFromKeyExchangeAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128CBC_HS256;

            string pkSeedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr pkHandle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    pkSeedJson,
                    method);

            KeyAlg jwkKeyAlg = KeyAlg.BLS12_381_G1;
            IntPtr skHandle = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = jwkKeyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromKeyExchangeAsync(
                keyAlg,
                skHandle,
                pkHandle);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }*/
        #endregion

        #region Get
        [Test, TestCase(TestName = "GetPublicBytesFromKeyAsync call returns request handle.")]
        public async Task GetPublicBytesFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr handle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);

            //Act
            SecretBuffer actual = await KeyApi.GetPublicBytesFromKeyAsync(
                handle);

            //Assert
            _ = actual.len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetSecretBytesFromKeyAsync call returns request handle.")]
        public async Task GetSecretBytesFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            IntPtr keyHandle = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = keyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Act
            SecretBuffer actual = await KeyApi.GetSecretBytesFromKeyAsync(
                keyHandle);

            //Assert
            _ = actual.len.Should().Be(0);
        }

        [Test, TestCase(TestName = "GetAlgorithmFromKeyAsync call returns request handle.")]
        public async Task GetAlgorithmFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr handle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);

            //Act
            string actual = await KeyApi.GetAlgorithmFromKeyAsync(
                handle);

            //Assert
            _ = actual.Should().Be(keyAlg.ToKeyAlgString());
        }

        [Test, TestCase(TestName = "GetEphemeralFromKeyAsync call returns request handle.")]
        public async Task GetEphemeralFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128GCM;
            byte testEphemeral = 5;
            IntPtr keyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            //Act
            byte actual = await KeyApi.GetEphemeralFromKeyAsync(
                keyHandle);

            //Assert
            _ = actual.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetJwkPublicFromKeyAsync call returns request handle.")]
        public async Task GetJwkPublicFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr handle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);

            //Act
            string actual = await KeyApi.GetJwkPublicFromKeyAsync(
                handle,
                keyAlg);

            //Assert
            _ = actual.Should().NotBe("");
        }

        [Test, TestCase(TestName = "GetJwkSecretFromKeyAsync call returns request handle.")]
        public async Task GetJwkSecretFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            IntPtr keyHandle = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = keyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Act
            SecretBuffer actual = await KeyApi.GetJwkSecretFromKeyAsync(
                keyHandle);

            //Assert
            _ = actual.len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetJwkThumbprintFromKeyAsync call returns request handle.")]
        public async Task GetJwkThumbprintFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            IntPtr keyHandle = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = keyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Act
            string actual = await KeyApi.GetJwkThumbprintFromKeyAsync(
                keyHandle,
                keyAlg);

            //Assert
            _ = actual.Should().NotBe("");
        }
        #endregion

        #region aead
        [Test, TestCase(TestName = "GetAeadRandomNonceFromKeyAsync call returns request handle.")]
        public async Task GetAeadRandomNonceFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128GCM;
            byte testEphemeral = 5;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            //Act
            SecretBuffer actual = await KeyApi.GetAeadRandomNonceFromKeyAsync(
                testHandle);

            //Assert
            _ = actual.len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetAeadParamsFromKeyAsync call returns request handle.")]
        public async Task GetAeadParamsFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128GCM;
            byte testEphemeral = 5;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            //Act
            AeadParams actual = await KeyApi.GetAeadParamsFromKeyAsync(
                testHandle);

            //Assert
            _ = actual.nonce_length.Should().NotBe(0);
            _ = actual.tag_length.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetAeadPaddingFromKeyAsync call returns request handle.")]
        public async Task GetAeadPaddingFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128GCM;
            byte testEphemeral = 5;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);
            long testMsgLen = 10;

            //Act
            int actual = await KeyApi.GetAeadPaddingFromKeyAsync(
                testHandle,
                testMsgLen);

            //Assert
            _ = actual.Should().Be(0);
        }

        [Test, TestCase(TestName = "EncryptKeyWithAeadAsync call returns request handle.")]
        public async Task EncryptKeyWithAeadAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128GCM;
            byte testEphemeral = 5;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);
            string testMessage = "testMessage";
            SecretBuffer testNonce = await KeyApi.CreateCryptoBoxRandomNonceAsync();
            string testAad = "testAad";

            //Act
            EncryptedBuffer actual = await KeyApi.EncryptKeyWithAeadAsync(
                testHandle,
                testMessage,
                testNonce,
                testAad);

            //Assert
            _ = actual.buffer.len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "DecryptKeyWithAeadAsync call returns request handle.")]
        public async Task DecryptKeyWithAeadAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A128GCM;
            byte testEphemeral = 5;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);
            string testCiphertext = "testCiphertext";
            SecretBuffer testNonce = await KeyApi.CreateCryptoBoxRandomNonceAsync();
            string testTag = "testTag";
            string testAad = "testAad";

            //Act
            SecretBuffer actual = await KeyApi.DecryptKeyWithAeadAsync(
                testHandle,
                testCiphertext,
                testNonce,
                testTag,
                testAad);

            //Assert
            _ = actual.len.Should().NotBe(0);
        }
        #endregion
    }
}
