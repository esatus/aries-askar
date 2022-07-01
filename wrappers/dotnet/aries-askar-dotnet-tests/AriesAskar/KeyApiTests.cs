﻿using aries_askar_dotnet;
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
        #region CreateKeyAsync
        [Test, TestCaseSource(nameof(CreateKeyAsyncCases))]
        public async Task CreateKeyAsyncTests(KeyAlg testKeyAlg)
        {
            //Arrange
            byte testEphemeral = 1;

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

        [Test, TestCaseSource(nameof(CreateKeyAsyncErrorCases))]
        public async Task CreateKeyAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            byte testEphemeral = 1;

            //Act
            Func<Task> action = async () =>
            {
                await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);
            };

            //Assert
            _ = action.Should().ThrowAsync<AriesAskarException>();
        }

        private static IEnumerable<TestCaseData> CreateKeyAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.NONE)
                .SetName("CreateKeyAsync throws an AriesAskarException if no key algorithm is provided.");
        }
        #endregion

        #region CreateKeyFromSeedAsync
        [Test, TestCaseSource(nameof(CreateKeyFromSeedAsyncCases))]
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

        [Test, TestCaseSource(nameof(CreateKeyFromSeedAsyncErrorCases))]
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
            _ = action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSeedAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.NONE)
                .SetName("CreateKeyFromSeedAsync throws an AriesAskarException if the seed length is not exactly 32.");
        }
        #endregion

        #region CreateKeyFromJwkAsync
        [Test, TestCaseSource(nameof(CreateKeyFromJwkAsyncCases))]
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

        [Test, TestCaseSource(nameof(CreateKeyFromJwkAsyncErrorCases))]
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
            _ = action.Should().ThrowAsync<AriesAskarException>();
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
        [Test, TestCaseSource(nameof(CreateKeyFromPublicBytesAsyncCases))]
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

        [Test, TestCaseSource(nameof(CreateKeyFromPublicBytesAsyncErrorCases))]
        public async Task CreateKeyFromPublicBytesAsyncErrorTests(KeyAlg testKeyAlg)
        {
            //Arrange
            byte[] testPublicBytes = new byte[48] { 135, 158, 158, 96, 143, 16, 146, 174, 97, 138, 210, 2, 111, 232, 164, 243, 4, 83, 205, 201, 250, 21, 222, 34, 99, 198, 131, 53, 87, 61, 171, 92, 104, 61, 45, 229, 135, 128, 193, 252, 30, 48, 54, 214, 171, 212, 122, 213 };

            //Act
            Func<Task> action = async () => await KeyApi.CreateKeyFromPublicBytesAsync(
                testKeyAlg,
                testPublicBytes);

            //Assert
            _ = action.Should().ThrowAsync<Exception>();
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
        [Test, TestCaseSource(nameof(CreateKeyFromSecretBytesAsyncCases))]
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

        [Test, TestCaseSource(nameof(CreateKeyFromSecretBytesAsyncErrorCases))]
        public async Task CreateKeyFromSecretBytesAsyncErrorTests(KeyAlg testKeyAlg, byte[] testSecretBytes)
        {
            //Act
            Func<Task> action = async () => await KeyApi.CreateKeyFromSecretBytesAsync(
                testKeyAlg,
                testSecretBytes);

            //Assert
            _ = action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromSecretBytesAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, new byte[] { 188, 199, 70, 37, 50, 103, 100, 241, 231, 16, 97, 18, 220, 249, 108, 76 })
                .SetName("CreateKeyFromSecretBytesAsync throws an AriesAskarException if the key algorithm does not fit the provided secret bytes.");
        }
        #endregion

        #region CreateKeyFromKeyExchangeAsync
        [Test, TestCaseSource(nameof(CreateKeyFromKeyExchangeAsyncCases))]
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

        [Test, TestCaseSource(nameof(CreateKeyFromKeyExchangeAsyncErrorCases))]
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
            _ = action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateKeyFromKeyExchangeAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.P256, KeyAlg.K256)
                .SetName("CreateKeyFromKeyExchangeAsync throws an AriesAskarException if the provided keys did not use the same key algorithm.");
        }
        #endregion
        #endregion

        #region Get
        #region GetPublicBytesFromKeyAsync
        [Test, TestCaseSource(nameof(GetPublicBytesFromKeyAsyncCases))]
        public async Task GetPublicBytesFromKeyAsyncWorks(KeyAlg testKeyAlg)
        {
            //Arrange
            byte testEphemeral = 1;
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

        [Test, TestCaseSource(nameof(GetPublicBytesFromKeyAsyncErrorCases))]
        public async Task GetPublicBytesFromKeyAsyncErrorWorks(KeyAlg testKeyAlg)
        {
            //Arrange
            byte testEphemeral = 1;
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                testKeyAlg,
                testEphemeral);

            //Act
            Func<Task> action = async () => await KeyApi.GetPublicBytesFromKeyAsync(
                testKeyHandle);

            _ = action.Should().ThrowAsync<Exception>();
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
        [Test, TestCaseSource(nameof(GetSecretBytesFromKeyAsyncCases))]
        public async Task GetSecretBytesFromKeyAsyncWorks(KeyAlg testKeyAlg)
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

        [Test, TestCaseSource(nameof(GetSecretBytesFromKeyAsyncErrorCases))]
        public async Task GetSecretBytesFromKeyAsyncErrorWorks(KeyAlg testKeyAlg)
        {
            //Arrange
            string testSeed = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeed,
                    testSeedMethod);

            //Act
            Func<Task> action = async () => await KeyApi.GetSecretBytesFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = action.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> GetSecretBytesFromKeyAsyncErrorCases()
        {
            yield return new TestCaseData(KeyAlg.NONE)
                .SetName("GetSecretBytesFromKeyAsync throws an AriesAskarException if the key handle points to invalid data.");
        }
        #endregion
        [Test, TestCaseSource(nameof(GetAlgorithmFromKeyAsyncCases))]
        public async Task GetAlgorithmFromKeyAsyncWorks(KeyAlg testKeyAlg, string expectedName)
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
            _ = actual.Should().Be(expectedName);
        }

        private static IEnumerable<TestCaseData> GetAlgorithmFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, "bls12381g1")
                .SetName("GetAlgorithmFromKeyAsync returns a string with the name of an algorithm for a given key.");
            yield return new TestCaseData(KeyAlg.NONE, "")
                .SetName("GetAlgorithmFromKeyAsync throws an AriesAskarException if the key handle points to invalid data.");
        }

        [Test, TestCaseSource(nameof(GetEphemeralFromKeyAsyncCases))]
        public async Task GetEphemeralFromKeyAsyncWorks(KeyAlg testKeyAlg, byte testEphemeral)
        {
            //Arrange
            IntPtr testKeyHandle = await KeyApi.CreateKeyAsync(
                    testKeyAlg,
                    testEphemeral);

            //Act
            byte actual = await KeyApi.GetEphemeralFromKeyAsync(
                testKeyHandle);

            //Assert
            _ = actual.Should().Be(testEphemeral);
        }

        private static IEnumerable<TestCaseData> GetEphemeralFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.A128GCM, (byte)1)
                .SetName("GetEphemeralFromKeyAsync returns the ephemeral as a byte for a given key.");
            yield return new TestCaseData(KeyAlg.NONE, (byte)0)
                .SetName("GetEphemeralFromKeyAsync throws an AriesAskarException if the key handle points to invalid data.");
        }

        [Test, TestCaseSource(nameof(GetJwkPublicFromKeyAsyncCases))]
        public async Task GetJwkPublicFromKeyAsyncWorks(KeyAlg testKeyAlg, string jwkPublic)
        {
            //Arrange
            string testSeed = "testseed000000000000000000000001";
            SeedMethod testSeedMethod = SeedMethod.BlsKeyGen;
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromSeedAsync(
                    testKeyAlg,
                    testSeed,
                    testSeedMethod);

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
            yield return new TestCaseData(KeyAlg.A128GCM, "")
                .SetName("GetJwkPublicFromKeyAsync throws an AriesAskarException if the algorithm is not BLS12_381_G1,BLS12_381_G2 or BLS12_381_G1G2.");
        }

        [Test, TestCaseSource(nameof(GetJwkSecretFromKeyAsyncCases))]
        public async Task GetJwkSecretFromKeyAsyncWorks(KeyAlg testKeyAlg, int secretLength)
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
            _ = ByteBuffer.Create(actual).len.Should().Be(secretLength);
        }

        private static IEnumerable<TestCaseData> GetJwkSecretFromKeyAsyncCases()
        {
            yield return new TestCaseData(KeyAlg.BLS12_381_G1, 104)
                .SetName("GetJwkSecretFromKeyAsync returns a byte array with the jwk secret for a given key.");
            yield return new TestCaseData(KeyAlg.A128GCM, 0)
                .SetName("GetJwkSecretFromKeyAsync throws an AriesAskarException if the algorithm is not BLS12_381_G1,BLS12_381_G2 or BLS12_381_G1G2.");
        }

        [Test, TestCaseSource(nameof(GetJwkThumbprintFromKeyAsyncCases))]
        public async Task GetJwkThumbprintFromKeyAsyncWorks(KeyAlg testKeyAlg, string thumbprint)
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
            yield return new TestCaseData(KeyAlg.A128GCM, "")
                .SetName("GetJwkThumbprintFromKeyAsync throws an AriesAskarException if the algorithm is not BLS12_381_G1,BLS12_381_G2 or BLS12_381_G1G2.");
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
