using aries_askar_dotnet;
using aries_askar_dotnet.AriesAskar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            testEntry = new()
            {
                { "name", "testName" },
                { "category", "testCategory" },
                { "value", "testValue" },
                { "tags", $"{{ \"~plaintag\": \"a\", \"enctag\": \"b\"}}" }
            };

            testEntry2 = new()
            {
                { "name", "testName2" },
                { "category", "testCategory" },
                { "value", "testValue" },
                { "tags", $"{{ \"~plaintag\": \"a\", \"enctag\": \"b\"}}" }
            };

            testKeyEntry = new()
            {
                { "key_name", "testKey" },
                { "metadata", "testMetadata" },
                { "tags", $"{{ \"a\" : \"b\" }}" }
            };

            testKeyEntry2 = new()
            {
                { "key_name", "testKey2" },
                { "metadata", "testMetadata" },
                { "tags", $"{{ \"a\" : \"b\" }}" }
            };

            testSpecUri = "sqlite://:memory:";
            testSeed = "testseed000000000000000000000001";
            testPassKey = await StoreApi.GenerateRawKeyAsync(testSeed);
            testKeyMethod = KeyMethod.RAW;
            testProfile = "testProfile";
        }

        [Test, TestCase(TestName = "EntryListCountAsync() returns the count.")]
        public async Task EntryListCountAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            // Act
            IntPtr entryListHandle = await session.FetchAllAsync(
                testEntry["category"].ToString());

            int count = await ResultListApi.EntryListCountAsync(entryListHandle);

            // Assert
            _ = count.Should().Be(1);
        }

        [Test, TestCase(TestName = "EntryListCountAsync() returns the count with multiple entries.")]
        public async Task EntryListCountAsyncWorks2()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            _ = await session.InsertAsync(
                testEntry2["category"].ToString(),
                testEntry2["name"].ToString(),
                testEntry2["value"].ToString(),
                testEntry2["tags"].ToString());
            // Act
            IntPtr entryListHandle = await session.FetchAllAsync(
                testEntry["category"].ToString());

            int count = await ResultListApi.EntryListCountAsync(entryListHandle);

            // Assert
            _ = count.Should().Be(2);
        }

        [Test, TestCase(TestName = "EntryListCountAsync() with invalid entryListHandle throws.")]
        public async Task EntryListCountAsyncThrows()
        {
            // Arrange

            // Act
            IntPtr entryListHandle = new();

            Func<Task<int>> func = async () => await ResultListApi.EntryListCountAsync(entryListHandle);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "EntryListGetCategoryAsync() returns the category.")]
        public async Task EntryListGetCategoryAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(
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

            _ = category.Should().Be("testCategory");
        }
        [Test, TestCase(TestName = "EntryListGetCategoryAsync() with invalid entryListHandle throws")]
        public async Task EntryListGetCategoryAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();

            // Act

            Func<Task> func = async () => await ResultListApi.EntryListGetCategoryAsync(entryListHandle, 0);
            // Assert

            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "EntryListGetNameAsync() returns the name.")]
        public async Task EntryListGetNameAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(
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
            _ = category.Should().Be("testName");
        }

        [Test, TestCase(TestName = "EntryListGetNameAsync() with invalid keyListHandle throws.")]
        public async Task EntryListGetNameAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();

            // Act

            Func<Task> func = async () => await ResultListApi.EntryListGetNameAsync(entryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "EntryListGetValueAsync() returns the value.")]
        public async Task EntryListGetValueAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString(),
                testEntry["value"].ToString(),
                testEntry["tags"].ToString());
            IntPtr entryListHandle = await session.FetchAsync(
                testEntry["category"].ToString(),
                testEntry["name"].ToString());
            // Act

            string bufferString = await ResultListApi.EntryListGetValueAsync(entryListHandle, 0);
            // Assert
            _ = bufferString.Should().Be("testValue");
        }

        [Test, TestCase(TestName = "EntryListGetValueAsync() with invalid keyListHandle throws.")]
        public async Task EntryListGetValueAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();

            // Act

            Func<Task> func = async () => await ResultListApi.EntryListGetValueAsync(entryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "EntryListGetTagsAsync() returns the tag.")]
        public async Task EntryListGetTagsAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertAsync(
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
            _ = tag.Should().Be("{\"enctag\":\"b\",\"~plaintag\":\"a\"}");
        }

        [Test, TestCase(TestName = "EntryListGetTagsAsync() with invalid keyListHandle throws.")]
        public async Task EntryListGetTagsAsyncThrows()
        {
            // Arrange
            IntPtr entryListHandle = new();

            // Act

            Func<Task> func = async () => await ResultListApi.EntryListGetTagsAsync(entryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListCountAsync() returns the count.")]
        public async Task KeyEntryListCountAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchAllKeysAsync();
            // Act
            int count = await ResultListApi.KeyEntryListCountAsync(keyEntryListHandle);

            // Assert
            _ = count.Should().Be(1);
        }

        [Test, TestCase(TestName = "KeyEntryListCountAsync() returns the count with multiple key entries.")]
        public async Task KeyEntryListCountAsyncWorks2()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry2["key_name"],
                testKeyEntry2["metadata"],
                testKeyEntry2["tags"]);
            IntPtr keyEntryListHandle = await session.FetchAllKeysAsync();
            // Act
            int count = await ResultListApi.KeyEntryListCountAsync(keyEntryListHandle);

            // Assert
            _ = count.Should().Be(2);
        }

        [Test, TestCase(TestName = "KeyEntryListCountAsync() with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListCountAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<int>> func = async () => await ResultListApi.KeyEntryListCountAsync(keyEntryListHandle);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetAlgorithmAsync() works and returns the algorithm.")]
        public async Task KeyEntryListGetAlgorithmAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetAlgorithmAsync(keyEntryListHandle, 0);

            // Assert
            _ = algo.Should().Be("ed25519");
        }

        [Test, TestCase(TestName = "KeyEntryListGetAlgorithmAsync() works with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetAlgorithmAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetAlgorithmAsync(keyEntryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetNameAsync() returns the name.")]
        public async Task KeyEntryListGetNameAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetNameAsync(keyEntryListHandle, 0);

            // Assert
            _ = algo.Should().Be("testKey");
        }

        [Test, TestCase(TestName = "KeyEntryListGetNameAsync() with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetNameAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetNameAsync(keyEntryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetMetadataAsync() returns the metadata.")]
        public async Task KeyEntryListGetMetadataAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetMetadataAsync(keyEntryListHandle, 0);

            // Assert
            _ = algo.Should().Be("testMetadata");
        }

        [Test, TestCase(TestName = "KeyEntryListGetMetadataAsync() with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetMetadataAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetMetadataAsync(keyEntryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "KeyEntryListGetTagsAsync() returns the metadata.")]
        public async Task KeyEntryListGetTagsAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);
            // Act
            string algo = await ResultListApi.KeyEntryListGetTagsAsync(keyEntryListHandle, 0);

            // Assert
            _ = algo.Should().Be($"{{\"a\":\"b\"}}");
        }

        [Test, TestCase(TestName = "KeyEntryListGetTagsAsync() with invalid keyEntryListHandle throws.")]
        public async Task KeyEntryListGetTagsAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<string>> func = async () => await ResultListApi.KeyEntryListGetTagsAsync(keyEntryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "LoadLocalKeyHandleFromKeyEntryListAsync() returns a localKeyHandle.")]
        public async Task LoadLocalKeyHandleFromKeyEntryListAsyncWorks()
        {
            // Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            Session session = await store.StartSessionAsync();
            _ = await session.InsertKeyAsync(
                KeyApi.CreateKeyAsync(KeyAlg.ED25519, true).GetAwaiter().GetResult(),
                testKeyEntry["key_name"],
                testKeyEntry["metadata"],
                testKeyEntry["tags"]);
            IntPtr keyEntryListHandle = await session.FetchKeyAsync(
                testKeyEntry["key_name"]);

            // Act
            IntPtr localKeyHandle = await ResultListApi.LoadLocalKeyHandleFromKeyEntryListAsync(keyEntryListHandle, 0);

            // Assert
            _ = localKeyHandle.Should().NotBe((IntPtr)0);
        }

        [Test, TestCase(TestName = "LoadLocalKeyHandleFromKeyEntryListAsync() with invalid keyEntryListHandle throws.")]
        public async Task LoadLocalKeyHandleFromKeyEntryListAsyncThrows()
        {
            // Arrange
            IntPtr keyEntryListHandle = new();

            // Act
            Func<Task<IntPtr>> func = async () => await ResultListApi.LoadLocalKeyHandleFromKeyEntryListAsync(keyEntryListHandle, 0);

            // Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        #region Tests for SetStringListCountAsync
        [Test, TestCase(TestName = "StringListCountAsync() returns the count.")]
        public async Task StringListCountAsyncWork()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            IntPtr StringListHandle = await store.GetListProfilesAsync();
            // Act
            int count = await ResultListApi.StringListCountAsync(StringListHandle);

            // Assert
            _ = count.Should().Be(1);
        }
        #endregion
        [Test, TestCase(TestName = "GetItemStringListAsync() returns the count.")]
        public async Task GetItemStringListAsynccWork()
        {
            //Arrange
            Store store = await StoreApi.ProvisionAsync(testSpecUri, testKeyMethod, testPassKey, testProfile);
            IntPtr StringListHandle = await store.GetListProfilesAsync();
            // Act
            string item = await ResultListApi.GetItemStringListAsync(StringListHandle, 0);

            // Assert
            _ = item.Should().Be("testProfile");
        }
    }
}
