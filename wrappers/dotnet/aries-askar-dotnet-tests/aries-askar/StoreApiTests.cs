using aries_askar_dotnet;
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

        #region store

        [Test, TestCase(TestName = "StoreGenerateRawKey call returns result string.")]
        public async Task StoreGenerateRawKeyWorks()
        {
            //Arrange

            //Act
            string actual = await StoreApi.StoreGenerateRawKeyAsync(testSeed);

            //Assert
            actual.Should().NotBe("");
        }

        #region provision and open
        private static IEnumerable<TestCaseData> CreateCasesStoreProvisioningWorks()
        {
            string seed = "testseed000000000000000000000001";
            string passKey = StoreApi.StoreGenerateRawKeyAsync(seed).GetAwaiter().GetResult();

            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", true)
                .SetName("StoreProvision call returns store with keyMethod 'raw' and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", true)
                .SetName("StoreProvision call returns store with keyMethod 'none' and recreate true.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", true)
                .SetName("StoreProvision call returns store with keyMethod 'kdf_argon2i' and recreate true.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", false)
                .SetName("StoreProvision call returns store with keyMethod 'raw' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", false)
                .SetName("StoreProvision call returns store with keyMethod 'none' and recreate false.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", false)
                .SetName("StoreProvision call returns store with keyMethod 'kdf_argon2i' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, true)
                .SetName("StoreProvision call returns store with keyMethod 'none' and no passkey and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, false)
                .SetName("StoreProvision call returns store with keyMethod 'none' and no passkey and recreate false.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, null, true)
                .SetName("StoreProvision call returns store with keyMethod 'raw' and no profile.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, null, true)
                .SetName("StoreProvision call returns store with keyMethod 'none' and no profile.");
        }

        [Test, TestCaseSource(nameof(CreateCasesStoreProvisioningWorks))]
        public async Task ProvisionAsyncWorks(KeyMethod keyMethod, string passKey, string profile, bool recreate)
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
        public async Task ProvisionAsyncThrows(KeyMethod keyMethod, string passKey, string profile, bool recreate)
        {
            //Arrange
            //Act
            Func<Task> actual = async() => await StoreApi.ProvisionAsync(testSpecUri, keyMethod, passKey, profile, recreate);

            //Assert
            await actual.Should().ThrowAsync<Exception>();

        }

        //Todo fix test
        /**
        [Test, TestCase(TestName = "OpenAsync call works.")]
        public async Task OpenAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, true);
            await store.CloseAsync();

            //Act
            //Todo we need to create a database with a store config?
            Store actual = await StoreApi.OpenAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);

            //Assert
            actual.storeHandle.Should().NotBe((IntPtr)0);
        }**/
        #endregion

        #region remove
        [Test, TestCase(TestName = "RemoveAsync call works and returns true.")]
        public async Task RemoveAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, true);

            //Act
            bool actual = await store.RemoveAsync(testSpecUri);

            //Assert
            actual.Should().Be(true);
        }
        private static IEnumerable<TestCaseData> CreateCasesRemoveAsyncThrows()
        {
            string uri = "sqlite://:data:";

            yield return new TestCaseData(null)
                .SetName("RemoveAsync throws with no specUri provided.");
            yield return new TestCaseData(uri)
                .SetName("RemoveAsync callback throws with wrong specUri provided.");
        }

        [Test, TestCaseSource(nameof(CreateCasesRemoveAsyncThrows))]
        public async Task RemoveAsyncFails(string specUri)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri);

            //Act
            Func<Task> actual = async () => await store.RemoveAsync(specUri);

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region create, get, remove profile
        [Test, TestCase(TestName = "CreateProfileAsync call returns works and return profile.")]
        public async Task CreateProfileAsyncWorks()
        {
            //Arrange
            string newProfile = "newStoreProfile";
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile,testRecreate);

            //Act
            string actual = await store.CreateProfileAsync(newProfile);

            //Assert
            actual.Should().Be(newProfile);
        }

        [Test, TestCase(TestName = "CreateProfileAsync callback call throws when creating profile with already existing name.")]
        public async Task CreateProfileAsyncThrows()
        {
            //Arrange
            string newProfile = "testProfile";
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Act
            Func<Task> actual = async () => await store.CreateProfileAsync(newProfile);

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "GetProfileNameAsync call works and returns active profile.")]
        public async Task GetProfileNameAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
            
            //Act
            string actual = await store.GetProfileNameAsync();

            //Assert
            actual.Should().Be(testProfile);
        }

        [Test, TestCase(TestName = "GetProfileNameAsync callback throws with invalid storeHandle.")]
        public async Task GetProfileNameAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
            store.storeHandle = (IntPtr)99;
            //Act
            Func<Task> actual = async () => await store.GetProfileNameAsync();

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "RemoveProfileAsync call works and returns true for existing profiles and false for non existing profiles.")]
        public async Task RemoveProfileAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
            string newProfile = await store.CreateProfileAsync("newProfile");
            //Act
            bool actual1 = await store.RemoveProfileAsync(testProfile);
            bool actual2 = await store.RemoveProfileAsync(newProfile);
            bool actual3 = await store.RemoveProfileAsync("nonExistingProfile");

            //Assert
            actual1.Should().Be(true);
            actual2.Should().Be(true);
            actual3.Should().Be(false);
        }

        [Test, TestCase(TestName = "RemoveProfileAsync call throws with no profile provided.")]
        public async Task RemoveProfileAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
 
            //Act
            Func<Task> actual = async() => await store.RemoveProfileAsync(null);

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region rekey
        [Test, TestCase(TestName = "RekeyAsync call works.")]
        public async Task RekeyAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
            string newTestSeed = "testseed500000200006400003008001";
            string newTestPassKey = await StoreApi.StoreGenerateRawKeyAsync(newTestSeed);
            //Act
            bool actual = await store.RekeyAsync(KeyMethod.RAW, newTestPassKey);

            //Assert
            actual.Should().Be(true);
        }

        [Test, TestCase(TestName = "RekeyAsync callback throws with invalid storeHandle.")]
        public async Task RekeyAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);
            string newTestSeed = "testseed500000200006400003008001";
            string newTestPassKey = await StoreApi.StoreGenerateRawKeyAsync(newTestSeed);
            store.storeHandle = (IntPtr)99;
            //Act
            Func<Task> actual = async() => await store.RekeyAsync(KeyMethod.RAW, newTestPassKey);

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateCasesCloseAsyncWorks()
        {
            yield return new TestCaseData(false, false)
                .SetName("CloseAsync works without removing store with removeStore equals false.");
            yield return new TestCaseData(true, true)
                .SetName("CloseAsync works and removes store with removeStore equals true.");
        }
        
        [Test, TestCaseSource(nameof(CreateCasesCloseAsyncWorks))]
        public async Task CloseAsync(bool removeStore, bool expected)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Act
            store.storeHandle = new IntPtr();
            bool actual = await store.CloseAsync(removeStore);

            //Assert
            actual.Should().Be(expected);
            store.session.Should().Be(null);
            store.storeHandle.Should().Be((IntPtr)0);
        }
        #endregion

        #region start session
        [Test, TestCase(TestName = "StartSessionAsync works.")]
        public async Task StartSessionAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);

            //Act
            Session actual = await store.StartSessionAsync(testProfile, testAsTransactions);

            //Assert
            actual.Should().NotBe(null);
            actual.sessionHandle.Should().NotBe(new IntPtr());
            actual.Should().BeEquivalentTo(store.session);
        }

        [Test, TestCase(TestName = "StartSessionAsync throws with invalid store handle.")]
        public async Task StartSessionAsyncThrowsInvalidStoreHandle()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            await store.RemoveAsync(testSpecUri);

            //Act
            Func<Task> actual = async () => await store.StartSessionAsync(testProfile, testAsTransactions);

            //Assert
            await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Cannot start session from closed store.");
        }

        [Test, TestCase(TestName = "StartSessionAsync throws with session already open.")]
        public async Task StartSessionAsyncThrowsSessionOpen()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, testAsTransactions);

            //Act
            Func<Task> actual = async () => await store.StartSessionAsync(testProfile, testAsTransactions);

            //Assert
            await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Session already opened.");
        }

        [Test, TestCase(TestName = "CreateSessionWorks and returns session as session.")]
        public async Task CreateSessionWorksAsSession()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);

            //Act
            Session actual = store.CreateSession();

            //Assert
            actual.Should().NotBe(null);
            actual.sessionHandle.Should().Be((IntPtr)0);
            actual.isTransaction.Should().Be(false);
            actual.storeHandle.Should().Be(store.storeHandle);  
        }

        [Test, TestCase(TestName = "CreateSessionWorks and returns session as transaction.")]
        public async Task CreateSessionWorksAsTxn()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);

            //Act
            Session actual = store.CreateTransaction();

            //Assert
            actual.Should().NotBe(null);
            actual.sessionHandle.Should().Be((IntPtr)0);
            actual.isTransaction.Should().Be(true);
            actual.storeHandle.Should().Be(store.storeHandle);
        }
        #endregion

        #endregion


        #region session
        [Test, TestCase(TestName = "StartAsync of session works and returns active session with handle.")]
        public async Task StartAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session sessionTxn = store.CreateTransaction();
            //Act
            Session actual = await sessionTxn.StartAsync();

            //Assert
            actual.Should().Be(store.session);
            actual.sessionHandle.Should().NotBe((IntPtr)0);
        }

        [Test, TestCase(TestName = "StartAsync of session throws with invalid store handle.")]
        public async Task StartAsyncThrowsInvalidStoreHandle()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session sessionTxn = store.CreateTransaction();
            await store.CloseAsync(true);

            //Act
            Func<Task> actual = async () => await sessionTxn.StartAsync();

            //Assert
            await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Cannot start session from closed store.");
        }

        [Test, TestCase(TestName = "StartAsync of session throws with session already open.")]
        public async Task StartAsyncThrowsSessionOpen()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session sessionTxn = store.CreateTransaction();
            Session session = await sessionTxn.StartAsync();

            //Act
            Func<Task> actual = async () => await sessionTxn.StartAsync();

            //Assert
            await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Session already opened.");
        }

        [Test, TestCase(TestName = "CountAsyncWorks works and returns counted number.")]
        public async Task CountAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool testInsert1 = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            bool testInsert2 = await session.InsertAsync(testEntry["category"].ToString(), "testName2");

            //Act
            long actual = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            actual.Should().Be(2);
            Console.WriteLine(actual);
        }

        [Test, TestCase(TestName = "InsertAsync works for session.")]
        public async Task SessionInsertAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            long initCount = await session.CountAsync(testEntry["category"].ToString());


            //Act
            bool actual = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            long finalCount = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            actual.Should().BeTrue();
            initCount.Should().Be(0);
            finalCount.Should().Be(1);
        }

        [Test, TestCase(TestName = "RemoveAsync works for session.")]
        public async Task SessionRemoveAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            long initCount = await session.CountAsync(testEntry["category"].ToString());

            //Act
            bool actual = await session.RemoveAsync(testEntry["category"].ToString(), "testName1");
            long finalCount = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            actual.Should().BeTrue();
            initCount.Should().Be(1);
            finalCount.Should().Be(0);
        }

        [Test, TestCase(TestName = "RemoveAllAsync works for session.")]
        public async Task SessionRemoveAllAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert1 = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            bool initInsert2 = await session.InsertAsync(testEntry["category"].ToString(), "testName2");
            long initCount = await session.CountAsync(testEntry["category"].ToString());

            //Act
            long actual = await session.RemoveAllAsync(testEntry["category"].ToString());
            long finalCount = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            actual.Should().Be(2);
            initCount.Should().Be(2);
            finalCount.Should().Be(0);
        }

        //TODO need Fetch method
        [Test, TestCase(TestName = "ReplaceAsyncs works for session.")]
        public async Task SessionReplaceAsyncWorks()
        {
            //Arrange
            string testName = "testName";
            string replacedTestName = "replacedTestName";
            string testValue = "testValue";
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert1 = await session.InsertAsync(testEntry["category"].ToString(), testName, testValue);
            bool initInsert2 = await session.InsertAsync(testEntry["category"].ToString(), "name", "val");
            IntPtr entry1 = await session.FetchAsync(testEntry["category"].ToString(), testName);
            
            string entryName1 = await ResultListApi.EntryListGetNameAsync(entry1, 1);
            string entryCat1 = await ResultListApi.EntryListGetCategoryAsync(entry1, 1);
            //Act
            /**
            int entryListCount = await ResultListApi.EntryListCountAsync(entry1);
            string entryName1 = await ResultListApi.EntryListGetNameAsync(entry1, 0);
            bool actual = await session.ReplaceAsync(testEntry["category"].ToString(), replacedTestName);
            IntPtr entry2 = await session.FetchAsync(testEntry["category"].ToString(), testName);
            int entryListCount2 = await ResultListApi.EntryListCountAsync(entry2);
            string entryName2 = await ResultListApi.EntryListGetNameAsync(entry2, 0);
            **/
            //Assert
            //actual.Should().BeTrue();
            //entryName1.Should().Be(testName);
            //entryName2.Should().Be(replacedTestName);
            //entry1.Should().NotBe(new IntPtr());
            Console.WriteLine(entry1);
        }

        private static IEnumerable<TestCaseData> CreateCasesCloseAndCommitAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("CloseAndCommitAsyncWorks works for session.");
            yield return new TestCaseData(true)
                .SetName("CloseAndCommitAsyncWorks works for transaction.");
        }

        [Test, TestCaseSource(nameof(CreateCasesCloseAndCommitAsyncWorks))]
        public async Task CloseAndCommitAsyncWorks(bool isTxn)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, isTxn);

            //Act
            bool actual = await session.CloseAndCommitAsync();

            //Assert
            actual.Should().BeTrue();
            session.sessionHandle.Should().Be(new IntPtr());
        }

        [Test, TestCase(TestName = "CloseAndCommitAsyncThrows for session handle equals 0.")]
        public async Task CloseAndCommitAsyncThrowsSessionHandle0()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, true);
            await session.CloseAndCommitAsync();

            //Act
            Func<Task> actual = async() => await session.CloseAndCommitAsync();

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "CloseAndCommitAsyncThrows for session equals null.")]
        public async Task CloseAndCommitAsyncThrowsSessionNull()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);

            //Act
            Func<Task> actual = async () => await store.session.CloseAndCommitAsync();

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateCasesCloseAndRollbackAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("CloseAndRollbackAsyncWorks works for session.");
            yield return new TestCaseData(true)
                .SetName("CloseAndRollbackAsyncWorks works for transaction.");
        }

        [Test, TestCaseSource(nameof(CreateCasesCloseAndRollbackAsyncWorks))]
        public async Task CloseAndRollbackAsyncWorks(bool isTxn)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, isTxn);

            //Act
            bool actual = await session.CloseAndRollbackAsync();

            //Assert
            actual.Should().BeTrue();
            session.sessionHandle.Should().Be(new IntPtr());
        }

        [Test, TestCase(TestName = "CloseAndRollbackAsyncThrows for session handle equals 0.")]
        public async Task CloseAndRollbackAsyncThrowsSessionHandle0()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, true);
            await session.CloseAndCommitAsync();

            //Act
            Func<Task> actual = async () => await session.CloseAndRollbackAsync();

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "CloseAndRollbackAsyncThrows for session equals null.")]
        public async Task CloseAndRollbackAsyncThrowsSessionNull()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);

            //Act
            Func<Task> actual = async () => await store.session.CloseAndRollbackAsync();

            //Assert
            await actual.Should().ThrowAsync<Exception>();
        }
        #endregion
    }
}
/**
 Store store = await StoreApi.OpenAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
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
            actual.Should().Be(true);**/
