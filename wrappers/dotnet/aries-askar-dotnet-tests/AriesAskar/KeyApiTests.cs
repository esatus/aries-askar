using aries_askar_dotnet.AriesAskar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class KeyApiTests
    {
        #region Create
        [Test, TestCaseSource(nameof(CreateKeyAsyncCases))]
        public async Task CreateKeyAsyncTests(KeyAlg testKeyAlg, bool shouldWork)
        {
            //Arrange
            byte testEphemeral = 1;

            //Act
            IntPtr actual = await KeyApi.CreateKeyAsync(
                testKeyAlg,
                testEphemeral);

            //Assert
            if (shouldWork)
            {
                _ = actual.Should().NotBe(new IntPtr());
            }
            else
            {
                _ = actual.Should().Be(new IntPtr());
            }
        }

        private static IEnumerable<TestCaseData> CreateKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256, true)
                .SetName("CreateKeyAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.NONE, false)
                .SetName("CreateKeyAsync fails if no key algorithm is provided.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromSeedAsyncCases))]
        public async Task CreateKeyFromSeedAsyncTests(KeyAlg testKeyAlg, bool shouldWork)
        {
            //Arrange
            string testSeedJson = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeedJson,
                    testSeedMethod);

            //Assert
            if (shouldWork)
            {
                _ = actual.Should().NotBe(new IntPtr());
            }
            else
            {
                _ = actual.Should().Be(new IntPtr());
            }
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSeedAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256, true)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.NONE, false)
                .SetName("CreateKeyFromSeedAsync fails if no key algorithm is provided.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromJwkAsyncCases))]
        public async Task CreateKeyFromJwkAsyncTests(KeyAlg testKeyAlg, bool shouldWork)
        {
            //Arrange

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = testKeyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Assert
            if (shouldWork)
            {
                _ = actual.Should().NotBe(new IntPtr());
            }
            else
            {
                _ = actual.Should().Be(new IntPtr());
            }
        }

        private static IEnumerable<TestCaseData> CreateKeyFromJwkAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, true)
                .SetName("CreateKeyFromJwkAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.A128CBC_HS256, false)
                .SetName("CreateKeyFromJwkAsync fails if the provided key algorithm is not BLS12_381_G1, BLS12_381_G2 or BLS12_381_G1G2.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromPublicBytesAsyncCases))]
        public async Task CreateKeyFromPublicBytesAsyncTests(KeyAlg testKeyAlg, bool shouldWork)
        {
            //Arrange
            string testSeedJson = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr testPkHandle = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeedJson,
                    testSeedMethod);
            byte[] testPublicBytes = await KeyApi.GetPublicBytesFromKeyAsync(
                testPkHandle);

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromPublicBytesAsync(
                testKeyAlg,
                testPublicBytes);

            //Assert
            if (shouldWork)
            {
                _ = actual.Should().NotBe(new IntPtr());
            }
            else
            {
                _ = actual.Should().Be(new IntPtr());
            }
        }

        private static IEnumerable<TestCaseData> CreateKeyFromPublicBytesAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, true)
                .SetName("CreateKeyFromPublicBytesAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.A128CBC_HS256, false)
                .SetName("CreateKeyFromPublicBytesAsync fails if the provided key algorithm is not BLS12_381_G1, BLS12_381_G2, BLS12_381_G1G2, ED25519, X25519, P256 or K256.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromSecretBytesAsyncCases))]
        public async Task CreateKeyFromSecretBytesAsyncTests(KeyAlg testKeyAlg, byte[] testSecretBytes, bool shouldWork)
        {
            //Act
            IntPtr actual = await KeyApi.CreateKeyFromSecretBytesAsync(
                testKeyAlg,
                testSecretBytes);

            //Assert
            if (shouldWork)
            {
                _ = actual.Should().NotBe(new IntPtr());
            }
            else
            {
                _ = actual.Should().Be(new IntPtr());
            }
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSecretBytesAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM, new byte[] { 188, 199, 70, 37, 50, 103, 100, 241, 231, 16, 97, 18, 220, 249, 108, 76 }, true)
                .SetName("CreateKeyFromSecretBytesAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, new byte[] { 188, 199, 70, 37, 50, 103, 100, 241, 231, 16, 97, 18, 220, 249, 108, 76 }, false)
                .SetName("CreateKeyFromSecretBytesAsync fails if the key algorithm does not fit the provided secret bytes.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromKeyExchangeAsyncCases))]
        public async Task CreateKeyFromKeyExchangeAsyncTests(KeyAlg testKeyAlg1, KeyAlg testKeyAlg2, bool shouldWork)
        {
            //Arrange
            string testSeedJson1 = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr secretKeyHandle1 = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg1,
                    testSeedJson1,
                    testSeedMethod);

            string testSeedJson2 = "testseed000000000000000000000002";
            IntPtr secretKeyHandle2 = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg2,
                    testSeedJson2,
                    testSeedMethod);

            KeyAlg testKeyAlg3 = KeyAlg.A128CBC_HS256;

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromKeyExchangeAsync(
                testKeyAlg3,
                secretKeyHandle1,
                secretKeyHandle2);

            //Assert
            if (shouldWork)
            {
                _ = actual.Should().NotBe(new IntPtr());
            }
            else
            {
                _ = actual.Should().Be(new IntPtr());
            }
        }

        private static IEnumerable<TestCaseData> CreateKeyFromKeyExchangeAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.K256, KeyAlg.K256, true)
                .SetName("CreateKeyFromKeyExchangeAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.P256, KeyAlg.K256, false)
                .SetName("CreateKeyFromKeyExchangeAsync fails if the provided keys did not use the same key algorithm.");
        }
        #endregion

        #region Get
        [Test, TestCaseSource(nameof(GetPublicBytesFromKeyAsyncCases))]
        public async Task GetPublicBytesFromKeyAsyncWorks(KeyAlg testKeyAlg, int expectedLength)
        {
            //Arrange
            byte testEphemeral = 1;
            IntPtr handle = await KeyApi.CreateKeyAsync(
                testKeyAlg,
                testEphemeral);

            //Act
            byte[] actual = await KeyApi.GetPublicBytesFromKeyAsync(
                handle);

            _ = actual.Length.Should().Be(expectedLength);
        }

        private static IEnumerable<TestCaseData> GetPublicBytesFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, 48)
                .SetName("GetPublicBytesFromKeyAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.A128GCM, 0)
                .SetName("GetPublicBytesFromKeyAsync fails if the key was created with one of " +
                "the following algorithms: A128GCM, A256GCM, A128CBC_HS256, A256CBC_HS512, A128KW," +
                "A256KW, C20P, XC20P.");
        }

        [Test, TestCaseSource(nameof(GetSecretBytesFromKeyAsyncCases))]
        public async Task GetSecretBytesFromKeyAsyncWorks(KeyAlg testKeyAlg, int expectedLength)
        {
            //Arrange
            string testSeedJson = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr secretKeyHandle = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeedJson,
                    testSeedMethod);

            //Act
            byte[] actual = await KeyApi.GetSecretBytesFromKeyAsync(
                secretKeyHandle);

            //Assert
            _ = actual.Length.Should().Be(expectedLength);
        }

        private static IEnumerable<TestCaseData> GetSecretBytesFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, 32)
                .SetName("GetSecretBytesFromKeyAsync returns the handle of the created key.");
            yield return new TestCaseData(KeyAlg.NONE, 0)
                .SetName("GetSecretBytesFromKeyAsync fails if the key handle points to invalid data.");
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

            byte[] testBox = await KeyApi.CryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testMessage,
                testNonce);

            //Act
            byte[] actual = await KeyApi.OpenCryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testBox,
                testNonce);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "SealCryptoBoxAsync call returns request handle.")]
        public async Task SealCryptoBoxAsyncWorks()
        {
            KeyAlg keyAlg = KeyAlg.X25519;
            byte testEphemeral = 5;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            string testMessage = "testMessage";

            //Act
            byte[] actual = await KeyApi.SealCryptoBoxAsync(
                testKeyHandle,
                testMessage);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        [Test, TestCase(TestName = "OpenSealCryptoBoxAsync call returns request handle.")]
        public async Task OpenSealCryptoBoxAsyncWorks()
        {
            KeyAlg keyAlg = KeyAlg.X25519;
            byte testEphemeral = 5;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            string testMessage = "testMessage";

            byte[] sealedBox = await KeyApi.SealCryptoBoxAsync(
                testKeyHandle,
                testMessage);

            byte[] actual = await KeyApi.OpenSealCryptoBoxAsync(
                testKeyHandle,
                sealedBox);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }
        #endregion

        #region Utils
        [Test, TestCase(TestName = "ConvertKeyAsync call returns request handle.")]
        public async Task ConvertKeyAsyncWorks()
        {
            //Arrange
            KeyAlg oldKeyAlg = KeyAlg.ED25519;
            byte testEphemeral = 5;
            IntPtr oldKey = await KeyApi.CreateKeyAsync(
                    oldKeyAlg,
                    testEphemeral);
            KeyAlg newKeyAlg = KeyAlg.X25519;

            //Act
            IntPtr convertedKey = await KeyApi.ConvertKeyAsync(
                oldKey,
                newKeyAlg);
            string actual = await KeyApi.GetAlgorithmFromKeyAsync(convertedKey);

            //Assert
            _ = actual.Should().Be(newKeyAlg.ToKeyAlgString());
        }

        [Test, TestCase(TestName = "FreeKeyAsync call returns request handle.")]
        public async Task FreeKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.ED25519;
            byte testEphemeral = 5;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            //Act
            string keyBeforeFree = await KeyApi.GetAlgorithmFromKeyAsync(testKeyHandle);
            Func<Task> actual = async () => await KeyApi.FreeKeyAsync(testKeyHandle);

            //Assert
            _ = actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "SignMessageFromKeyAsync call returns request handle.")]
        public async Task SignMessageFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.K256;
            byte testEphemeral = 5;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);
            byte[] testMessage = ByteBuffer.Create("testMessage").Decode();
            string testSigType = "ES256K";

            //Act
            byte[] actual = await KeyApi.SignMessageFromKeyAsync(
                testKeyHandle,
                testMessage,
                testSigType);

            //Assert
            _ = actual.Should().NotBeEmpty();
        }

        [Test, TestCase(TestName = "VerifySignatureFromKeyAsync call returns request handle.")]
        public async Task VerifySignatureFromKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.K256;
            byte testEphemeral = 5;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);
            byte[] testMessage = ByteBuffer.Create("testMessage").Decode();
            string testSigType = "ES256K";

            //Act
            byte[] testSignedMessage = await KeyApi.SignMessageFromKeyAsync(
                testKeyHandle,
                testMessage,
                testSigType);

            bool actual = await KeyApi.VerifySignatureFromKeyAsync(
                testKeyHandle,
                testMessage,
                testSignedMessage,
                testSigType);

            //Assert
            _ = actual.Should().BeTrue();
        }

        [Test, TestCase(TestName = "WrapKeyAsync call returns request handle.")]
        public async Task WrapKeyAsyncWorks()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A256GCM;
            byte testEphemeral = 5;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            KeyAlg otherKeyAlg = KeyAlg.A128KW;
            byte otherTestEphemeral = 15;
            IntPtr otherTestKeyHandle = await KeyApi.CreateKeyAsync(
                    otherKeyAlg,
                    otherTestEphemeral);

            byte[] testNonce = await KeyApi.GetAeadRandomNonceFromKeyAsync(testKeyHandle);

            //Act
            (byte[] value, byte[] tag, byte[] nonce) = await KeyApi.WrapKeyAsync(
                testKeyHandle,
                otherTestKeyHandle,
                testNonce);

            //Assert
            _ = value.Should().NotBeEmpty();
        }
        #endregion
    }
}
