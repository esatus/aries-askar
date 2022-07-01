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
        Dictionary<string, string> testEntry;
        string testSpecUri;
        KeyMethod testKeyMethod;
        string testPassKey;
        string testProfile;
        string testSeed;
        bool testRecreate;
        bool testAsTransactions;

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

            testSpecUri = "sqlite://:memory:";
            testSeed = "testseed000000000000000000000001";
            testPassKey = await StoreApi.StoreGenerateRawKeyAsync(testSeed);
            testKeyMethod = KeyMethod.RAW;  //kdf:argon2i   //none
            testProfile = "testProfile";
            testRecreate = true;
            testAsTransactions = true;
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
