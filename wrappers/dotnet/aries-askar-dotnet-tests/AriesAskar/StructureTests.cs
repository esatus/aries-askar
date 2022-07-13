using aries_askar_dotnet.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using static aries_askar_dotnet.Models.Structures;

namespace aries_askar_dotnet_tests.AriesAskar
{
    public class StructureTests
    {
        #region EncryptedBuffer
        [Test, TestCaseSource(nameof(EncryptedBufferCases)), Category("EncryptedBuffer")]
        public async Task EncryptedBufferTests(string expectedValue, string expectedTag, string expectedNonce, long tagPos, long noncePos)
        {
            //Arrange
            ByteBuffer testValue = ByteBuffer.Create(expectedValue + expectedTag + expectedNonce);
            EncryptedBuffer testObject = new()
            {
                buffer = testValue,
                tag_pos = tagPos,
                nonce_pos = noncePos
            };

            //Act
            (byte[] actualValueBytes, byte[] actualTagBytes, byte[] actualNonceBytes) = testObject.Decode();
            ByteBuffer valueBuffer = ByteBuffer.Create(actualValueBytes);
            string actualValue = valueBuffer.DecodeToString();
            ByteBuffer tagBuffer = ByteBuffer.Create(actualTagBytes);
            string actualTag = tagBuffer.DecodeToString();
            ByteBuffer nonceBuffer = ByteBuffer.Create(actualNonceBytes);
            string actualNonce = nonceBuffer.DecodeToString();

            //Assert
            _ = actualValue.Should().Be(expectedValue);
            _ = actualTag.Should().Be(expectedTag);
            _ = actualNonce.Should().Be(expectedNonce);
        }

        private static IEnumerable<TestCaseData> EncryptedBufferCases()
        {
            yield return new TestCaseData("testMessageWithoutTagWithoutNonce", "", "", 0, 0)
                .SetName("test1");
            yield return new TestCaseData("testMessageWithTagWithoutNonce", "testTag", "", 30, 37)
                .SetName("test2");
            yield return new TestCaseData("testMessageWithoutTagWithNonce", "", "testNonce", 0, 30)
                .SetName("test3");
            yield return new TestCaseData("testMessageWithTagWithNonce", "testTag", "testNonce", 27, 34)
                .SetName("test4");
        }
        #endregion
    }
}
