using aries_askar_dotnet.AriesAskar;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class LogApiTest
    {


        [Test, TestCase(TestName = "SetCustomLoggerAsyncWorks() call returns a result int.")]
        public async Task SetCustomLoggerAsyncWorks()
        {
            int maxlevel = 1;
            IntPtr context = new IntPtr();
            //Act
            int actual = await LogApi.SetCustomLoggerAsync(context, maxlevel);

            //Assert
            _ = actual.Should().Be(0);

        }


        [Test, TestCase(TestName = "ClearCustomLoggerAsyncWorks() call returns a result string.")]
        public async Task ClearCustomLoggerAsyncWorks()
        {
            IntPtr context = new IntPtr();
            int maxlevel = 1;
            //Act
            int actual = await LogApi.SetCustomLoggerAsync(context, maxlevel);
            //Act
            await LogApi.ClearCustomLoggerAsync();

            //Assert
            _ = actual.Should().Be(0);

        }

        [Test, TestCase(TestName = "SetDefaultLoggerAsyncWorks() call returns a result string.")]
        public async Task SetDefaultLoggerAsyncWorks()
        {
            //Act
            int actual = await LogApi.SetDefaultLoggerAsync();

            //Assert
            _ = actual.Should().Be(0);

        }

        [Test, TestCase(TestName = "SetMaxLogLevelAsyncWorks() call returns a result int.")]
        public async Task SetMaxLogLevelAsyncWorks() 
        {
            int maxlevel = 1;

            //Act
            int actual = await LogApi.SetMaxLogLevelAsync(maxlevel);

            //Assert
            _ = actual.Should().Be(0);

        }


    }
}
