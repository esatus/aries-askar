using aries_askar_dotnet;
using aries_askar_dotnet.AriesAskar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class KeyApiTests
    {
        private readonly UTF8Encoding Decoder = new(true, true);

        #region Create
        #region CreateKeyAsync
        [Test, TestCaseSource(nameof(CreateKeyAsyncCases)), Category("Create")]
        public async Task CreateKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;

            //Act
            IntPtr actual = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        private static IEnumerable<TestCaseData> CreateKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256)
                .SetName("CreateKeyAsync returns the handle of the created key with A128CBC_HS256.");
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("CreateKeyAsync returns the handle of the created key with A128GCM.");
            yield return new TestCaseData(KeyAlg.A128KW)
                .SetName("CreateKeyAsync returns the handle of the created key with A128KW.");
            yield return new TestCaseData(KeyAlg.A256CBC_HS512)
                .SetName("CreateKeyAsync returns the handle of the created key with A256CBC_HS512.");
            yield return new TestCaseData(KeyAlg.A256GCM)
                .SetName("CreateKeyAsync returns the handle of the created key with A256GCM.");
            yield return new TestCaseData(KeyAlg.A256KW)
                .SetName("CreateKeyAsync returns the handle of the created key with A256KW.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G1)
                .SetName("CreateKeyAsync returns the handle of the created key with BLS12_381_G1.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G1G2)
                .SetName("CreateKeyAsync returns the handle of the created key with BLS12_381_G1G2.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G2)
                .SetName("CreateKeyAsync returns the handle of the created key with BLS12_381_G2.");
            yield return new TestCaseData(KeyAlg.C20P)
                .SetName("CreateKeyAsync returns the handle of the created key with C20P.");
            yield return new TestCaseData(KeyAlg.ED25519)
                .SetName("CreateKeyAsync returns the handle of the created key with ED25519.");
            yield return new TestCaseData(KeyAlg.K256)
                .SetName("CreateKeyAsync returns the handle of the created key with K256.");
            yield return new TestCaseData(KeyAlg.P256)
                .SetName("CreateKeyAsync returns the handle of the created key with P256.");
            yield return new TestCaseData(KeyAlg.X25519)
                .SetName("CreateKeyAsync returns the handle of the created key with X25519.");
            yield return new TestCaseData(KeyAlg.XC20P)
                .SetName("CreateKeyAsync returns the handle of the created key with XC20P.");
        }

        [Test, TestCaseSource(nameof(CreateKeyAsyncErrorCases)), Category("Create")]
        public async Task CreateKeyAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;

            //Act
            Func<Task> action = async () =>
            {
                _ = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);
            };

            //Assert
            _ = await action.Should().ThrowAsync<AriesAskarException>();
        }

        private static IEnumerable<TestCaseData> CreateKeyAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.NONE)
                .SetName("CreateKeyAsync throws an AriesAskarException if no key algorithm is provided.");
        }
        #endregion

        #region CreateKeyFromSeedAsync
        [Test, TestCaseSource(nameof(CreateKeyFromSeedAsyncCases)), Category("Create")]
        public async Task CreateKeyFromSeedAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            string testSeed = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeed,
                    testSeedMethod);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSeedAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with A128CBC_HS256.");
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with A128GCM.");
            yield return new TestCaseData(KeyAlg.A128KW)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with A128KW.");
            yield return new TestCaseData(KeyAlg.A256CBC_HS512)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with A256CBC_HS512.");
            yield return new TestCaseData(KeyAlg.A256GCM)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with A256GCM.");
            yield return new TestCaseData(KeyAlg.A256KW)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with A256KW.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G1)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with BLS12_381_G1.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G1G2)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with BLS12_381_G1G2.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G2)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with BLS12_381_G2.");
            yield return new TestCaseData(KeyAlg.C20P)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with C20P.");
            yield return new TestCaseData(KeyAlg.ED25519)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with ED25519.");
            yield return new TestCaseData(KeyAlg.K256)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with K256.");
            yield return new TestCaseData(KeyAlg.P256)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with P256.");
            yield return new TestCaseData(KeyAlg.X25519)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with X25519.");
            yield return new TestCaseData(KeyAlg.XC20P)
                .SetName("CreateKeyFromSeedAsync returns the handle of the created key with XC20P.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromSeedAsyncErrorCases)), Category("Create")]
        public async Task CreateKeyFromSeedAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            string testSeed = "testseed";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;

            //Act
            Func<Task> action = async () => await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeed,
                    testSeedMethod);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSeedAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.NONE)
                .SetName("CreateKeyFromSeedAsync throws an AriesAskarException if the seed length is not exactly 32.");
        }
        #endregion

        #region CreateKeyFromJwkAsync
        [Test, TestCaseSource(nameof(CreateKeyFromJwkAsyncCases)), Category("Create")]
        public async Task CreateKeyFromJwkAsyncTests(KeyAlg testKeyAlg, string testX)
        {
            //Arrange

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromJwkAsync(JsonConvert.SerializeObject(new
            {
                crv = testKeyAlg.ToJwkCrvString(),
                kty = "OKP",
                x = testX
            }));

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        private static IEnumerable<TestCaseData> CreateKeyFromJwkAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1,
                "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV")
                .SetName("CreateKeyFromJwkAsync returns the handle of the created key for BLS12_381_G1.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G2,
                "iZIOsO6BgLV72zCrBE2ym3DEhDYcghnUMO4O8IVVD8yS-C_zu6OA3L-ny-AO4rbkAo-" +
                "WuApZEjn83LY98UtoKpTufn4PCUFVQZzJNH_gXWHR3oDspJaCbOajBfm5qj6d")
                .SetName("CreateKeyFromJwkAsync returns the handle of the created key for BLS12_381_G2.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G1G2,
                "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrVi" +
                "ZIOsO6BgLV72zCrBE2ym3DEhDYcghnUMO4O8IVVD8yS-C_zu6OA3L-ny-AO4rbkAo-WuA" +
                "pZEjn83LY98UtoKpTufn4PCUFVQZzJNH_gXWHR3oDspJaCbOajBfm5qj6d")
                .SetName("CreateKeyFromJwkAsync returns the handle of the created key for BLS12_381_G1G2.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromJwkAsyncErrorCases)), Category("Create")]
        public async Task CreateKeyFromJwkAsyncErrorTests(KeyAlg testKeyAlg, string testX)
        {
            //Arrange

            //Act
            Func<Task> action = async () => await KeyApi.CreateKeyFromJwkAsync(JsonConvert.SerializeObject(new
            {
                crv = testKeyAlg.ToJwkCrvString(),
                kty = "OKP",
                x = testX
            }));

            //Assert
            _ = await action.Should().ThrowAsync<AriesAskarException>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromJwkAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256,
                "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV")
                .SetName("CreateKeyFromJwkAsync throws an AriesAskarException if the provided key algorithm is not BLS12_381_G1, BLS12_381_G2 or BLS12_381_G1G2.");
            yield return new TestCaseData(KeyAlg.BLS12_381_G1,
                "iZIOsO6BgLV72zCrBE2ym3DEhDYcghnUMO4O8IVVD8yS-C_zu6OA3L-ny-AO4rbkAo-" +
                "WuApZEjn83LY98UtoKpTufn4PCUFVQZzJNH_gXWHR3oDspJaCbOajBfm5qj6d")
                .SetName("CreateKeyFromJwkAsync throws an AriesAskarException if the provided json does not fit the used algorithm.");
        }
        #endregion

        #region CreateKeyFromPublicBytesAsync
        [Test, TestCaseSource(nameof(CreateKeyFromPublicBytesAsyncCases)), Category("Create")]
        public async Task CreateKeyFromPublicBytesAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            byte[] testPublicBytes = new byte[48] { 135, 158, 158, 96, 143, 16, 146, 174, 97, 138, 210, 2, 111, 232, 164, 243, 4, 83, 205, 201, 250, 21, 222, 34, 99, 198, 131, 53, 87, 61, 171, 92, 104, 61, 45, 229, 135, 128, 193, 252, 30, 48, 54, 214, 171, 212, 122, 213 };

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromPublicBytesAsync(
                testKeyAlg,
                testPublicBytes);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        private static IEnumerable<TestCaseData> CreateKeyFromPublicBytesAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1)
                .SetName("CreateKeyFromPublicBytesAsync returns the handle of the created key.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromPublicBytesAsyncErrorCases)), Category("Create")]
        public async Task CreateKeyFromPublicBytesAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            byte[] testPublicBytes = new byte[48] { 135, 158, 158, 96, 143, 16, 146, 174, 97, 138, 210, 2, 111, 232, 164, 243, 4, 83, 205, 201, 250, 21, 222, 34, 99, 198, 131, 53, 87, 61, 171, 92, 104, 61, 45, 229, 135, 128, 193, 252, 30, 48, 54, 214, 171, 212, 122, 213 };

            //Act
            Func<Task> action = async () => await KeyApi.CreateKeyFromPublicBytesAsync(
                testKeyAlg,
                testPublicBytes);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromPublicBytesAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is A128CBC_HS256.");
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is A128GCM.");
            yield return new TestCaseData(KeyAlg.A128KW)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is A128KW.");
            yield return new TestCaseData(KeyAlg.A256CBC_HS512)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is A256CBC_HS512.");
            yield return new TestCaseData(KeyAlg.A256GCM)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is A256GCM.");
            yield return new TestCaseData(KeyAlg.A256KW)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is A256KW.");
            yield return new TestCaseData(KeyAlg.C20P)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is C20P.");
            yield return new TestCaseData(KeyAlg.XC20P)
                .SetName("CreateKeyFromPublicBytesAsync throws an AriesAskarException if the provided key algorithm is XC20P.");
        }
        #endregion

        #region CreateKeyFromSecretBytesAsync
        [Test, TestCaseSource(nameof(CreateKeyFromSecretBytesAsyncCases)), Category("Create")]
        public async Task CreateKeyFromSecretBytesAsyncTests(KeyAlg testKeyAlg, byte[] testSecretBytes)
        {
            //Act
            IntPtr actual = await KeyApi.CreateKeyFromSecretBytesAsync(
                testKeyAlg,
                testSecretBytes);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSecretBytesAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM, new byte[] { 188, 199, 70, 37, 50, 103, 100, 241, 231, 16, 97, 18, 220, 249, 108, 76 })
                .SetName("CreateKeyFromSecretBytesAsync returns the handle of the created key.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromSecretBytesAsyncErrorCases)), Category("Create")]
        public async Task CreateKeyFromSecretBytesAsyncErrorTests(KeyAlg testKeyAlg, byte[] testSecretBytes)
        {
            //Act
            Func<Task> action = async () => await KeyApi.CreateKeyFromSecretBytesAsync(
                testKeyAlg,
                testSecretBytes);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSecretBytesAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, new byte[] { 188, 199, 70, 37, 50, 103, 100, 241, 231, 16, 97, 18, 220, 249, 108, 76 })
                .SetName("CreateKeyFromSecretBytesAsync throws an AriesAskarException if the key algorithm does not fit the provided secret bytes.");
        }
        #endregion

        #region CreateKeyFromKeyExchangeAsync
        [Test, TestCaseSource(nameof(CreateKeyFromKeyExchangeAsyncCases)), Category("Create")]
        public async Task CreateKeyFromKeyExchangeAsyncTests(KeyAlg testKeyAlg1, KeyAlg testKeyAlg2)
        {
            //Arrange
            string testSeed1 = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr secretKeyHandle1 = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg1,
                    testSeed1,
                    testSeedMethod);

            string testSeed2 = "testseed000000000000000000000002";
            IntPtr secretKeyHandle2 = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg2,
                    testSeed2,
                    testSeedMethod);

            KeyAlg testKeyAlg3 = KeyAlg.A128CBC_HS256;

            //Act
            IntPtr actual = await KeyApi.CreateKeyFromKeyExchangeAsync(
                testKeyAlg3,
                secretKeyHandle1,
                secretKeyHandle2);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
        }

        private static IEnumerable<TestCaseData> CreateKeyFromKeyExchangeAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.K256, KeyAlg.K256)
                .SetName("CreateKeyFromKeyExchangeAsync returns the handle of the created key.");
        }

        [Test, TestCaseSource(nameof(CreateKeyFromKeyExchangeAsyncErrorCases)), Category("Create")]
        public async Task CreateKeyFromKeyExchangeAsyncErrorTests(KeyAlg testKeyAlg1, KeyAlg testKeyAlg2)
        {
            //Arrange
            string testSeed1 = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr secretKeyHandle1 = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg1,
                    testSeed1,
                    testSeedMethod);

            string testSeed2 = "testseed000000000000000000000002";
            IntPtr secretKeyHandle2 = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg2,
                    testSeed2,
                    testSeedMethod);

            KeyAlg testKeyAlg3 = KeyAlg.A128CBC_HS256;

            //Act
            Func<Task> action = async () => await KeyApi.CreateKeyFromKeyExchangeAsync(
                testKeyAlg3,
                secretKeyHandle1,
                secretKeyHandle2);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromKeyExchangeAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.P256, KeyAlg.K256)
                .SetName("CreateKeyFromKeyExchangeAsync throws an AriesAskarException if the provided keys are not created with the same key algorithm.");
        }
        #endregion
        #endregion

        #region Get
        #region GetPublicBytesFromKeyAsync
        [Test, TestCaseSource(nameof(GetPublicBytesFromKeyAsyncCases)), Category("Get")]
        public async Task GetPublicBytesFromKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                testKeyAlg,
                testEphemeral);

            //Act
            byte[] actual = await KeyApi.GetPublicBytesFromKeyAsync(
                testKeyHandle);

            _ = actual.Length.Should().Be(48);
        }

        private static IEnumerable<TestCaseData> GetPublicBytesFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1)
                .SetName("GetPublicBytesFromKeyAsync returns the public bytes of a given key.");
        }

        [Test, TestCaseSource(nameof(GetPublicBytesFromKeyAsyncErrorCases)), Category("Get")]
        public async Task GetPublicBytesFromKeyAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                testKeyAlg,
                testEphemeral);

            //Act
            Func<Task> action = async () => await KeyApi.GetPublicBytesFromKeyAsync(
                testKeyHandle);

            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetPublicBytesFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with A128CBC_HS256.");
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with A128GCM.");
            yield return new TestCaseData(KeyAlg.A128KW)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with A128KW.");
            yield return new TestCaseData(KeyAlg.A256CBC_HS512)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with A256CBC_HS512.");
            yield return new TestCaseData(KeyAlg.A256GCM)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with A256GCM.");
            yield return new TestCaseData(KeyAlg.A256KW)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with A256KW.");
            yield return new TestCaseData(KeyAlg.C20P)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with C20P.");
            yield return new TestCaseData(KeyAlg.XC20P)
                .SetName("GetPublicBytesFromKeyAsync throws an AriesAskarException if the key was created with XC20P.");
        }
        #endregion

        #region GetSecretBytesFromKeyAsync
        [Test, TestCaseSource(nameof(GetSecretBytesFromKeyAsyncCases)), Category("Get")]
        public async Task GetSecretBytesFromKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            string testSeed = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeed,
                    testSeedMethod);

            //Act
            byte[] actual = await KeyApi.GetSecretBytesFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = actual.Length.Should().Be(32);
        }

        private static IEnumerable<TestCaseData> GetSecretBytesFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1)
                .SetName("GetSecretBytesFromKeyAsync returns the secret bytes of a given key.");
        }

        [Test, TestCaseSource(nameof(GetSecretBytesFromKeyAsyncErrorCases)), Category("Get")]
        public async Task GetSecretBytesFromKeyAsyncErrorTests()
        {
            //Arrange
            IntPtr testKeyHandle = new();

            //Act
            Func<Task> action = async () => await KeyApi.GetSecretBytesFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetSecretBytesFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("GetSecretBytesFromKeyAsync throws an AriesAskarException if the key handle points to invalid data.");
        }
        #endregion

        #region GetAlgorithmFromKeyAsync
        [Test, TestCaseSource(nameof(GetAlgorithmFromKeyAsyncCases)), Category("Get")]
        public async Task GetAlgorithmFromKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            string testSeed = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeed,
                    testSeedMethod);

            //Act
            string actual = await KeyApi.GetAlgorithmFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = actual.Should().Be(testKeyAlg.ToKeyAlgString());
        }

        private static IEnumerable<TestCaseData> GetAlgorithmFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1)
                .SetName("GetAlgorithmFromKeyAsync returns a string with the name of an algorithm for a given key.");
        }

        [Test, TestCaseSource(nameof(GetAlgorithmFromKeyAsyncErrorCases)), Category("Get")]
        public async Task GetAlgorithmFromKeyAsyncErrorTests()
        {
            //Arrange
            IntPtr testKeyHandle = new();

            //Act
            Func<Task> action = async () => await KeyApi.GetAlgorithmFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetAlgorithmFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("GetAlgorithmFromKeyAsync throws an AriesAskarException if the key handle points to invalid data.");
        }
        #endregion

        #region GetEphemeralFromKeyAsync
        [Test, TestCaseSource(nameof(GetEphemeralFromKeyAsyncCases)), Category("Get")]
        public async Task GetEphemeralFromKeyAsyncTests(KeyAlg testKeyAlg, bool testEphemeral)
        {
            //Arrange
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);

            //Act
            bool actual = await KeyApi.GetEphemeralFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = actual.Should().Be(testEphemeral);
        }

        private static IEnumerable<TestCaseData> GetEphemeralFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM, true)
                .SetName("GetEphemeralFromKeyAsync returns the ephemeral as a byte for a given key.");
        }

        [Test, TestCaseSource(nameof(GetEphemeralFromKeyAsyncErrorCases)), Category("Get")]
        public async Task GetEphemeralFromKeyAsyncErrorTests()
        {
            //Arrange
            IntPtr testKeyHandle = new();

            //Act
            Func<Task> action = async () => await KeyApi.GetEphemeralFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetEphemeralFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("GetEphemeralFromKeyAsync throws an AriesAskarException if the key handle points to invalid data.");
        }
        #endregion

        #region GetJwkPublicFromKeyAsync
        [Test, TestCaseSource(nameof(GetJwkPublicFromKeyAsyncCases)), Category("Get")]
        public async Task GetJwkPublicFromKeyAsyncTests(KeyAlg testKeyAlg, string jwkPublic)
        {
            //Arrange
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromJwkAsync(JsonConvert.SerializeObject(new
            {
                crv = testKeyAlg.ToJwkCrvString(),
                kty = "OKP",
                x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
            }));

            //Act
            string actual = await KeyApi.GetJwkPublicFromKeyAsync(
                testKeyHandle,
                testKeyAlg);

            //Assert
            _ = actual.Should().Be(jwkPublic);
        }

        private static IEnumerable<TestCaseData> GetJwkPublicFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, "{\"crv\":\"BLS12381_G1\",\"kty\":\"OKP\",\"x\":\"h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV\"}")
                .SetName("GetJwkPublicFromKeyAsync returns a string with the jwk public for a given key.");
        }

        [Test, TestCaseSource(nameof(GetJwkPublicFromKeyAsyncErrorCases)), Category("Get")]
        public async Task GetJwkPublicFromKeyAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            string testSeed = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeed,
                    testSeedMethod);

            //Act
            Func<Task> action = async () => await KeyApi.GetJwkPublicFromKeyAsync(
                testKeyHandle,
                testKeyAlg);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetJwkPublicFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.A128CBC_HS256)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with A128CBC_HS256.");
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with A128GCM.");
            yield return new TestCaseData(KeyAlg.A128KW)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with A128KW.");
            yield return new TestCaseData(KeyAlg.A256CBC_HS512)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with A256CBC_HS512.");
            yield return new TestCaseData(KeyAlg.A256GCM)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with A256GCM.");
            yield return new TestCaseData(KeyAlg.A256KW)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with A256KW.");
            yield return new TestCaseData(KeyAlg.C20P)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with C20P.");
            yield return new TestCaseData(KeyAlg.XC20P)
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the key was created with XC20P.");
        }
        #endregion

        #region GetJwkSecretFromKeyAsync
        [Test, TestCaseSource(nameof(GetJwkSecretFromKeyAsyncCases)), Category("Get")]
        public async Task GetJwkSecretFromKeyAsyncTests(KeyAlg testKeyAlg, int secretLength)
        {
            //Arrange
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = testKeyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Act
            byte[] actual = await KeyApi.GetJwkSecretFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = actual.Length.Should().Be(secretLength);
        }

        private static IEnumerable<TestCaseData> GetJwkSecretFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, 104)
                .SetName("GetJwkSecretFromKeyAsync returns a byte array with the jwk secret for a given key.");
        }

        [Test, TestCaseSource(nameof(GetJwkSecretFromKeyAsyncErrorCases)), Category("Get")]
        public async Task GetJwkSecretFromKeyAsyncErrorTests()
        {
            //Arrange
            IntPtr testKeyHandle = new();

            //Act
            Func<Task> action = async () => await KeyApi.GetJwkSecretFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetJwkSecretFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("GetJwkSecretFromKeyAsync throws an AriesAskarException if the handle points to invalid data.");
        }
        #endregion

        #region GetJwkThumbprintFromKeyAsync
        [Test, TestCaseSource(nameof(GetJwkThumbprintFromKeyAsyncCases)), Category("Get")]
        public async Task GetJwkThumbprintFromKeyAsyncTests(KeyAlg testKeyAlg, string thumbprint)
        {
            //Arrange
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromJwkAsync(
                JsonConvert.SerializeObject(new
                {
                    crv = testKeyAlg.ToJwkCrvString(),
                    kty = "OKP",
                    x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
                }));

            //Act
            string actual = await KeyApi.GetJwkThumbprintFromKeyAsync(
                testKeyHandle,
                testKeyAlg);

            //Assert
            _ = actual.Should().Be(thumbprint);
        }

        private static IEnumerable<TestCaseData> GetJwkThumbprintFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, "CEHoH9pekaE1pelnkF_goAxKl2K7HVgBHr7tW8lUkDI")
                .SetName("GetJwkThumbprintFromKeyAsync returns a string with the thumbprint for a given key.");
        }

        [Test, TestCaseSource(nameof(GetJwkThumbprintFromKeyAsyncErrorCases)), Category("Get")]
        public async Task GetJwkThumbprintFromKeyAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            IntPtr testKeyHandle = new();

            //Act
            Func<Task> action = async () => await KeyApi.GetJwkThumbprintFromKeyAsync(
                testKeyHandle,
                testKeyAlg);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetJwkThumbprintFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1)
                .SetName("GetJwkThumbprintFromKeyAsync throws an AriesAskarException if the handle points to invalid data.");
        }
        #endregion
        #endregion

        #region Aead
        #region GetAeadRandomNonceFromKeyAsync
        [Test, TestCaseSource(nameof(GetAeadRandomNonceFromKeyAsyncCases)), Category("Aead")]
        public async Task GetAeadRandomNonceFromKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);

            //Act
            byte[] actual = await KeyApi.GetAeadRandomNonceFromKeyAsync(
                testHandle);

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        private static IEnumerable<TestCaseData> GetAeadRandomNonceFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("GetAeadRandomNonceFromKeyAsync call returns the handle of the nonce.");
        }

        [Test, TestCaseSource(nameof(GetAeadRandomNonceFromKeyAsyncErrorCases)), Category("Aead")]
        public async Task GetAeadRandomNonceFromKeyAsyncErrorTests()
        {
            //Arrange
            //IntPtr testKeyHandle = await KeyApi.CreateKeyFromJwkAsync(
            //    JsonConvert.SerializeObject(new
            //    {
            //        crv = KeyAlg.BLS12_381_G1.ToJwkCrvString(),
            //        kty = "OKP",
            //        x = "h56eYI8Qkq5hitICb-ik8wRTzcn6Fd4iY8aDNVc9q1xoPS3lh4DB_B4wNtar1HrV"
            //    }));
            IntPtr testKeyHandle = new();
            //Act
            Func<Task> action = async () => await KeyApi.GetAeadRandomNonceFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetAeadRandomNonceFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("GetAeadRandomNonceFromKeyAsync throws an AriesAskarException if the handle points to invalid data.");
        }
        #endregion

        #region GetAeadParamsFromKeyAsync
        [Test, TestCaseSource(nameof(GetAeadParamsFromKeyAsyncCases)), Category("Aead")]
        public async Task GetAeadParamsFromKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);

            //Act
            (uint actualNonceLen, uint actualTagLen) = await KeyApi.GetAeadParamsFromKeyAsync(
                testHandle);

            //Assert
            _ = actualNonceLen.Should().NotBe(0);
            _ = actualTagLen.Should().NotBe(0);
        }

        private static IEnumerable<TestCaseData> GetAeadParamsFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("GetAeadParamsFromKeyAsync call returns the handle of the nonce.");
        }

        [Test, TestCaseSource(nameof(GetAeadParamsFromKeyAsyncErrorCases)), Category("Aead")]
        public async Task GetAeadParamsFromKeyAsyncErrorTests()
        {
            //Arrange
            IntPtr testHandle = new();

            //Act
            Func<Task> action = async () => await KeyApi.GetAeadParamsFromKeyAsync(
                testHandle);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetAeadParamsFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("GetAeadParamsFromKeyAsync throws an AriesAskarException if the handle points to invalid data.");
        }
        #endregion

        #region GetAeadPaddingFromKeyAsync
        [Test, TestCaseSource(nameof(GetAeadPaddingFromKeyAsyncCases)), Category("Aead")]
        public async Task GetAeadPaddingFromKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);
            long testMsgLen = 10;

            //Act
            int actual = await KeyApi.GetAeadPaddingFromKeyAsync(
                testHandle,
                testMsgLen);

            //Assert
            _ = actual.Should().Be(0);
        }

        private static IEnumerable<TestCaseData> GetAeadPaddingFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("GetAeadPaddingFromKeyAsyncCases call returns the padding.");
        }

        [Test, TestCaseSource(nameof(GetAeadPaddingFromKeyAsyncErrorCases)), Category("Aead")]
        public async Task GetAeadPaddingFromKeyAsyncErrorTests()
        {
            //Arrange
            IntPtr testHandle = new();
            long testMsgLen = 10;
            //Act
            Func<Task> action = async () => await KeyApi.GetAeadPaddingFromKeyAsync(
                testHandle,
                testMsgLen);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetAeadPaddingFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("GetAeadPaddingFromKeyAsync throws an AriesAskarException if the handle points to invalid data.");
        }
        #endregion

        #region EncryptKeyWithAeadAsync
        [Test, TestCaseSource(nameof(EncryptKeyWithAeadAsyncCases)), Category("Aead")]
        public async Task EncryptKeyWithAeadAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);
            string testMessage = "testMessage";
            byte[] testNonce = await KeyApi.GetAeadRandomNonceFromKeyAsync(testHandle);
            string testAad = "testAad";

            //Act
            (byte[] value, _, _) = await KeyApi.EncryptKeyWithAeadAsync(
                testHandle,
                testMessage,
                testNonce,
                testAad);

            //Assert
            _ = value.Length.Should().NotBe(0);
        }

        private static IEnumerable<TestCaseData> EncryptKeyWithAeadAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("EncryptKeyWithAeadAsync call encrypts with key algorithm A128GCM.");
            yield return new TestCaseData(KeyAlg.A256GCM)
                .SetName("EncryptKeyWithAeadAsync call encrypts with key algorithm A256GCM.");
            yield return new TestCaseData(KeyAlg.A128CBC_HS256)
                .SetName("EncryptKeyWithAeadAsync call encryptswith key algorithm A128CBC_HS256.");
            yield return new TestCaseData(KeyAlg.A256CBC_HS512)
                .SetName("EncryptKeyWithAeadAsync call encrypts with key algorithm A256CBC_HS512.");
            yield return new TestCaseData(KeyAlg.C20P)
                .SetName("EncryptKeyWithAeadAsync call encrypts with key algorithm C20P.");
            yield return new TestCaseData(KeyAlg.XC20P)
                .SetName("EncryptKeyWithAeadAsync call encrypts with key algorithm XC20P.");
        }

        [Test, TestCaseSource(nameof(EncryptKeyWithAeadAsyncErrorCases)), Category("Aead")]
        public async Task EncryptKeyWithAeadAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);
            string testMessage = "testMessage";
            byte[] testNonce = Array.Empty<byte>();
            string testAad = "testAad";

            //Act
            Func<Task> action = async () => await KeyApi.EncryptKeyWithAeadAsync(
                testHandle,
                testMessage,
                testNonce,
                testAad);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> EncryptKeyWithAeadAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.P256)
                .SetName("EncryptKeyWithAeadAsync throws an AriesAskarException if the provided key algorithm is not A128GCM, A256GCM, A128CBC_HS256, A256CBC_HS512, C20P or XC20P.");
        }
        #endregion

        #region DecryptKeyWithAeadAsync

        [Test, TestCaseSource(nameof(DecryptKeyWithAeadAsyncCases)), Category("Aead")]
        public async Task DecryptKeyWithAeadAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
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

        private static IEnumerable<TestCaseData> DecryptKeyWithAeadAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM)
                .SetName("DecryptKeyWithAeadAsync call decrypt with key algorithm A128GCM.");
            yield return new TestCaseData(KeyAlg.A256GCM)
                .SetName("DecryptKeyWithAeadAsync call decrypts with key algorithm A256GCM.");
            yield return new TestCaseData(KeyAlg.A128CBC_HS256)
                .SetName("DecryptKeyWithAeadAsync call decrypts with key algorithm A128CBC_HS256.");
            yield return new TestCaseData(KeyAlg.A256CBC_HS512)
                .SetName("DecryptKeyWithAeadAsync call decrypts with key algorithm A256CBC_HS512.");
            yield return new TestCaseData(KeyAlg.C20P)
                .SetName("DecryptKeyWithAeadAsync call decrypts with key algorithm C20P.");
            yield return new TestCaseData(KeyAlg.XC20P)
                .SetName("DecryptKeyWithAeadAsync call decrypts with key algorithm XC20P.");
        }

        [Test, TestCaseSource(nameof(DecryptKeyWithAeadAsyncErrorCases)), Category("Aead")]
        public async Task DecryptKeyWithAeadAsyncErrorTests()
        {
            //Arrange
            IntPtr testHandle = new();
            byte[] testValue = Array.Empty<byte>();
            byte[] testNonce = Array.Empty<byte>();
            byte[] testTag = Array.Empty<byte>();
            string testAad = "testAad";
            //Act
            Func<Task> action = async () => await KeyApi.DecryptKeyWithAeadAsync(
                testHandle,
                testValue,
                testNonce,
                testTag,
                testAad);

            //Assert
            _ = await action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> DecryptKeyWithAeadAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("DecryptKeyWithAeadAsync throws an AriesAskarException if the handle points to invalid data.");
        }
        #endregion

        #endregion

        #region Crypto

        #region CreateCryptoBoxRandomNonceAsync

        [Test, TestCaseSource(nameof(CreateCryptoBoxRandomNonceAsyncCases)), Category("Crypto")]
        public async Task CreateCryptoBoxRandomNonceAsyncTests(KeyAlg testKeyAlg)
        {
            KeyAlg keyRecipientAlg = testKeyAlg;
            bool testEphemeral = true;
            _ = await KeyApi.CreateKeyAsync(
                    keyRecipientAlg,
                    testEphemeral);

            KeyAlg keySenderAlg = testKeyAlg;
            _ = await KeyApi.CreateKeyAsync(
                    keySenderAlg,
                    testEphemeral);

            //Act
            byte[] actual = await KeyApi.CreateCryptoBoxRandomNonceAsync();

            //Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        private static IEnumerable<TestCaseData> CreateCryptoBoxRandomNonceAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.X25519)
                .SetName("CreateCryptoBoxRandomNonceAsync works and creates a nonce.");
        }

        #endregion

        #region CryptoBoxAsync

        [Test, TestCaseSource(nameof(CryptoBoxAsyncCases)), Category("Crypto")]
        public async Task CryptoBoxAsyncTests(KeyAlg testKeyAlg)
        {
            KeyAlg keyRecipientAlg = testKeyAlg;
            bool testEphemeral = true;
            IntPtr testRecipientHandle = await KeyApi.CreateKeyAsync(
                    keyRecipientAlg,
                    testEphemeral);

            KeyAlg keySenderAlg = testKeyAlg;
            IntPtr testSenderHandle = await KeyApi.CreateKeyAsync(
                    keySenderAlg,
                    testEphemeral);

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

        private static IEnumerable<TestCaseData> CryptoBoxAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.X25519)
                .SetName("CryptoBoxAsync works and encrypts the message with key algorithm X25519.");
        }

        [Test, TestCaseSource(nameof(CryptoBoxAsyncErrorCases)), Category("Crypto")]
        public async Task CryptoBoxAsyncErrorTests(KeyAlg testKeyAlgRecip, KeyAlg testKeyAlgSend)
        {
            // Arrange
            KeyAlg keyRecipientAlg = testKeyAlgRecip;
            bool testEphemeral = true;
            IntPtr testRecipientHandle = await KeyApi.CreateKeyAsync(
                    keyRecipientAlg,
                    testEphemeral);

            KeyAlg keySenderAlg = testKeyAlgSend;
            IntPtr testSenderHandle = await KeyApi.CreateKeyAsync(
                    keySenderAlg,
                    testEphemeral);

            string testMessage = "testMessage";
            byte[] testNonce = await KeyApi.CreateCryptoBoxRandomNonceAsync();

            //Act
            Func<Task<byte[]>> func = async () => await KeyApi.CryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testMessage,
                testNonce);

            //Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        private static IEnumerable<TestCaseData> CryptoBoxAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.A256GCM, KeyAlg.A256GCM)
                .SetName("CryptoBoxAsync throws an AriesAskarException if the provided key algorithm is not X25519.");
        }
        #endregion

        #region OpenCryptoBoxAsync

        [Test, TestCaseSource(nameof(OpenCryptoBoxAsyncCases)), Category("Crypto")]
        public async Task OpenCryptoBoxAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            KeyAlg keyRecipientAlg = KeyAlg.X25519;
            bool testEphemeral = true;
            IntPtr testRecipientHandle = await KeyApi.CreateKeyAsync(
                    keyRecipientAlg,
                    testEphemeral);

            KeyAlg keySenderAlg = KeyAlg.X25519;
            IntPtr testSenderHandle = await KeyApi.CreateKeyAsync(
                    keySenderAlg,
                    testEphemeral);

            string testMessage = "testMessage";
            byte[] testNonce = await KeyApi.CreateCryptoBoxRandomNonceAsync();

            byte[] testBox = await KeyApi.CryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testMessage,
                testNonce);

            //Act
            string actual = await KeyApi.OpenCryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testBox,
                testNonce);

            //Assert
            _ = actual.Should().Be(testMessage);
        }

        private static IEnumerable<TestCaseData> OpenCryptoBoxAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.X25519)
                .SetName("OpenCryptoBoxAsync works and decrypts the message.");
        }

        [Test, TestCaseSource(nameof(OpenCryptoBoxAsyncErrorCases)), Category("Crypto")]
        public async Task OpenCryptoBoxAsyncErrorTests()
        {
            // Arrange
            KeyAlg keyRecipientAlg = KeyAlg.X25519;
            bool testEphemeral = true;
            IntPtr testRecipientHandle = await KeyApi.CreateKeyAsync(
                    keyRecipientAlg,
                    testEphemeral);

            KeyAlg keySenderAlg = KeyAlg.X25519;
            IntPtr testSenderHandle = await KeyApi.CreateKeyAsync(
                    keySenderAlg,
                    testEphemeral);

            string testMessage = "testMessage";
            byte[] testNonce = await KeyApi.CreateCryptoBoxRandomNonceAsync();

            byte[] testBox = await KeyApi.CryptoBoxAsync(
                testRecipientHandle,
                testSenderHandle,
                testMessage,
                testNonce);

            //Act
            Func<Task<string>> func = async () => await KeyApi.OpenCryptoBoxAsync(
                new IntPtr(),
                new IntPtr(),
                testBox,
                testNonce);

            //Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        private static IEnumerable<TestCaseData> OpenCryptoBoxAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("OpenCryptoBoxAsync throws an AriesAskarException if the provided handles are invalid.");
        }
        #endregion

        #region SealCryptoBoxAsync

        [Test, TestCaseSource(nameof(SealCryptoBoxAsyncCases)), Category("Crypto")]
        public async Task SealCryptoBoxAsyncTests(KeyAlg testKeyAlg)
        {
            // Arrange
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);

            string testMessage = "testMessage";

            // Act
            byte[] actual = await KeyApi.SealCryptoBoxAsync(
                testKeyHandle,
                testMessage);

            // Assert
            _ = ByteBuffer.Create(actual).len.Should().NotBe(0);
        }

        private static IEnumerable<TestCaseData> SealCryptoBoxAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.X25519)
                .SetName("SealCryptoBoxAsync works and encrypts the message.");
        }

        [Test, TestCaseSource(nameof(SealCryptoBoxAsyncErrorCases)), Category("Crypto")]
        public async Task SealCryptoBoxAsynccErrorTests()
        {
            //Arrange
            IntPtr testKeyHandle = new();

            string testMessage = "testMessage";

            //Act
            Func<Task<byte[]>> func = async () => await KeyApi.SealCryptoBoxAsync(
                testKeyHandle,
                testMessage);

            //Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        private static IEnumerable<TestCaseData> SealCryptoBoxAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("SealCryptoBoxAsync throws an AriesAskarException if the provided handles are invalid.");
        }
        #endregion

        #region OpenSealCryptoBoxAsync

        [Test, TestCaseSource(nameof(OpenSealCryptoBoxAsyncCases)), Category("Crypto")]
        public async Task OpenSealCryptoBoxAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.X25519;
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            string testMessage = "testMessage";

            byte[] sealedBox = await KeyApi.SealCryptoBoxAsync(
                testKeyHandle,
                testMessage);
            //Act
            string actual = await KeyApi.OpenSealCryptoBoxAsync(
                testKeyHandle,
                sealedBox);

            //Assert
            _ = actual.Should().Be(testMessage);
        }

        private static IEnumerable<TestCaseData> OpenSealCryptoBoxAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.X25519)
                .SetName("OpenSealCryptoBoxAsync works and decrypts the message.");
        }

        [Test, TestCaseSource(nameof(OpenSealCryptoBoxAsyncErrorCases)), Category("Crypto")]
        public async Task OpenSealCryptoBoxAsyncErrorTests()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.X25519;
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            string testMessage = "testMessage";

            byte[] sealedBox = await KeyApi.SealCryptoBoxAsync(
                testKeyHandle,
                testMessage);

            //Act
            Func<Task<string>> func = async () => await KeyApi.OpenSealCryptoBoxAsync(
                new IntPtr(),
                sealedBox);

            //Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        private static IEnumerable<TestCaseData> OpenSealCryptoBoxAsyncErrorCases()
        {
            yield return new TestCaseData()
                .SetName("OpenSealCryptoBoxAsync throws an AriesAskarException if the provided handles are invalid.");
        }
        #endregion

        #endregion

        #region Utils
        [Test, TestCase(TestName = "ConvertKeyAsync call returns request handle."), Category("Utils")]
        public async Task ConvertKeyAsyncTests()
        {
            //Arrange
            KeyAlg oldKeyAlg = KeyAlg.ED25519;
            bool testEphemeral = true;
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

        [Test, TestCase(TestName = "FreeKeyAsync call returns request handle."), Category("Utils")]
        public async Task FreeKeyAsyncTests()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.ED25519;
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            //Act
            string keyBeforeFree = await KeyApi.GetAlgorithmFromKeyAsync(testKeyHandle);
            Func<Task> actual = async () => await KeyApi.FreeKeyAsync(testKeyHandle);

            //Assert
            _ = actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "SignMessageFromKeyAsync call returns request handle."), Category("Utils")]
        public async Task SignMessageFromKeyAsyncTests()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.K256;
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);
            byte[] testMessage = ByteBuffer.Create("testMessage").Decode();
            SignatureType testSigType = SignatureType.ES256K;

            //Act
            byte[] actual = await KeyApi.SignMessageFromKeyAsync(
                testKeyHandle,
                testMessage,
                testSigType);

            //Assert
            _ = actual.Should().NotBeEmpty();
        }

        private static IEnumerable<TestCaseData> VerifySignatureFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.K256, SignatureType.ES256K)
                .SetName("VerifySignatureFromKeyAsync call returns request handle for keyAlg K256 and SigType ES256K.");
            yield return new TestCaseData(KeyAlg.P256, SignatureType.ES256)
                .SetName("VerifySignatureFromKeyAsync call returns request handle for keyAlg P256 and SigType ES256.");
            yield return new TestCaseData(KeyAlg.ED25519, SignatureType.EdDSA)
                .SetName("VerifySignatureFromKeyAsync call returns request handle for keyAlg ED25519 and SigType EdDSA.");
        }

        [Test, TestCaseSource(nameof(VerifySignatureFromKeyAsyncCases)), Category("Utils")]
        public async Task VerifySignatureFromKeyAsyncTests(KeyAlg keyAlg, SignatureType sigType)
        {
            //Arrange
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);
            byte[] testMessage = ByteBuffer.Create("testMessage").Decode();

            //Act
            byte[] testSignature = await KeyApi.SignMessageFromKeyAsync(
                testKeyHandle,
                testMessage,
                sigType);

            bool actual = await KeyApi.VerifySignatureFromKeyAsync(
                testKeyHandle,
                testMessage,
                testSignature,
                sigType);

            //Assert
            _ = actual.Should().BeTrue();
        }

        [Test, TestCase(TestName = "WrapKeyAsync call returns request handle."), Category("Utils")]
        public async Task WrapKeyAsyncTests()
        {
            //Arrange
            KeyAlg keyAlg = KeyAlg.A256GCM;
            bool testEphemeral = true;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    keyAlg,
                    testEphemeral);

            KeyAlg otherKeyAlg = KeyAlg.A128KW;
            IntPtr otherTestKeyHandle = await KeyApi.CreateKeyAsync(
                    otherKeyAlg,
                    testEphemeral);

            byte[] testNonce = await KeyApi.GetAeadRandomNonceFromKeyAsync(testKeyHandle);

            //Act
            (byte[] value, _, _) = await KeyApi.WrapKeyAsync(
                testKeyHandle,
                otherTestKeyHandle,
                testNonce);

            //Assert
            _ = value.Should().NotBeEmpty();
        }

        [Test, TestCase(TestName = "EcdhEs SenderWrapKeyAsync and ReceiverUnwrapKeyAsyncworks works and returns the input message."), Category("Utils")]
        public async Task EcdhEsWrapUnwrapKeyAsyncWorks()
        {
            //Arrange
            bool testEphemeral = true;
            KeyAlg keyAlg = KeyAlg.X25519;
            string algId = "ECDH-ES";
            string enc = "A256GCM";
            string apu = "Alice";
            string apv = "Bob";
            EcdhEs ecdhEs = new(algId, apu, apv);
            string msgSend = "testMessage";

            IntPtr keyBob = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            IntPtr keyEphemeral = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            string jwkEphemeral = await KeyApi.GetJwkPublicFromKeyAsync(keyEphemeral, keyAlg);

            KeyAlg keyAlgCek = KeyAlg.A256GCM;
            IntPtr cek = await KeyApi.CreateKeyAsync(keyAlgCek, testEphemeral);
            string testAeadContent = $"{{ \"alg\":{algId}, \"enc\":{enc}, \"apu\":{apu}, \"apv\":{apv}, \"epk\":{jwkEphemeral}}}";

            //Act
            (byte[] testAeadCiphertext, byte[] testAeadTag, byte[] testAeadNonce) = await KeyApi.EncryptKeyWithAeadAsync(cek, msgSend, null, testAeadContent);

            (byte[] encryptCiphertext, _, _) = await ecdhEs.SenderWrapKeyAsync(
                KeyAlg.A128KW,
                keyEphemeral,
                keyBob,
                cek);

            IntPtr cekReceiver = await ecdhEs.ReceiverUnwrapKeyAsync(
                KeyAlg.A128KW,
                keyAlgCek,
                keyEphemeral,
                keyBob,
                encryptCiphertext);

            string msgReceive = Decoder.GetString(await KeyApi.DecryptKeyWithAeadAsync(cekReceiver, testAeadCiphertext, testAeadNonce, testAeadTag, testAeadContent));
            string cekSecret = Decoder.GetString(await KeyApi.GetJwkSecretFromKeyAsync(cek));
            string cekReceiverSecret = Decoder.GetString(await KeyApi.GetJwkSecretFromKeyAsync(cekReceiver));

            //Assert
            _ = msgReceive.Should().Be(msgSend);
            _ = cekSecret.Should().Be(cekReceiverSecret);
        }

        [Test, TestCase(TestName = "EcdhEs EncryptDirectAsync and DecryptDirectAsync works and returns the input message."), Category("Utils")]
        public async Task EcdhEsEncryptDecryptDirectAsyncWorks()
        {
            //Arrange
            bool testEphemeral = true;
            KeyAlg keyAlg = KeyAlg.P256;
            KeyAlg directKeyAlg = KeyAlg.A256GCM;
            string algId = "ECDH-ES";
            string enc = "A256GCM";
            string apu = "Alice";
            string apv = "Bob";
            EcdhEs ecdhEs = new(algId, apu, apv);
            string msgSend = "testMessage";

            IntPtr keyBob = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            IntPtr keyEphemeral = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            string jwkEphemeral = await KeyApi.GetJwkPublicFromKeyAsync(keyEphemeral, keyAlg);

            KeyAlg keyAlgCek = KeyAlg.A256GCM;
            _ = await KeyApi.CreateKeyAsync(keyAlgCek, testEphemeral);
            string testAeadContent = $"{{ \"alg\":{algId}, \"enc\":{enc}, \"apu\":{apu}, \"apv\":{apv}, \"epk\":{jwkEphemeral}}}";

            //Act
            (byte[] testCiphertext, byte[] testTag, byte[] testNonce) = await ecdhEs.EncryptDirectAsync(
                directKeyAlg,
                keyEphemeral,
                keyBob,
                msgSend,
                null,
                testAeadContent);

            string msgReceived = await ecdhEs.DecryptDirectAsync(
                directKeyAlg,
                keyEphemeral,
                keyBob,
                testCiphertext,
                testNonce,
                testTag,
                testAeadContent);

            //Assert
            _ = msgReceived.Should().Be(msgSend);
        }

        [Test, TestCase(TestName = "Ecdh1Pu SenderWrapKeyAsync and ReceiverUnwrapKeyAsyncworks works and returns the input message."), Category("Utils")]
        public async Task Ecdh1PuWrapUnwrapKeyAsyncWorks()
        {
            //Arrange
            bool testEphemeral = true;
            KeyAlg keyAlg = KeyAlg.X25519;
            string algId = "ECDH-1PU";
            string enc = "A256GCM";
            string apu = "Alice";
            string apv = "Bob";
            Ecdh1Pu ecdh1Pu = new(algId, apu, apv);
            string msgSend = "testMessage";

            IntPtr keyAlice = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            IntPtr keyBob = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            IntPtr keyEphemeral = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            string jwkEphemeral = await KeyApi.GetJwkPublicFromKeyAsync(keyEphemeral, keyAlg);

            KeyAlg keyAlgCek = KeyAlg.A256GCM;
            IntPtr cek = await KeyApi.CreateKeyAsync(keyAlgCek, testEphemeral);
            string testAeadContent = $"{{ \"alg\":{algId}, \"enc\":{enc}, \"apu\":{apu}, \"apv\":{apv}, \"epk\":{jwkEphemeral}}}";

            //Act
            (byte[] testAeadCiphertext, byte[] testAeadTag, byte[] testAeadNonce) = await KeyApi.EncryptKeyWithAeadAsync(cek, msgSend, null, testAeadContent);

            (byte[] encryptCiphertext, _, _) = await ecdh1Pu.SenderWrapKeyAsync(
                KeyAlg.A128KW,
                keyEphemeral,
                keyAlice,
                keyBob,
                cek,
                testAeadTag);

            IntPtr cekReceiver = await ecdh1Pu.ReceiverUnwrapKeyAsync(
                KeyAlg.A128KW,
                keyAlgCek,
                keyEphemeral,
                keyAlice,
                keyBob,
                testAeadTag,
                encryptCiphertext);


            string msgReceive = Decoder.GetString(await KeyApi.DecryptKeyWithAeadAsync(cekReceiver, testAeadCiphertext, testAeadNonce, testAeadTag, testAeadContent));
            string cekSecret = Decoder.GetString(await KeyApi.GetJwkSecretFromKeyAsync(cek));
            string cekReceiverSecret = Decoder.GetString(await KeyApi.GetJwkSecretFromKeyAsync(cekReceiver));

            //Assert
            _ = msgReceive.Should().Be(msgSend);
            _ = cekSecret.Should().Be(cekReceiverSecret);
        }

        [Test, TestCase(TestName = "Ecdh1Pu EncryptDirectAsync and DecryptDirectAsync works and returns the input message."), Category("Utils")]
        public async Task Ecdh1PuEncryptDecryptDirectAsyncWorks()
        {
            //Arrange
            bool testEphemeral = true;
            KeyAlg keyAlg = KeyAlg.P256;
            KeyAlg directKeyAlg = KeyAlg.A256GCM;
            string algId = "ECDH-1PU";
            string enc = "A256GCM";
            string apu = "Alice";
            string apv = "Bob";
            Ecdh1Pu ecdh1Pu = new(algId, apu, apv);
            string msgSend = "testMessage";

            IntPtr keyAlice = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            IntPtr keyBob = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            IntPtr keyEphemeral = await KeyApi.CreateKeyAsync(keyAlg, testEphemeral);
            string jwkEphemeral = await KeyApi.GetJwkPublicFromKeyAsync(keyEphemeral, keyAlg);

            KeyAlg keyAlgCek = KeyAlg.A256GCM;
            _ = await KeyApi.CreateKeyAsync(keyAlgCek, testEphemeral);
            string testAeadContent = $"{{ \"alg\":{algId}, \"enc\":{enc}, \"apu\":{apu}, \"apv\":{apv}, \"epk\":{jwkEphemeral}}}";

            //Act
            (byte[] testCiphertext, byte[] testTag, byte[] testNonce) = await ecdh1Pu.EncryptDirectAsync(
                directKeyAlg,
                keyEphemeral,
                keyAlice,
                keyBob,
                msgSend,
                null,
                testAeadContent);

            string msgReceived = await ecdh1Pu.DecryptDirectAsync(
                directKeyAlg,
                keyEphemeral,
                keyAlice,
                keyBob,
                testCiphertext,
                testNonce,
                testTag,
                testAeadContent);

            //Assert
            _ = msgReceived.Should().Be(msgSend);
        }
        #endregion
    }
}
