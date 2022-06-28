using aries_askar_dotnet.aries_askar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.aries_askar
{
    public class StoreApiTests
    {
        Dictionary<string, object> testEntry;
        string testSpecUri;
        KeyMethod testKeyMethod;
        string testPassKey;
        string testProfile;
        string testSeed;
        bool testRecreate;
        bool testAsTransactions;

        //Dictionary<string, object> testEntry;
        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            testEntry = new()
            {
                { "name", "testName" },
                { "category", "testCategory" },
                { "value", "testValue" },
                {"tags", new Dictionary<string, object>
                    {
                        { "~plaintag", "a" },
                        { "enctag", new Dictionary<string, string>
                            {
                                { "b", "c" }
                            }
                        }
                    }
                },
            };

            testSpecUri = "sqlite://:memory:";
            testSeed = "testseed000000000000000000000001";
            testPassKey = await StoreApi.StoreGenerateRawKeyAsync(testSeed);
            testKeyMethod = KeyMethod.RAW;  //kdf:argon2i   //none
            testProfile = "testProfile";
            testRecreate = true;
            testAsTransactions = true;
        }

        [Test, TestCase(TestName = "StoreGenerateRawKey call returns result string.")]
        public async Task StoreGenerateRawKey()
        {
            //Arrange
            //string testSeed = "testseed000000000000000000000001";
            //Act
            string actual = await StoreApi.StoreGenerateRawKeyAsync(testSeed);

            //Assert
            actual.Should().NotBe("");
        }

        private static IEnumerable<TestCaseData> CreateCasesStoreProvisioningWorks()
        {
            string seed = "testseed000000000000000000000001";
            string passKey = StoreApi.StoreGenerateRawKeyAsync(seed).GetAwaiter().GetResult();

            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", true)
                .SetName("StoreProvision call returns store handle with keyMethod 'raw' and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", true)
                .SetName("StoreProvision call returns store handle with keyMethod 'none' and recreate true.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", true)
                .SetName("StoreProvision call returns store handle with keyMethod 'kdf_argon2i' and recreate true.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", false)
                .SetName("StoreProvision call returns store handle with keyMethod 'raw' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", false)
                .SetName("StoreProvision call returns store handle with keyMethod 'none' and recreate false.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", false)
                .SetName("StoreProvision call returns store handle with keyMethod 'kdf_argon2i' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, true)
                .SetName("StoreProvision call returns store handle with keyMethod 'none' and no passkey and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, false)
                .SetName("StoreProvision call returns store handle with keyMethod 'none' and no passkey and recreate false.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, null, true)
                .SetName("StoreProvision call returns store handle with keyMethod 'raw' and no profile.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, null, true)
                .SetName("StoreProvision call returns store handle with keyMethod 'none' and no profile.");
        }

        [Test, TestCaseSource(nameof(CreateCasesStoreProvisioningWorks))]
        public async Task StoreProvisionReturnsHandle(KeyMethod keyMethod, string passKey, string profile, bool recreate)
        {
            //Arrange
            //Act
            Store actual = await StoreApi.ProvisionAsync(testSpecUri, keyMethod, passKey, profile, recreate);

            //Assert
            actual.storeHandle.Should().NotBe((IntPtr)0);
            
        }

        private static IEnumerable<TestCaseData> CreateCasesStoreProvisioningThrows()
        {
            string seed = "testseed000000000000000000000001";
            string passKey = StoreApi.StoreGenerateRawKeyAsync(seed).GetAwaiter().GetResult();

            yield return new TestCaseData(KeyMethod.RAW, null, "testProfile", true)
                .SetName("StoreProvision call throws with keyMethod 'raw' and no passkey and recreate true.");
            yield return new TestCaseData(KeyMethod.RAW, null, "testProfile", false)
                            .SetName("StoreProvision call throws with keyMethod 'raw' and no passkey and recreate false.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, null, "testProfile", true)
                .SetName("StoreProvision call throws with keyMethod 'kdf_argon2i' and given passkey and recreate true.");
        }

        [Test, TestCaseSource(nameof(CreateCasesStoreProvisioningThrows))]
        public async Task StoreProvisionThrows(KeyMethod keyMethod, string passKey, string profile, bool recreate)
        {
            //Arrange
            //Act
            Func<Task> actual = async() => await StoreApi.ProvisionAsync(testSpecUri, keyMethod, passKey, profile, recreate);

            //Assert
            await actual.Should().ThrowAsync<Exception>();

        }

        [Test, TestCase(TestName = "InsertAsync call works.")]
        public async Task SessionUpdate()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
            Session session = await store.StartSessionAsync(testProfile, testAsTransactions);

            //Act
            bool actual = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                null,
                //testEntry["tags"].ToString(), //TODO
                (long)999999);

            //Assert
            actual.Should().Be(true);
        }

        [Test, TestCase(TestName = "SessionCloseAsync call returns works.")]
        public async Task SessionClose()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
            Session session = await store.StartSessionAsync(testProfile, testAsTransactions);

            //Act
            bool actual = await session.RollbackAsync();

            //Assert
            actual.Should().Be(true);
        }
    }
}
