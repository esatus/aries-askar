﻿using aries_askar_dotnet;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aries_askar_dotnet_tests
{
    public class AriesAskarExceptionTests
    {
        private static IEnumerable<TestCaseData> CreateErrorCodeCases()
        {
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "0", "Success")
                .SetName("AriesAskarException contains ErrorCode 'Success' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "1", "Backend")
                .SetName("AriesAskarException contains ErrorCode 'Backend' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "2", "Busy")
                .SetName("AriesAskarException contains ErrorCode 'Busy' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "3", "Duplicate")
                .SetName("AriesAskarException contains ErrorCode 'Duplicate' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "4", "Encryption")
                .SetName("AriesAskarException contains ErrorCode 'Encryption' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "5", "Input")
                .SetName("AriesAskarException contains ErrorCode 'Input' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "6", "NotFound")
                .SetName("AriesAskarException contains ErrorCode 'NotFound' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "7", "Unexpected")
                .SetName("AriesAskarException contains ErrorCode 'Unexpected' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "8", "Unsupported")
                .SetName("AriesAskarException contains ErrorCode 'Unsupported' text after parsing the code to string.");
            yield return new TestCaseData("message matching to rust errorCode", "some exta from rust errorCode", "100", "Custom")
                .SetName("AriesAskarException contains ErrorCode 'Custom' text after parsing the code to string.");
            yield return new TestCaseData("no message", "some exta from rust errorCode", "999", "Unknown error code")
                .SetName("AriesAskarException contains 'Unknown error code' text after trying to parse an unknown errorCode.");
            yield return new TestCaseData("no message", "some exta from rust errorCode", "xyz", "An unknown error code was received.")
                .SetName("AriesAskarException contains 'An unknown error code was received' text after trying to parse an non integer errorCode.");
        }

        [Test, TestCaseSource(nameof(CreateErrorCodeCases))]
        public Task AriesAskarExceptionsRightMessages(string testMessage, string testExtra, string errorCode, string expected)
        {
            //Arrange
            string testErrorMessage = $"{{\"code\":\"{errorCode}\",\"message\":\"{testMessage}\",\"extra\":\"{testExtra}\" }}";

            //Act
            AriesAskarException testException = AriesAskarException.FromSdkError(testErrorMessage);
            string actual = errorCode != "xyz" ? testException.Message.Substring(1, expected.Length) : testException.Message;

            //Assert
            _ = actual.Should().Be(expected);
            return Task.CompletedTask;
        }
    }
}