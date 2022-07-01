using aries_askar_dotnet;
using aries_askar_dotnet.AriesAskar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class ResultListApiTests
    {
        private Dictionary<string, string> testEntry;
        private Dictionary<string, string> testEntry2;
        private Dictionary<string, string> testKeyEntry;
        private Dictionary<string, string> testKeyEntry2;
        private string testSpecUri;
        private KeyMethod testKeyMethod;
        private string testPassKey;
        private string testProfile;
        private string testSeed;
        private bool testRecreate;
        private bool testAsTransactions;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            testEntry = new()
            {
                { "name", "testName" },
                { "category", "testCategory" },
                { "value", "testValue" },
                //{ "tags", $"{{\"~plaintag\": \"a\", \"enctag\": {{ \"b\", \"c\" }} }}" }
                { "tags", $"{{ \"~plaintag\": \"a\", \"enctag\": \"b\"}}" }
            };

            testEntry2 = testEntry;

            testKeyEntry = new()
            {
                { "key_name", "testKey" },
                { "metadata", "testMetadata" },
                { "tags", $"{{ \"a\" : \"b\" }}" }
            };

            testSpecUri = "sqlite://:memory:";
            testSeed = "testseed000000000000000000000001";
            testPassKey = await StoreApi.StoreGenerateRawKeyAsync(testSeed);
            testKeyMethod = KeyMethod.RAW;  //kdf:argon2i   //none
            testProfile = "testProfile";
            testRecreate = true;
            testAsTransactions = true;
        }

        [Test, TestCase(TestName = "EntryListCountAsync returns the count.")]
        public async Task EntryListCountAsyncWorks()
        {
            //// Arrange
            //Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            //Session session = await store.StartSessionAsync();
            //bool wasInserted = await session.InsertAsync(
            //    testEntry["category"].ToString(),
            //    testEntry["name"].ToString(),
            //    testEntry["value"].ToString(),
            //    testEntry["tags"].ToString());
            //// Act
            //IntPtr entryListHandle = await session.FetchAllAsync(
            //    testEntry["category"].ToString());

            //int count = await ResultListApi.EntryListCountAsync(entryListHandle);
            //// Assert

            //count.Should().BeGreaterThan(1);
        }

        [Test, TestCase(TestName = "EntryListGetCategoryAsync returns the category.")]
        public async Task EntryListGetCategoryAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            // Act
            IntPtr entryListHandle = await session.FetchAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString());

            string category = await ResultListApi.EntryListGetCategoryAsync(entryListHandle, 0);
            // Assert

            category.Should().Be("testCategory");
        }
        [Test, TestCase(TestName = "EntryListGetCategoryAsync with invalid entryListHandle throws")]
        public async Task EntryListGetCategoryAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();
            
            // Act

            Func<Task> func= async () => await ResultListApi.EntryListGetCategoryAsync(entryListHandle, 0);
            // Assert

            await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "EntryListGetNameAsync returns the name.")]
        public async Task EntryListGetNameAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            IntPtr entryListHandle = await session.FetchAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString());
            // Act

            string category = await ResultListApi.EntryListGetNameAsync(entryListHandle, 0);

            // Assert
            category.Should().Be("testName");
        }

        [Test, TestCase(TestName = "EntryListGetNameAsync with invalid keyListHandle throws.")]
        public async Task EntryListGetNameAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();

            // Act

            Func<Task> func = async () => await ResultListApi.EntryListGetNameAsync(entryListHandle, 0);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }


        [Test, TestCase(TestName = "EntryListGetValueAsync returns the value.")]
        public async Task EntryListGetValueAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            IntPtr entryListHandle = await session.FetchAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString());
            // Act

            ByteBuffer buffer = await ResultListApi.EntryListGetValueAsync(entryListHandle, 0);
            string bufferString = buffer.DecodeToString();
            // Assert
            bufferString.Should().Be("testValue");
        }

        [Test, TestCase(TestName = "EntryListGetValueAsync with invalid keyListHandle throws.")]
        public async Task EntryListGetValueAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();

            // Act

            Func<Task> func = async () => await ResultListApi.EntryListGetValueAsync(entryListHandle, 0);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }


        [Test, TestCase(TestName = "EntryListGetTagsAsync returns the tag.")]
        public async Task EntryListGetTagsAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            IntPtr entryListHandle = await session.FetchAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString());
            // Act

            string tag = await ResultListApi.EntryListGetTagsAsync(entryListHandle, 0);
            // Assert
            tag.Should().Be("{\"enctag\":\"b\",\"~plaintag\":\"a\"}");
        }

        [Test, TestCase(TestName = "EntryListGetTagsAsync with invalid keyListHandle throws.")]
        public async Task EntryListGetTagsAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();

            // Act

            Func<Task> func = async () => await ResultListApi.EntryListGetTagsAsync(entryListHandle, 0);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "EntryListFreeAsync frees the  given entryListHandle and returns true.")]
        public async Task EntryListFreeAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            IntPtr entryListHandle = await session.FetchAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString());
            // Act

            Func<Task<string>> nameBeforeFreeing = async () => await ResultListApi.EntryListGetNameAsync(entryListHandle, 0);
            Func<Task<bool>> freed = async () => await ResultListApi.EntryListFreeAsync(entryListHandle);
            // Assert
            nameBeforeFreeing.Invoke().Result.Should().Be("testName");
            await freed.Should().ThrowAsync<AriesAskarException>().WithMessage("'Success' error occured with ErrorCode '0' : .");
        }

        //[Test, TestCase(TestName = "EntryListFreeAsync with invalid entryListHandle throws.")]
        //public async Task EntryListFreeAsyncThrows()
        //{
        //    // Arrange
        //    IntPtr entryListHandle = new();

        //    // Act
        //    Func<Task<bool>> func = async () => await ResultListApi.EntryListFreeAsync(entryListHandle);

        //    // Assert
        //    await func.Should().ThrowAsync<AriesAskarException>().WithMessage("dwa");
        //}

        // TODO 

        //[Test, TestCase(TestName = "KeyEntryListCountAsync returns the count.")]
        //public async Task KeyEntryListCountAsyncWorks()
        //{
        //    // Arrange
        //    Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
        //    Session session = await store.StartSessionAsync();
        //    bool wasInserted = await session.InsertKeyAsync(
        //        KeyApi.CreateKeyAsync(KeyAlg.ED25519, 1).GetAwaiter().GetResult(),
        //        testKeyEntry["key_name"],
        //        testKeyEntry["metadata"],
        //        testKeyEntry["tags"]);
        //    IntPtr keyEntryListHandle = await session.FetchAllKeysAsync();
        //    // Act
        //    int count = await ResultListApi.KeyEntryListCountAsync(keyEntryListHandle);

        //    // Assert
        //    count.Should().Be(1);
        //}

        [Test, TestCase(TestName = "KeyEntryListCountAsync with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListCountAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<int>> func = async () => await ResultListApi.KeyEntryListCountAsync(keyEntryListHandle);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetAlgorithmAsyncWorks returns the algorithm.")]
        public async Task KeyEntryListGetAlgorithmAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, 1).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetAlgorithmAsync(keyEntryListHandle, 0);

            // Assert
            algo.Should().Be("ed25519");
        }

        [Test, TestCase(TestName = "KeyEntryListGetAlgorithmAsyncWorks with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetAlgorithmAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetAlgorithmAsync(keyEntryListHandle, 0);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetNameAsync returns the name.")]
        public async Task KeyEntryListGetNameAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, 1).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetNameAsync(keyEntryListHandle, 0);

            // Assert
            algo.Should().Be("testKey");
        }

        [Test, TestCase(TestName = "KeyEntryListGetNameAsync with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetNameAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetNameAsync(keyEntryListHandle, 0);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetMetadataAsync returns the metadata.")]
        public async Task KeyEntryListGetMetadataAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, 1).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetMetadataAsync(keyEntryListHandle, 0);

            // Assert
            algo.Should().Be("testMetadata");
        }

        [Test, TestCase(TestName = "KeyEntryListGetMetadataAsync with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetMetadataAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetMetadataAsync(keyEntryListHandle, 0);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetTagsAsync returns the metadata.")]
        public async Task KeyEntryListGetTagsAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            bool wasInserted = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, 1).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetTagsAsync(keyEntryListHandle, 0);

            // Assert
            algo.Should().Be($"{{\"a\":\"b\"}}");
        }

        [Test, TestCase(TestName = "KeyEntryListGetTagsAsync with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetTagsAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetTagsAsync(keyEntryListHandle, 0);

            // Assert
            await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "Test some EntryList methods.")]
        public async Task SomeEntryListMethodsWork()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            string test = $"{{ \"~plaintag\": \"a\", \"enctag\": \"b\"}}";
            //Act
            bool actual = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString()
                );
            long count = await session.CountAsync(
                testEntry["category"].ToString(),
                $"{{ \"~plaintag\": \"a\", \"enctag\": \"b\"}}");

            IntPtr entry = await session.FetchAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString());

            string catName = await ResultListApi.EntryListGetCategoryAsync(entry, 0);
            string nameName = await ResultListApi.EntryListGetNameAsync(entry, 0);
            ByteBuffer valName = await ResultListApi.EntryListGetValueAsync(entry, 0);
            string tagName = await ResultListApi.EntryListGetTagsAsync(entry, 0);

            //Assert
            actual.Should().BeTrue();
            count.Should().Be(1);
        }
    }
}
