using aries_askar_dotnet;
using aries_askar_dotnet.AriesAskar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class StoreApiTests
    {
        private Dictionary<string, string> testEntry;
        private string _testUriInMemory;
        private string _testPathDb;
        private KeyMethod testKeyMethod;
        private string testPassKey;
        private string testProfile;
        private string testSeed;
        private bool testRecreate;
        private bool testAsTransactions;
        private string _dbType;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            testEntry = new()
            {
                { "name", "testName" },
                { "category", "testCategory" },
                { "value", "testValue" },
                { "tags", $"{{ \"~plaintag\": \"a\", \"enctag\": \"b\"}}" },
            };
            _dbType = "sqlite";
            _testUriInMemory = "sqlite://:memory:";
            testSeed = "testseed000000000000000000000001";
            testPassKey = await StoreApi.GenerateRawKeyAsync(testSeed);
            testKeyMethod = KeyMethod.RAW;  //kdf:argon2i   //none
            testProfile = "testProfile";
            testRecreate = true;
            testAsTransactions = true;

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string testPathDb = Path.Combine(currentDirectory, @"..\..\..\test-db");
            _testPathDb = _dbType + "://" + Path.GetFullPath(testPathDb);
        }

        #region STORE
        #region provision and open
        private static IEnumerable<TestCaseData> CreateCasesStoreProvisioningWorks()
        {
            string seed = "testseed000000000000000000000001";
            string passKey = StoreApi.GenerateRawKeyAsync(seed).GetAwaiter().GetResult();

            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", true)
                .SetName("ProvisionAsync() call returns store with keyMethod 'raw' and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", true)
                .SetName("ProvisionAsync() call returns store with keyMethod 'none' and recreate true.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", true)
                .SetName("ProvisionAsync() call returns store with keyMethod 'kdf_argon2i' and recreate true.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", false)
                .SetName("ProvisionAsync() call returns store with keyMethod 'raw' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", false)
                .SetName("ProvisionAsync() call returns store with keyMethod 'none' and recreate false.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", false)
                .SetName("ProvisionAsync() call returns store with keyMethod 'kdf_argon2i' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, true)
                .SetName("ProvisionAsync() call returns store with keyMethod 'none' and no passkey and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, false)
                .SetName("ProvisionAsync() call returns store with keyMethod 'none' and no passkey and recreate false.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, null, true)
                .SetName("ProvisionAsync() call returns store with keyMethod 'raw' and no profile.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, null, true)
                .SetName("ProvisionAsync() call returns store with keyMethod 'none' and no profile.");
        }

        [Test, TestCaseSource(nameof(CreateCasesStoreProvisioningWorks))]
        public async Task ProvisionAsyncWorks(KeyMethod keyMethod, string passKey, string profile, bool recreate)
        {
            //Arrange

            //Act
            Store actual = await StoreApi.ProvisionAsync(_testPathDb, keyMethod, passKey, profile, recreate);

            //Assert
            _ = actual.storeHandle.Should().NotBe((IntPtr)0);

            //Clean-up
            _ = await StoreApi.CloseAsync(actual, remove: true);
        }

        private static IEnumerable<TestCaseData> CreateCasesStoreProvisioningThrows()
        {
            string specUri = "sqlite://:memory:";

            yield return new TestCaseData(specUri, KeyMethod.RAW, null, "testProfile", true)
                .SetName("ProvisionAsync() callback throws with keyMethod 'raw' and no passkey and recreate true.");
            yield return new TestCaseData(specUri, KeyMethod.RAW, null, "testProfile", false)
                            .SetName("ProvisionAsync() callback throws with keyMethod 'raw' and no passkey and recreate false.");
            yield return new TestCaseData(specUri, KeyMethod.KDF_ARGON2I, null, "testProfile", true)
                .SetName("ProvisionAsync() callback throws with keyMethod 'kdf_argon2i' and given passkey and recreate true.");
            yield return new TestCaseData(null, KeyMethod.KDF_ARGON2I, null, "testProfile", true)
                .SetName("ProvisionAsync() throws with specUri equals null.");
        }

        [Test, TestCaseSource(nameof(CreateCasesStoreProvisioningThrows))]
        public async Task ProvisionAsyncThrows(string specUri, KeyMethod keyMethod, string passKey, string profile, bool recreate)
        {
            //Arrange
            //Act
            Func<Task> actual = async () => await StoreApi.ProvisionAsync(specUri, keyMethod, passKey, profile, recreate);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();

        }

        private static IEnumerable<TestCaseData> CreateCasesOpenAsyncWorks()
        {
            string seed = "testseed000000000000000000000001";
            string passKey = StoreApi.GenerateRawKeyAsync(seed).GetAwaiter().GetResult();

            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", true)
                .SetName("OpenAsync() call returns store with keyMethod 'raw' and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", true)
                .SetName("OpenAsync() call returns store with keyMethod 'none' and recreate true.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", true)
                .SetName("OpenAsync() call returns store with keyMethod 'kdf_argon2i' and recreate true.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, "testProfile", false)
                .SetName("OpenAsync() call returns store with keyMethod 'raw' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, "testProfile", false)
                .SetName("OpenAsync() call returns store with keyMethod 'none' and recreate false.");
            yield return new TestCaseData(KeyMethod.KDF_ARGON2I, passKey, "testProfile", false)
                .SetName("OpenAsync() call returns store with keyMethod 'kdf_argon2i' and recreate false.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, true)
                .SetName("OpenAsync() call returns store with keyMethod 'none' and no passkey and recreate true.");
            yield return new TestCaseData(KeyMethod.NONE, null, null, false)
                .SetName("OpenAsync() call returns store with keyMethod 'none' and no passkey and recreate false.");
            yield return new TestCaseData(KeyMethod.RAW, passKey, null, true)
                .SetName("OpenAsync() call returns store with keyMethod 'raw' and no profile.");
            yield return new TestCaseData(KeyMethod.NONE, passKey, null, true)
                .SetName("OpenAsync() call returns store with keyMethod 'none' and no profile.");
        }

        [Test, TestCaseSource(nameof(CreateCasesOpenAsyncWorks))]
        public async Task OpenAsyncWorks(KeyMethod keyMethod, string passKey, string profile, bool recreate)
        {
            //Arrange
            Store init = await StoreApi.ProvisionAsync(_testPathDb, keyMethod, passKey, profile, recreate);
            _ = await init.CloseAsync();
            _ = init.storeHandle.Should().Be((IntPtr)0);

            //Act
            Store actual = await StoreApi.OpenAsync(_testPathDb, keyMethod, passKey, profile);

            //Assert
            _ = actual.storeHandle.Should().NotBe((IntPtr)0);

            //Clean-up
            _ = await StoreApi.CloseAsync(actual, remove: true);
        }


        [Test]
        [TestCase(TestName = "OpenAsync() works on several store processes.")]
        public async Task OpenAsyncWorksSeveralStoreThreads()
        {
            //Arrange

            //Act
            Store store1 = await StoreApi.ProvisionAsync(_testPathDb, testKeyMethod, testPassKey);
            _ = store1.storeHandle.Should().NotBe((IntPtr)0);
            Store store2 = await StoreApi.OpenAsync(_testPathDb, testKeyMethod, testPassKey);
            _ = store2.storeHandle.Should().NotBe((IntPtr)0);
            Store store3 = await StoreApi.OpenAsync(_testPathDb, testKeyMethod, testPassKey);
            _ = store3.storeHandle.Should().NotBe((IntPtr)0);
            _ = store1.storeHandle.Should().NotBe(store2.storeHandle);
            _ = store1.storeHandle.Should().NotBe(store3.storeHandle);
            _ = store2.storeHandle.Should().NotBe(store3.storeHandle);
            _ = await store1.CloseAsync();
            _ = await store2.CloseAsync();
            _ = await store3.CloseAsync();

            //Clean-up
            _ = await StoreApi.RemoveAsync(store1, _testPathDb);
        }

        [Test, TestCase(TestName = "OpenAsync() works and previously set entry still saved in database.")]
        public async Task OpenAsyncSessionEntrysNotEmpty()
        {
            //Arrange
            Store init = await StoreApi.ProvisionAsync(_testPathDb);
            Session session = await init.StartSessionAsync();
            _ = await session.InsertAsync(testEntry["category"].ToString(), testEntry["name"].ToString(), testEntry["value"].ToString());
            _ = await session.CloseAndCommitAsync();
            _ = await init.CloseAsync();
            _ = init.storeHandle.Should().Be((IntPtr)0);
            _ = init.session.Should().Be(null);

            //Act
            Store storeNew = await StoreApi.OpenAsync(_testPathDb);
            Session sessionNew = await storeNew.StartSessionAsync();
            IntPtr resultHandle = await sessionNew.FetchAsync(testEntry["category"].ToString(), testEntry["name"].ToString());
            string actual = await ResultListApi.EntryListGetValueAsync(resultHandle, 0);

            //Assert
            _ = actual.Should().Be(testEntry["value"].ToString());

            //Clean-up
            _ = await StoreApi.CloseAsync(storeNew, remove: true);
        }

        [Test, TestCase(TestName = "OpenAsync() works and previously set key still saved in database.")]
        public async Task OpenAsyncSessionKeyNotEmpty()
        {
            //Arrange
            Store init = await StoreApi.ProvisionAsync(_testPathDb);
            Session session = await init.StartSessionAsync();
            IntPtr testKeyHandle = await KeyApi.CreateKeyFromSeedAsync(KeyAlg.ED25519, testSeed, SeedMethod.BlsKeyGen);
            byte[] pBytesInit = await KeyApi.GetPublicBytesFromKeyAsync(testKeyHandle);
            byte[] sBytesInit = await KeyApi.GetSecretBytesFromKeyAsync(testKeyHandle);
            _ = await session.InsertKeyAsync(testKeyHandle, "testKey");
            _ = await session.CloseAndCommitAsync();
            _ = await init.CloseAsync();
            _ = init.storeHandle.Should().Be((IntPtr)0);
            _ = init.session.Should().Be(null);

            //Act
            Store storeNew = await StoreApi.OpenAsync(_testPathDb);
            Session sessionNew = await storeNew.StartSessionAsync();
            IntPtr resultHandle = await sessionNew.FetchKeyAsync("testKey");
            IntPtr actual = await ResultListApi.LoadLocalKeyHandleFromKeyEntryListAsync(resultHandle, 0);
            byte[] pBytesActual = await KeyApi.GetPublicBytesFromKeyAsync(actual);
            byte[] sBytesActual = await KeyApi.GetSecretBytesFromKeyAsync(actual);

            //Assert
            _ = actual.Should().NotBe(testKeyHandle);
            _ = pBytesInit.Should().BeEquivalentTo(pBytesActual);
            _ = sBytesInit.Should().BeEquivalentTo(sBytesActual);

            //Clean-up
            _ = await StoreApi.CloseAsync(storeNew, remove: true);
        }

        private static IEnumerable<TestCaseData> CreateCasesOpenAsyncThrows()
        {
            yield return new TestCaseData(null)
                .SetName("OpenAsync() throws with specUri equals null.");
            yield return new TestCaseData("unknown:uri")
                .SetName("OpenAsync() throws with wrong specUri.");
        }

        [Test, TestCaseSource(nameof(CreateCasesOpenAsyncThrows))]
        public async Task OpenAsyncThrows(string specUri)
        {
            //Arrange
            //Act
            Func<Task> actual = async () => await StoreApi.OpenAsync(specUri);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region remove
        [Test, TestCase(TestName = "RemoveAsync() call works and returns true.")]
        public async Task RemoveAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, true);
            _ = await store.StartSessionAsync();

            //Act
            bool actual = await store.RemoveAsync(_testUriInMemory);

            //Assert
            _ = actual.Should().Be(true);
        }
        private static IEnumerable<TestCaseData> CreateCasesRemoveAsyncThrows()
        {
            string uri = "sqlite://:data:";

            yield return new TestCaseData(null)
                .SetName("RemoveAsync() throws with no specUri provided.");
            yield return new TestCaseData(uri)
                .SetName("RemoveAsync() callback throws with wrong specUri provided.");
        }

        [Test, TestCaseSource(nameof(CreateCasesRemoveAsyncThrows))]
        public async Task RemoveAsyncFails(string specUri)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory);

            //Act
            Func<Task> actual = async () => await store.RemoveAsync(specUri);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region create, get, remove profile
        [Test, TestCase(TestName = "CreateProfileAsync() call returns works and return profile.")]
        public async Task CreateProfileAsyncWorks()
        {
            //Arrange
            string newProfile = "newStoreProfile";
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Act
            string actual = await store.CreateProfileAsync(newProfile);

            //Assert
            _ = actual.Should().Be(newProfile);
        }

        [Test, TestCase(TestName = "CreateProfileAsync() callback call throws when creating profile with already existing name.")]
        public async Task CreateProfileAsyncThrows()
        {
            //Arrange
            string newProfile = "testProfile";
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Act
            Func<Task> actual = async () => await store.CreateProfileAsync(newProfile);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "GetProfileNameAsync() call works and returns active profile.")]
        public async Task GetProfileNameAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Act
            string actual = await store.GetProfileNameAsync();

            //Assert
            _ = actual.Should().Be(testProfile);
        }

        [Test, TestCase(TestName = "GetProfileNameAsync() callback throws with invalid storeHandle.")]
        public async Task GetProfileNameAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);
            store.storeHandle = (IntPtr)99;
            //Act
            Func<Task> actual = async () => await store.GetProfileNameAsync();

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "RemoveProfileAsync() call works and returns true for existing profiles and false for non existing profiles.")]
        public async Task RemoveProfileAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);
            string newProfile = await store.CreateProfileAsync("newProfile");
            //Act
            bool actual1 = await store.RemoveProfileAsync(testProfile);
            bool actual2 = await store.RemoveProfileAsync(newProfile);
            bool actual3 = await store.RemoveProfileAsync("nonExistingProfile");

            //Assert
            _ = actual1.Should().Be(true);
            _ = actual2.Should().Be(true);
            _ = actual3.Should().Be(false);
        }

        [Test, TestCase(TestName = "RemoveProfileAsync() call throws with no profile provided.")]
        public async Task RemoveProfileAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Act
            Func<Task> actual = async () => await store.RemoveProfileAsync(null);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region rekey
        private static IEnumerable<TestCaseData> CreateCasesStoreRekeyWorks()
        {
            string seed = "testseed000000000000000000000001";
            string passKey = StoreApi.GenerateRawKeyAsync(seed).GetAwaiter().GetResult();

            string newTestSeed = "testseed500000200006400003008001";
            string newTestPassKey = StoreApi.GenerateRawKeyAsync(newTestSeed).GetAwaiter().GetResult();
            KeyMethod newTestKeyMethod = KeyMethod.RAW;

            yield return new TestCaseData(KeyMethod.RAW, passKey, newTestKeyMethod, newTestPassKey)
                .SetName("RekeyAsync() call works and Store can be opend with new key with keyMethod 'raw'.");
        }

        [Test, TestCaseSource(nameof(CreateCasesStoreRekeyWorks))]
        public async Task RekeyAsyncCasesWorks(KeyMethod testKeyMethod, string testPassKey, KeyMethod newTestKeyMethod, string newTestPassKey)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testPathDb, testKeyMethod, testPassKey);
            _ = await store.CloseAsync();
            _ = store.storeHandle.Should().Be((IntPtr)0);

            store = await StoreApi.OpenAsync(_testPathDb, testKeyMethod, testPassKey);
            _ = store.storeHandle.Should().NotBe((IntPtr)0);

            //Act
            bool actual = await store.RekeyAsync(newTestKeyMethod, newTestPassKey);
            _ = await store.CloseAsync();
            _ = store.storeHandle.Should().Be((IntPtr)0);
            Store newStore = await StoreApi.OpenAsync(_testPathDb, newTestKeyMethod, newTestPassKey);

            //Assert
            _ = actual.Should().Be(true);
            _ = newStore.storeHandle.Should().NotBe((IntPtr)0);

            //Clean-up
            _ = await StoreApi.CloseAsync(newStore, remove: true);
        }

        [Test, TestCase(TestName = "RekeyAsync() call works.")]
        public async Task RekeyAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);
            string newTestSeed = "testseed500000200006400003008001";
            string newTestPassKey = await StoreApi.GenerateRawKeyAsync(newTestSeed);
            //Act
            bool actual = await store.RekeyAsync(KeyMethod.RAW, newTestPassKey);

            //Assert
            _ = actual.Should().Be(true);
        }

        [Test, TestCase(TestName = "RekeyAsync() callback throws with invalid storeHandle.")]
        public async Task RekeyAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);
            string newTestSeed = "testseed500000200006400003008001";
            string newTestPassKey = await StoreApi.GenerateRawKeyAsync(newTestSeed);
            store.storeHandle = (IntPtr)99;
            //Act
            Func<Task> actual = async () => await store.RekeyAsync(KeyMethod.RAW, newTestPassKey);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateCasesCloseAsyncWorks()
        {
            yield return new TestCaseData(false, false)
                .SetName("CloseAsync() works without removing store with removeStore equals false.");
            yield return new TestCaseData(true, true)
                .SetName("CloseAsync() works and removes store with removeStore equals true.");
        }

        [Test, TestCaseSource(nameof(CreateCasesCloseAsyncWorks))]
        public async Task CloseAsync(bool removeStore, bool expected)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile, testRecreate);

            //Act
            store.storeHandle = new IntPtr();
            bool actual = await store.CloseAsync(removeStore);

            //Assert
            _ = actual.Should().Be(expected);
            _ = store.session.Should().Be(null);
            _ = store.storeHandle.Should().Be((IntPtr)0);
        }
        #endregion

        #region generate raw key
        private static IEnumerable<TestCaseData> CreateCasesGenerateRawKeyAsyncWorks()
        {
            yield return new TestCaseData("testseed000000000000000000000001")
                .SetName("GenerateRawKeyAsync() call returns result string if seed length is 32.");
            yield return new TestCaseData("testseedlengthless32")
                .SetName("GenerateRawKeyAsync() call returns result string if seed length is less than 32.");
            yield return new TestCaseData("testseed000000000000000000000001greater32")
                .SetName("GenerateRawKeyAsync() call returns result string if seed length is greater than 32.");
            yield return new TestCaseData(null)
                .SetName("GenerateRawKeyAsync() call returns result string if seed is null.");
        }

        [Test, TestCaseSource(nameof(CreateCasesGenerateRawKeyAsyncWorks))]
        public async Task StoreGenerateRawKeyWorks(string testSeed)
        {
            //Arrange

            //Act
            string actual = await StoreApi.GenerateRawKeyAsync(testSeed);

            //Assert
            _ = actual.Should().NotBe("");
        }
        #endregion

        #region start session
        [Test, TestCase(TestName = "StartSessionAsync() works.")]
        public async Task StartSessionAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);

            //Act
            Session actual = await store.StartSessionAsync(testProfile, testAsTransactions);

            //Assert
            _ = actual.Should().NotBe(null);
            _ = actual.sessionHandle.Should().NotBe(new IntPtr());
            _ = actual.Should().BeEquivalentTo(store.session);
        }

        [Test, TestCase(TestName = "StartSessionAsync() throws with invalid store handle.")]
        public async Task StartSessionAsyncThrowsInvalidStoreHandle()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            _ = await store.RemoveAsync(_testUriInMemory);

            //Act
            Func<Task> actual = async () => await store.StartSessionAsync(testProfile, testAsTransactions);

            //Assert
            _ = await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Cannot start session from closed store.");
        }

        [Test, TestCase(TestName = "StartSessionAsync() throws with session already open.")]
        public async Task StartSessionAsyncThrowsSessionOpen()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, testAsTransactions);

            //Act
            Func<Task> actual = async () => await store.StartSessionAsync(testProfile, testAsTransactions);

            //Assert
            _ = await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Session already opened.");
        }

        [Test, TestCase(TestName = "CreateSession() works and returns session as session.")]
        public async Task CreateSessionWorksAsSession()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);

            //Act
            Session actual = store.CreateSession();

            //Assert
            _ = actual.Should().NotBe(null);
            _ = actual.sessionHandle.Should().Be((IntPtr)0);
            _ = actual.isTransaction.Should().Be(false);
            _ = actual.storeHandle.Should().Be(store.storeHandle);
        }

        [Test, TestCase(TestName = "CreateSession() works and returns session as transaction.")]
        public async Task CreateSessionWorksAsTxn()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);

            //Act
            Session actual = store.CreateTransaction();

            //Assert
            _ = actual.Should().NotBe(null);
            _ = actual.sessionHandle.Should().Be((IntPtr)0);
            _ = actual.isTransaction.Should().Be(true);
            _ = actual.storeHandle.Should().Be(store.storeHandle);
        }
        #endregion

        #region start scan
        [Test, TestCase(TestName = "StartScanAsync() works.")]
        public async Task StartScanAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            int numStartScanInputs = 5;
            //Act
            Scan actual = await store.StartScanAsync(testEntry["category"].ToString());

            //Assert
            _ = actual.parameters.Should().Contain(testEntry["category"].ToString());
            _ = actual.parameters.Count.Should().Be(numStartScanInputs);
            _ = actual.storeHandle.Should().Be(store.storeHandle);
            _ = actual.scanHandle.Should().NotBe(new IntPtr());
        }

        [Test, TestCase(TestName = "StartScanAsync() throws with category equals null.")]
        public async Task StartScanAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);

            //Act
            Func<Task> actual = async () => await store.StartScanAsync(null);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #endregion

        #region SESSION

        #region start session
        [Test, TestCase(TestName = "StartAsync() of session works and returns active session with handle.")]
        public async Task StartAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session sessionTxn = store.CreateTransaction();
            //Act
            Session actual = await sessionTxn.StartAsync();

            //Assert
            _ = actual.Should().Be(store.session);
            _ = actual.sessionHandle.Should().NotBe((IntPtr)0);
        }

        [Test, TestCase(TestName = "StartAsync() of session throws with invalid store handle.")]
        public async Task StartAsyncThrowsInvalidStoreHandle()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session sessionTxn = store.CreateTransaction();
            _ = await store.CloseAsync(true);

            //Act
            Func<Task> actual = async () => await sessionTxn.StartAsync();

            //Assert
            _ = await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Cannot start session from closed store.");
        }

        [Test, TestCase(TestName = "StartAsync() of session throws with session already open.")]
        public async Task StartAsyncThrowsSessionOpen()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session sessionTxn = store.CreateTransaction();
            Session session = await sessionTxn.StartAsync();

            //Act
            Func<Task> actual = async () => await sessionTxn.StartAsync();

            //Assert
            _ = await actual.Should().ThrowAsync<AriesAskarException>().WithMessage("'Wrapper' error occured with ErrorCode '99' : Session already opened.");
        }
        #endregion

        #region count records
        [Test, TestCase(TestName = "CountAsync() works and returns counted number.")]
        public async Task CountAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            _ = await session.InsertAsync(testEntry["category"].ToString(), "testName2");

            //Act
            long actual = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            _ = actual.Should().Be(2);
        }

        [Test, TestCase(TestName = "CountAsync() throws with category equals null.")]
        public async Task CountAsyncThrows()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool testInsert1 = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            bool testInsert2 = await session.InsertAsync(testEntry["category"].ToString(), "testName2");

            //Act
            Func<Task> actual = async () => await session.CountAsync(null);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "CountAsync() throws with session handle equals 0.")]
        public async Task CountAsyncThrowsNoHandle()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool testInsert1 = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            _ = await store.CloseAsync();
            //Act
            Func<Task> actual = async () => await session.CountAsync(testEntry["category"].ToString());

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "CountAsync() callback throws with invalid sessionHandle.")]
        public async Task CountAsyncThrowsInvalidHandle()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool testInsert1 = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            bool testInsert2 = await session.InsertAsync(testEntry["category"].ToString(), "testName2");
            session.sessionHandle = (IntPtr)99;
            //Act
            Func<Task> actual = async () => await session.CountAsync(testEntry["category"].ToString());

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region insert records
        private static IEnumerable<TestCaseData> CreateCasesInsertAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("InsertAsync() works for session.");
            yield return new TestCaseData(true)
                .SetName("InsertAsync() works for transaction.");
        }
        [Test, TestCaseSource(nameof(CreateCasesInsertAsyncWorks))]
        public async Task SessionInsertAsyncWorks(bool asTxn)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            long initCount = await session.CountAsync(testEntry["category"].ToString());

            //Act
            bool actual = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            long finalCount = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            _ = actual.Should().BeTrue();
            _ = initCount.Should().Be(0);
            _ = finalCount.Should().Be(1);
        }

        private static IEnumerable<TestCaseData> CreateCasesSessionInsertAsyncThrows()
        {
            yield return new TestCaseData("testCategory", "testName", true, true)
                .SetName("InsertAsync() callback throws with inserting a duplicate record.");
            yield return new TestCaseData(null, "testName", false, true)
                .SetName("InsertAsync() throws with no category provided.");
            yield return new TestCaseData("testCategory", null, false, true)
                .SetName("InsertAsync() throws with no name provided.");
            yield return new TestCaseData("testCategory", "testName", false, false)
                .SetName("InsertAsync() throws wrapper error with session handle equals 0.");
        }

        [Test, TestCaseSource(nameof(CreateCasesSessionInsertAsyncThrows))]
        public async Task SessionInsertAsyncThrows(string category, string name, bool doubleInsert, bool sessionHandleExisting)
        {
            //Arrange

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            if (doubleInsert)
            {
                _ = await session.InsertAsync(category, name, "testValue");
            }

            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.InsertAsync(category, name, "testValue");

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region remove / remove all records
        [Test, TestCase(TestName = "RemoveAsync() works for session.")]
        public async Task SessionRemoveAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            long initCount = await session.CountAsync(testEntry["category"].ToString());

            //Act
            bool actual = await session.RemoveAsync(testEntry["category"].ToString(), "testName1");
            long finalCount = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            _ = actual.Should().BeTrue();
            _ = initCount.Should().Be(1);
            _ = finalCount.Should().Be(0);
        }

        private static IEnumerable<TestCaseData> CreateCasesSessionRemoveAsyncThrows()
        {
            string testCategory = "testCategory";
            string testName = "testName";
            yield return new TestCaseData(null, testName, true)
                .SetName("RemoveAsync() throws for session when providing category equals null.");
            yield return new TestCaseData(testCategory, null, true)
                .SetName("RemoveAsync() throws for session when providing name equals null.");
            yield return new TestCaseData(testCategory, testName, true)
                .SetName("RemoveAsync() callback for session throws with trying to remove an non existing record.");
            yield return new TestCaseData(testCategory, testName, false)
                .SetName("RemoveAsync() throws wrapper error for session with sessionHandle equals 0.");
        }

        [Test, TestCaseSource(nameof(CreateCasesSessionRemoveAsyncThrows))]
        public async Task SessionRemoveAsyncThrows(string category, string name, bool sessionHandleExisting)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.RemoveAsync(category, name);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "RemoveAllAsync() works for session.")]
        public async Task SessionRemoveAllAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(testEntry["category"].ToString(), "testName1");
            _ = await session.InsertAsync(testEntry["category"].ToString(), "testName2");
            long initCount = await session.CountAsync(testEntry["category"].ToString());

            //Act
            long actual = await session.RemoveAllAsync(testEntry["category"].ToString());
            long finalCount = await session.CountAsync(testEntry["category"].ToString());

            //Assert
            _ = actual.Should().Be(2);
            _ = initCount.Should().Be(2);
            _ = finalCount.Should().Be(0);
        }

        [Test, TestCase(TestName = "RemoveAllAsync() throws with no category provided.")]
        public async Task SessionRemoveAllAsyncThrowsNoCategory()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();

            //Act
            Func<Task> actual = async () => await session.RemoveAllAsync(null);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "RemoveAllAsync() throws wrapper error with session handle equals 0.")]
        public async Task SessionRemoveAllAsyncThrowsNoHandle()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await store.CloseAsync();

            //Act
            Func<Task> actual = async () => await session.RemoveAllAsync("testCategory");

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region replace records
        [Test, TestCase(TestName = "ReplaceAsync() works for session.")]
        public async Task SessionReplaceAsyncWorks()
        {
            //Arrange
            string testName = "testName";
            string testValue = "testValue";
            string replacedTestValue = "newTestValue";

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(testEntry["category"].ToString(), testName, testValue);

            string entryVal1 = await ResultListApi.EntryListGetValueAsync(
                await session.FetchAsync(
                    testEntry["category"].ToString(),
                    testName),
                0);

            //Act
            bool actual = await session.ReplaceAsync(testEntry["category"].ToString(), testName, replacedTestValue);

            string entryVal2 = await ResultListApi.EntryListGetValueAsync(
                await session.FetchAsync(
                    testEntry["category"].ToString(),
                    testName),
                0);

            //Assert
            _ = actual.Should().BeTrue();
            _ = entryVal1.Should().Be(testValue);
            _ = entryVal2.Should().Be(replacedTestValue);
        }

        private static IEnumerable<TestCaseData> CreateCasesSessionReplaceAsyncThrows()
        {
            yield return new TestCaseData("testCategory", "newTestName", true)
                .SetName("ReplaceAsync() callback throws with trying to replace value in an non existing record.");
            yield return new TestCaseData(null, "testName", true)
                .SetName("ReplaceAsync() throws with no category provided.");
            yield return new TestCaseData("testCategory", null, true)
                .SetName("ReplaceAsync() throws with no name provided.");
            yield return new TestCaseData("testCategory", "testName", false)
                .SetName("ReplaceAsync() throws wrapper error with session handle equals 0.");
        }

        [Test, TestCaseSource(nameof(CreateCasesSessionReplaceAsyncThrows))]
        public async Task SessionReplaceAsyncThrows(string category, string name, bool sessionHandleExisting)
        {
            //Arrange
            string testName = "testName";
            string testCategory = "testCategory";
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert1 = await session.InsertAsync(testCategory, testName);
            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.ReplaceAsync(category, name);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "ReplaceAsync() throws with no category name provided.")]
        public async Task SessionReplaceAsyncThrowsNoCategory()
        {
            //Arrange
            string testName = "testName";

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert1 = await session.InsertAsync(testEntry["category"].ToString(), testName);

            //Act
            Func<Task> actual = async () => await session.ReplaceAsync(null, testName);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region fetch / fetch all records
        private static IEnumerable<TestCaseData> CreateCasesFetchAsyncWorks()
        {
            yield return new TestCaseData(false, true)
                .SetName("FetchAsync() works for session with forUpdate equals true and returns an entryListHandle unequals null.");
            yield return new TestCaseData(false, false)
                .SetName("FetchAsync() works for session with forUpdate equals false and returns an entryListHandle unequals null.");
            yield return new TestCaseData(true, true)
                .SetName("FetchAsync() works for transaction with forUpdate equals true and returns an entryListHandle unequals null.");
            yield return new TestCaseData(true, false)
                .SetName("FetchAsync() works for transaction with forUpdate equals false and returns an entryListHandle unequals null.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchAsyncWorks))]
        public async Task SessionFetchAsyncWorks(bool asTxn, bool forUpdate)
        {
            //Arrange
            string testName = "testName";
            string testValue = "testValue";

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            _ = await session.InsertAsync(testEntry["category"].ToString(), testName, testValue);

            //Act
            IntPtr actual = await session.FetchAsync(testEntry["category"].ToString(), testName, forUpdate);
            string testVal = await ResultListApi.EntryListGetValueAsync(actual, 0);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
            _ = testVal.Should().Be(testValue);
        }

        private static IEnumerable<TestCaseData> CreateCasesFetchAsyncThrows()
        {
            yield return new TestCaseData("testCategory", null, true)
                .SetName("FetchAsync() throws for session with not providing record name.");
            yield return new TestCaseData(null, "testName", true)
                .SetName("FetchAsync() throws for session with not providing record category.");
            yield return new TestCaseData("testCategory", "testName", false)
                .SetName("FetchAsync() throws wrapper error for session with session handle equals 0.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchAsyncThrows))]
        public async Task SessionFetchAsyncThrows(string category, string name, bool sessionHandleExisting)
        {
            //Arrange
            string testName = "testName";
            string testValue = "testValue";

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert1 = await session.InsertAsync(testEntry["category"].ToString(), testName, testValue);
            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.FetchAsync(category, name);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateCasesFetchAllAsyncWorks()
        {
            yield return new TestCaseData(false, true)
                .SetName("FetchAllAsync() works for session with forUpdate equals true and returns an entryListHandle unequals null.");
            yield return new TestCaseData(false, false)
                .SetName("FetchAllAsync() works for session with forUpdate equals false and returns an entryListHandle unequals null.");
            yield return new TestCaseData(true, true)
                .SetName("FetchAllAsync() works for transaction with forUpdate equals true and returns an entryListHandle unequals null.");
            yield return new TestCaseData(true, false)
                .SetName("FetchAllAsync() works for transaction with forUpdate equals false and returns an entryListHandle unequals null.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchAllAsyncWorks))]
        public async Task SessionFetchAllAsyncWorks(bool asTxn, bool forUpdate)
        {
            //Arrange
            string testName1 = "testName1";
            string testName2 = "testName2";
            string testValue = "testValue";

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            _ = await session.InsertAsync(testEntry["category"].ToString(), testName1, testValue, testEntry["tags"].ToString());
            _ = await session.InsertAsync(testEntry["category"].ToString(), testName2, testValue);

            //Act
            IntPtr actual = await session.FetchAllAsync(testEntry["category"].ToString());
            string val1 = await ResultListApi.EntryListGetValueAsync(actual, 0);
            string val2 = await ResultListApi.EntryListGetValueAsync(actual, 1);
            string name1 = await ResultListApi.EntryListGetNameAsync(actual, 0);
            string name2 = await ResultListApi.EntryListGetNameAsync(actual, 1);
            List<string> names = new() { name1, name2 };

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
            _ = val1.Should().Be(testValue);
            _ = val2.Should().Be(testValue);
            _ = names.Should().Contain(testName1);
            _ = names.Should().Contain(testName2);
        }
        private static IEnumerable<TestCaseData> CreateCasesFetchAllAsyncThrows()
        {
            yield return new TestCaseData(null, true)
                .SetName("FetchAllAsync() throws with category equals null.");
            yield return new TestCaseData("testCategory", false)
                .SetName("FetchAllAsync() throws wrapper error for session with session handle equals 0.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchAllAsyncThrows))]
        public async Task SessionFetchAllAsyncThrows(string category, bool sessionHandleExisting)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }
            //Act
            Func<Task> actual = async () => await session.FetchAllAsync(category);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region insert key
        private static IEnumerable<TestCaseData> CreateCasesInsertKeyAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("InsertKeyAsync() works for session.");
            yield return new TestCaseData(true)
                .SetName("InsertKeyAsync() works for transaction.");
        }
        [Test, TestCaseSource(nameof(CreateCasesInsertKeyAsyncWorks))]
        public async Task SessionInsertKeyAsyncWorks(bool asTxn)
        {
            //Arrange
            string testKeyName = "testKeyName";
            IntPtr keyHandle = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            IntPtr initialKeyHandle = await session.FetchKeyAsync(testKeyName);

            //Act
            bool actual = await session.InsertKeyAsync(keyHandle, testKeyName);

            IntPtr fetchedKeyHandle = await session.FetchKeyAsync(testKeyName);
            string fetchedKeyName = await ResultListApi.KeyEntryListGetNameAsync(fetchedKeyHandle, 0);

            //Assert
            _ = actual.Should().BeTrue();
            _ = initialKeyHandle.Should().Be(new IntPtr());
            _ = fetchedKeyHandle.Should().NotBe(new IntPtr());
            _ = fetchedKeyName.Should().Be(testKeyName);
        }
        private static IEnumerable<TestCaseData> CreateCasesInsertKeyAsyncThrows()
        {
            string testName = "testName";
            IntPtr validKeyHandle = KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true).GetAwaiter().GetResult();

            yield return new TestCaseData(validKeyHandle, null, false, true)
                .SetName("InsertKeyAsync() throws with no name provided.");
            yield return new TestCaseData(new IntPtr(), testName, false, true)
                .SetName("InsertKeyAsync() throws with invalid keyHandle.");
            yield return new TestCaseData(validKeyHandle, testName, true, true)
                .SetName("InsertKeyAsync() callback throws with inserting duplicate key.");
            yield return new TestCaseData(validKeyHandle, testName, false, false)
                .SetName("InsertKeyAsync() throws wrapper erro with session handle equals 0.");
        }
        [Test, TestCaseSource(nameof(CreateCasesInsertKeyAsyncThrows))]
        public async Task SessionInsertKeyAsyncThrows(IntPtr keyHandle, string testName, bool doubleInsert, bool sessionHandleExisting)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            if (doubleInsert)
            {
                _ = await session.InsertKeyAsync(keyHandle, testName);
            }

            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.InsertKeyAsync(keyHandle, testName);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region update key
        private static IEnumerable<TestCaseData> CreateCasesUpdateKeyAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("UpdateKeyAsync() works for session resulting in changed metadata and tags.");
            yield return new TestCaseData(true)
                .SetName("UpdateKeyAsync() works for transaction resulting in changed metadata and tags.");
        }
        [Test, TestCaseSource(nameof(CreateCasesUpdateKeyAsyncWorks))]
        public async Task SessionUpdateKeyAsyncWorks(bool asTxn)
        {
            //Arrange
            string testKeyName = "testKeyName";
            string testMetadata = "testKeyMetadata";
            string testTag = $"{{\"~plaintag\":\"plainKeyTag\"}}";
            string newTestMetadata = "newTestKeyMetadata";
            string newTestTag = $"{{\"~plaintag\":\"newPlainKeyTag\"}}";
            IntPtr keyHandle = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            bool insertResult = await session.InsertKeyAsync(keyHandle, testKeyName, testMetadata, testTag);
            IntPtr initialKeyHandle = await session.FetchKeyAsync(testKeyName);
            _ = await ResultListApi.KeyEntryListGetMetadataAsync(initialKeyHandle, 0);
            _ = await ResultListApi.KeyEntryListGetTagsAsync(initialKeyHandle, 0);

            //Act
            bool actual = await session.UpdateKeyAsync(testKeyName, newTestMetadata, newTestTag);

            IntPtr fetchedKeyHandle = await session.FetchKeyAsync(testKeyName);
            string fetchedKeyMetadata = await ResultListApi.KeyEntryListGetMetadataAsync(fetchedKeyHandle, 0);
            string fetchedKeyTags = await ResultListApi.KeyEntryListGetTagsAsync(fetchedKeyHandle, 0);

            //Assert
            _ = actual.Should().BeTrue();
            _ = insertResult.Should().BeTrue();
            _ = fetchedKeyHandle.Should().NotBe(new IntPtr());
            _ = fetchedKeyMetadata.Should().Be(newTestMetadata);
            _ = fetchedKeyTags.Should().Be(newTestTag);
        }

        private static IEnumerable<TestCaseData> CreateCasesUpdateKeyAsyncThrows()
        {
            yield return new TestCaseData(null, true)
                .SetName("UpdateKeyAsync() throws with no key name provided.");
            yield return new TestCaseData("", true)
                .SetName("UpdateKeyAsync() callback throws with providing wrong keyName to non existing key.");
            yield return new TestCaseData("testKeyName", false)
                .SetName("UpdateKeyAsync() throws wrapper error with session handle equals 0.");
        }
        [Test, TestCaseSource(nameof(CreateCasesUpdateKeyAsyncThrows))]
        public async Task SessionUpdateKeyAsyncThrows(string testCaseKeyName, bool sessionHandleExisting)
        {
            //Arrange
            string testKeyName = "testKeyName";
            string testMetadata = "testKeyMetadata";
            string testTag = $"{{\"~plaintag\":\"plainKeyTag\"}}";
            IntPtr keyHandle = KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true).GetAwaiter().GetResult();

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool insertResult = await session.InsertKeyAsync(keyHandle, testKeyName, testMetadata, testTag);
            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.UpdateKeyAsync(testCaseKeyName);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region remove key
        private static IEnumerable<TestCaseData> CreateCasesRemoveKeyAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("RemoveKeyAsync() works for session.");
            yield return new TestCaseData(true)
                .SetName("RemoveKeyAsync() works for transaction.");
        }
        [Test, TestCaseSource(nameof(CreateCasesRemoveKeyAsyncWorks))]
        public async Task SessionRemoveKeyAsyncWorks(bool asTxn)
        {
            //Arrange
            string testKeyName = "testKeyName";
            IntPtr keyHandle = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            _ = await session.InsertKeyAsync(keyHandle, testKeyName);
            IntPtr initialKeyHandle = await session.FetchKeyAsync(testKeyName);

            //Act
            bool actual = await session.RemoveKeyAsync(testKeyName);
            IntPtr fetchedKeyHandle = await session.FetchKeyAsync(testKeyName);

            //Assert
            _ = actual.Should().BeTrue();
            _ = initialKeyHandle.Should().NotBe(new IntPtr());
            _ = fetchedKeyHandle.Should().Be(new IntPtr());
        }

        private static IEnumerable<TestCaseData> CreateCasesRemoveKeyAsyncThrows()
        {
            string testCaseKeyName = "testKeyName";
            yield return new TestCaseData(false, null, false, true)
                .SetName("RemoveKeyAsync() throws for session with invalid keyName.");
            yield return new TestCaseData(true, null, false, true)
                .SetName("RemoveKeyAsync() throws for transaction with invalid keyName.");
            yield return new TestCaseData(false, testCaseKeyName, true, true)
                .SetName("RemoveKeyAsync() callback throws for session with trying to remove a already removed key.");
            yield return new TestCaseData(true, testCaseKeyName, true, true)
                .SetName("RemoveKeyAsync() callback throws for transaction with trying to remove a already removed key.");
            yield return new TestCaseData(false, testCaseKeyName, false, false)
                .SetName("RemoveKeyAsync() throws wrapper error with session handle equals 0.");
        }
        [Test, TestCaseSource(nameof(CreateCasesRemoveKeyAsyncThrows))]
        public async Task SessionRemoveKeyAsyncThrows(bool asTxn, string testCaseInputKeyName, bool doubleRemove, bool sessionHandleExisting)
        {
            //Arrange
            string testKeyName = "testKeyName";
            IntPtr keyHandle = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            _ = await session.InsertKeyAsync(keyHandle, testKeyName);
            if (doubleRemove)
            {
                _ = await session.RemoveKeyAsync(testCaseInputKeyName);
            }

            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.RemoveKeyAsync(testCaseInputKeyName);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region fetch key / fetch all keys
        private static IEnumerable<TestCaseData> CreateCasesFetchKeyAsyncWorks()
        {
            yield return new TestCaseData(false, true)
                .SetName("FetchKeyAsync() works for session with forUpdate equals true and returns the inserted key.");
            yield return new TestCaseData(false, false)
                .SetName("FetchKeyAsync() works for session with forUpdate equals false and returns the inserted key.");
            yield return new TestCaseData(true, true)
                .SetName("FetchKeyAsync() works for transaction with forUpdate equals true and returns the inserted key.");
            yield return new TestCaseData(true, false)
                .SetName("FetchKeyAsync() works for transaction with forUpdate equals false and returns the inserted key.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchKeyAsyncWorks))]
        public async Task SessionFetchKeyAsyncWorks(bool asTxn, bool forUpdate)
        {
            //Arrange
            string testKeyName = "testKeyName";
            IntPtr keyHandle = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            _ = await session.InsertKeyAsync(keyHandle, testKeyName);

            //Act
            IntPtr actual = await session.FetchKeyAsync(testKeyName, forUpdate);
            string fetchedKeyName = await ResultListApi.KeyEntryListGetNameAsync(actual, 0);

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
            _ = fetchedKeyName.Should().Be(testKeyName);
        }

        private static IEnumerable<TestCaseData> CreateCasesFetchKeyAsyncThrows()
        {
            yield return new TestCaseData(null, true)
                .SetName("FetchKeyAsync() throws for session with not providing key name.");
            yield return new TestCaseData("testKeyName", false)
                .SetName("FetchKeyAsync() throws wrapper error for session with session handle equals 0.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchKeyAsyncThrows))]
        public async Task SessionFetchKeyAsyncThrows(string testCaseKeyName, bool sessionHandleExisting)
        {
            //Arrange
            string testKeyName = "testKeyName";
            IntPtr keyHandle = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert1 = await session.InsertKeyAsync(keyHandle, testKeyName);
            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.FetchKeyAsync(testCaseKeyName);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        private static IEnumerable<TestCaseData> CreateCasesFetchAllKeyAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("FetchAllKeyAsync() works for session.");
            yield return new TestCaseData(true)
                .SetName("FetchAllKeyAsync() works for transaction.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchAllKeyAsyncWorks))]
        public async Task SessionFetchAllKeyAsyncWorks(bool asTxn)
        {
            //Arrange
            string testKeyName1 = "testKeyName1_A128CBC";
            string testKeyName2 = "testKeyName2_A128CBC";
            string testKeyName3 = "testKeyNameC20P";
            IntPtr keyHandle1 = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);
            IntPtr keyHandle2 = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);
            IntPtr keyHandle3 = await KeyApi.CreateKeyAsync(KeyAlg.C20P, true);

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(asTransactions: asTxn);
            _ = await session.InsertKeyAsync(keyHandle1, testKeyName1);
            _ = await session.InsertKeyAsync(keyHandle2, testKeyName2);
            _ = await session.InsertKeyAsync(keyHandle3, testKeyName3);

            //Act
            IntPtr actual = await session.FetchAllKeysAsync(KeyAlg.A128CBC_HS256);
            string fetchedKeyName1 = await ResultListApi.KeyEntryListGetNameAsync(actual, 0);
            string fetchedKeyName2 = await ResultListApi.KeyEntryListGetNameAsync(actual, 1);
            List<string> names = new() { fetchedKeyName1, fetchedKeyName2 };

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
            _ = names.Should().Contain(testKeyName1);
            _ = names.Should().Contain(testKeyName2);
            _ = names.Should().NotContain(testKeyName3);
        }

        private static IEnumerable<TestCaseData> CreateCasesFetchAllKeyAsyncThrows()
        {
            yield return new TestCaseData(false)
                .SetName("FetchAllKeyAsync() throws wrapper error for session with session handle equals 0.");
        }
        [Test, TestCaseSource(nameof(CreateCasesFetchAllKeyAsyncThrows))]
        public async Task SessionFetchAllKeyAsyncThrows(bool sessionHandleExisting)
        {
            //Arrange
            string testKeyName1 = "testKeyName1_A128CBC";
            IntPtr keyHandle1 = await KeyApi.CreateKeyAsync(KeyAlg.A128CBC_HS256, true);

            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool initInsert1 = await session.InsertKeyAsync(keyHandle1, testKeyName1);
            if (!sessionHandleExisting)
            {
                _ = await store.CloseAsync();
            }

            //Act
            Func<Task> actual = async () => await session.FetchAllKeysAsync(KeyAlg.A128CBC_HS256);

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region close and commit session
        private static IEnumerable<TestCaseData> CreateCasesCloseAndCommitAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("CloseAndCommitAsync() works for session.");
            yield return new TestCaseData(true)
                .SetName("CloseAndCommitAsync() works for transaction.");
        }

        [Test, TestCaseSource(nameof(CreateCasesCloseAndCommitAsyncWorks))]
        public async Task CloseAndCommitAsyncWorks(bool isTxn)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, isTxn);

            //Act
            bool actual = await session.CloseAndCommitAsync();

            //Assert
            _ = actual.Should().BeTrue();
            _ = session.sessionHandle.Should().Be(new IntPtr());
        }

        [Test, TestCase(TestName = "CloseAndCommitAsync() throws for session handle equals 0.")]
        public async Task CloseAndCommitAsyncThrowsSessionHandle0()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, true);
            _ = await session.CloseAndCommitAsync();

            //Act
            Func<Task> actual = async () => await session.CloseAndCommitAsync();

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "CloseAndCommitAsync() throws for session equals null.")]
        public async Task CloseAndCommitAsyncThrowsSessionNull()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);

            //Act
            Func<Task> actual = async () => await store.session.CloseAndCommitAsync();

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #region close and rollback session
        private static IEnumerable<TestCaseData> CreateCasesCloseAndRollbackAsyncWorks()
        {
            yield return new TestCaseData(false)
                .SetName("CloseAndRollbackAsync() works for session.");
            yield return new TestCaseData(true)
                .SetName("CloseAndRollbackAsync() works for transaction.");
        }

        [Test, TestCaseSource(nameof(CreateCasesCloseAndRollbackAsyncWorks))]
        public async Task CloseAndRollbackAsyncWorks(bool isTxn)
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, isTxn);

            //Act
            bool actual = await session.CloseAndRollbackAsync();

            //Assert
            _ = actual.Should().BeTrue();
            _ = session.sessionHandle.Should().Be(new IntPtr());
        }

        [Test, TestCase(TestName = "CloseAndRollbackAsync() throws for session handle equals 0.")]
        public async Task CloseAndRollbackAsyncThrowsSessionHandle0()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync(testProfile, true);
            _ = await session.CloseAndCommitAsync();

            //Act
            Func<Task> actual = async () => await session.CloseAndRollbackAsync();

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }

        [Test, TestCase(TestName = "CloseAndRollbackAsync() throws for session equals null.")]
        public async Task CloseAndRollbackAsyncThrowsSessionNull()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);

            //Act
            Func<Task> actual = async () => await store.session.CloseAndRollbackAsync();

            //Assert
            _ = await actual.Should().ThrowAsync<Exception>();
        }
        #endregion

        #endregion

        #region SCAN

        #region next scan
        private static IEnumerable<TestCaseData> CreateCasesNextScanAsyncWorks()
        {
            string testProfile = "testProfile";
            string testTag = $"{{ \"~plaintag\": \"a\", \"enctag\": \"b\"}}";

            yield return new TestCaseData(testTag, 0, -1, testProfile, 3)
                .SetName("NextScanAsync() works with no offset and limit and returns entryListHandle to scanned records.");
            yield return new TestCaseData(testTag, 1, -1, testProfile, 2)
                .SetName("NextScanAsync() works with offset of 1 and returns entryListHandle to the scanned records (2 of 3 elements).");
            yield return new TestCaseData(testTag, 0, 1, testProfile, 1)
                .SetName("NextScanAsync() works with offset 0 and limit of 1 and returns entryListHandle to the scanned records (1 of 3 elements).");
        }
        [Test, TestCaseSource(nameof(CreateCasesNextScanAsyncWorks))]
        public async Task NextScanAsyncWorks(string tag, long offset, long limit, string profile, int expected)
        {
            //Arrange
            List<string> testNames = new() { "testName1", "testName2", "testName3" };
            List<string> testValues = new() { "testVal1", "testVal2", "testVal3" };
            long maxElements = testNames.Count;
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(testEntry["category"].ToString(), testNames[0], testValues[0], tag);
            _ = await session.InsertAsync(testEntry["category"].ToString(), testNames[1], testValues[1], tag);
            _ = await session.InsertAsync(testEntry["category"].ToString(), testNames[2], testValues[2], tag);
            Scan scan = await store.StartScanAsync(testEntry["category"].ToString(), tag, offset, limit, profile);

            //Act
            IntPtr actual = await scan.NextAsync();

            int max = (int)limit;
            if (limit == -1)
            {
                max = (int)(maxElements - offset);
            }
            List<string> names = new();
            List<string> values = new();

            for (int n = 0; n < max; n++)
            {
                string name = await ResultListApi.EntryListGetNameAsync(actual, n);
                string value = await ResultListApi.EntryListGetValueAsync(actual, n);
                names.Add(name);
                values.Add(value);
            }

            //Assert
            _ = actual.Should().NotBe(new IntPtr());
            _ = names.Count.Should().Be(expected);
            _ = values.Count.Should().Be(expected);

        }
        #endregion

        #region free scan
        [Test, TestCase(TestName = "FreeScanAsync() works.")]
        public async Task FreeScanAsyncWorks()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(_testUriInMemory, testKeyMethod, testPassKey, testProfile);
            Scan scan = await store.StartScanAsync(testEntry["category"].ToString());

            //Act
            bool actual = await scan.FreeAsync();

            //Assert
            _ = actual.Should().BeTrue();
            _ = scan.scanHandle.Should().Be(new IntPtr());
            _ = scan.storeHandle.Should().Be(new IntPtr());
            _ = scan.parameters.Should().BeNullOrEmpty();
        }
        #endregion

        #endregion
    }
}
