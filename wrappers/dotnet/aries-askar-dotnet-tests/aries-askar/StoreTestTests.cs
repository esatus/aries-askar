using aries_askar_dotnet.aries_askar;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.aries_askar
{
    class StoreTestTests
    {
        [Test, TestCase(TestName = "SessionUpdateAsync call returns store handle.")]
        public async Task SessionUpdate()
        {
            //Arrange
            StoreTest store = await StoreTest.StoreProvisionAsync("sqlite://:memory:");
            string key = await store.StoreGenerateRawKeyAsync("seed");


            //Act
            bool success = await store.StoreRemoveProfileAsync(store,"test");

            //Assert
        }
    }
}
