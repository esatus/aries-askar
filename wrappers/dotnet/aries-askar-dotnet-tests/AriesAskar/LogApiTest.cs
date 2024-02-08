using aries_askar_dotnet;
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

        [Test, TestCase(TestName = "SetCustomLoggerAsyncWorks() call with invalid maxlevel throws.")]
        public async Task SetCustomLoggerAsyncThrows()
        {
            int maxlevel = 6;
            IntPtr context = new();

            //Act
            Func<Task> func = async () => await LogApi.SetCustomLoggerAsync(context, maxlevel);

            //Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }

        [Test, TestCase(TestName = "SetDefaultLoggerAsyncWorks() call returns a result int.")]
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

        [Test, TestCase(TestName = "SetMaxLogLevelAsyncWorks() call with invalid maxlevel throws.")]
        public async Task SetMaxLogLevelAsyncThrows()
        {
            int maxlevel = 6;

            //Act
            Func<Task> func = async () => await LogApi.SetMaxLogLevelAsync(maxlevel);

            //Assert
            _ = await func.Should().ThrowAsync<AriesAskarException>();
        }
    }
}
