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
                if (keyAlg == KeyAlg.NONE)
                {
                    continue;
                }
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
                if(keyAlg == KeyAlg.NONE)
                {
                    continue;
                }
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
            byte[] publicBytes = await KeyApi.GetPublicBytesFromKeyAsync(
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
            byte[] secretBytes = await KeyApi.GetSecretBytesFromKeyAsync(
                skHandle);

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromSecretBytesAsync(
                keyAlg,
                secretBytes);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        [Test, TestCase(TestName = "CreateKeyFromKeyExchangeAsync call returns request handle.")]
        public async Task CreateKeyFromKeyExchangeAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg1 = KeyAlg.K256;
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr secretKeyHandle1 = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg1,
                    seedJson,
                    method);

            KeyAlg keyAlg2 = KeyAlg.K256;
            string seedJson2 = "testseed000000000000000000000002";
            IntPtr secretKeyHandle2 = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg2,
                    seedJson2,
                    method);

            KeyAlg keyAlg = KeyAlg.A128CBC_HS256;

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromKeyExchangeAsync(
                keyAlg,
                secretKeyHandle1,
                secretKeyHandle2);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }
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
            byte[] actual = await KeyApi.GetPublicBytesFromKeyAsync(
                handle);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "GetSecretBytesFromKeyAsync call returns request handle.")]
        public async Task GetSecretBytesFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.BLS12_381_G1;
            string seedJson = "testseed000000000000000000000001";
            SeedMethod method = SeedMethod.BlsKeyGen;
            IntPtr secretKeyHandle = await KeyApi.CreateKeyFromSeedAsync(
                    keyAlg,
                    seedJson,
                    method);

            //Act
            byte[] actual = await KeyApi.GetSecretBytesFromKeyAsync(
                secretKeyHandle);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
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
            byte[] actual = await KeyApi.GetJwkSecretFromKeyAsync(
                keyHandle);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
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
            byte[] actual = await KeyApi.GetAeadRandomNonceFromKeyAsync(
                testHandle);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
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
            byte[] testNonce = await KeyApi.GetAeadRandomNonceFromKeyAsync(testHandle);
            string testAad = "testAad";

            //Act
            (byte[] value, byte[] tag, byte[] nonce) = await KeyApi.EncryptKeyWithAeadAsync(
                testHandle,
                testMessage,
                testNonce,
                testAad);

            //Assert
            _ = value.Length.Should().NotBe(0);
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
            byte[] testNonce = await KeyApi.GetAeadRandomNonceFromKeyAsync(testHandle);
            string testTag = "testTag";
            string testAad = "testAad";

            string testMessage = "testMessage";
            (byte[] value, byte[] tag, byte[] nonce) = await KeyApi.EncryptKeyWithAeadAsync(
                testHandle,
                testMessage,
                testNonce,
                testAad);

            //Act
            byte[] actual = await KeyApi.DecryptKeyWithAeadAsync(
                testHandle,
                value,
                nonce,
                tag,
                testAad);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }
        #endregion

        #region Crypto
        [Test, TestCase(TestName = "CreateCryptoBoxRandomNonceAsync call returns request handle.")]
        public async Task CreateCryptoBoxRandomNonceAsyncWorks()
        {
            //Arrange

            //Act
            byte[] actual = await KeyApi.CreateCryptoBoxRandomNonceAsync();

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "CryptoBoxAsync call returns request handle.")]
        public async Task CryptoBoxAsyncWorks()
        {
            KeyAlg keyRecipientAlg = KeyAlg.X25519;
            byte testRecipientEphemeral = 5;
            IntPtr testRecipientHandle = await KeyApi.CreateKeyAsync(
                    keyRecipientAlg,
                    testRecipientEphemeral);

            KeyAlg keySenderAlg = KeyAlg.X25519;
            byte testSenderEphemeral = 10;
            IntPtr testSenderHandle = await KeyApi.CreateKeyAsync(
                    keySenderAlg,
                    testSenderEphemeral);

            string testMessage = "testMessage";
            byte[] testNonce = await KeyApi.CreateCryptoBoxRandomNonceAsync();

            //Act
            byte[] actual = await KeyApi.CryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testMessage,
                testNonce);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "OpenCryptoBoxAsync call returns request handle.")]
        public async Task OpenCryptoBoxAsyncWorks()
        {
            KeyAlg keyRecipientAlg = KeyAlg.X25519;
            byte testRecipientEphemeral = 5;
            IntPtr testRecipientHandle = await KeyApi.CreateKeyAsync(
                    keyRecipientAlg,
                    testRecipientEphemeral);

            KeyAlg keySenderAlg = KeyAlg.X25519;
            byte testSenderEphemeral = 10;
            IntPtr testSenderHandle = await KeyApi.CreateKeyAsync(
                    keySenderAlg,
                    testSenderEphemeral);

            string testMessage = "testMessage";
            byte[] testNonce = await KeyApi.CreateCryptoBoxRandomNonceAsync();

            byte[] encrypted = await KeyApi.CryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testMessage,
                testNonce);

            //Act
            byte[] actual = await KeyApi.OpenCryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                encrypted,
                testNonce);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }
        #endregion
    }
}
