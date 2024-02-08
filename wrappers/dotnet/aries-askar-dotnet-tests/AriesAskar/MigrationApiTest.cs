using aries_askar_dotnet.AriesAskar;
using aries_askar_dotnet.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class MigrationApiTest
    {
        private string _testPathDb;
        private string _dbType;
        private string _testUriInMemory;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _dbType = "sqlite";
            _testUriInMemory = "sqlite://:memory:";
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string testPathDb = Path.Combine(currentDirectory, @"..\..\..\Resources\indy_wallet_sqlite.db");
            _testPathDb = _dbType + "://" + Path.GetFullPath(testPathDb);
        }

        [Test, TestCase(TestName = "MigrateIndySdkAsync() call returns a result string.")]
        public async Task MigrateIndySdkAsyncWorks()
        {
            string specuri = _testPathDb;
            string walletname = "walletwallet.0";
            string walletkey = "GfwU1DC7gEZNs3w41tjBiZYj7BNToDoFEqKY6wZXqs1A";
            string kdflevel = "RAW";

            //Act
            bool actual = await MigrationApi.MigrateIndySdkAsync(specuri, walletname, walletkey, kdflevel);

            //Assert
            _ = actual.Should().Be(true);
        }

        [Test, TestCase(TestName = "MigrateIndySdkAsync() call with invalid db throws.")]
        public async Task MigrateIndySdkAsyncThrows()
        {
            string specuri = null;
            string walletname = "walletwallet.0";
            string walletkey = "GfwU1DC7gEZNs3w41tjBiZYj7BNToDoFEqKY6wZXqs1A";
            string kdflevel = "RAW";

            //Act
            Func<Task> func = async () => await MigrationApi.MigrateIndySdkAsync(specuri, walletname, walletkey, kdflevel);

            //Assert
            await func.Should().ThrowAsync<Exception>();
        }

    }
}
