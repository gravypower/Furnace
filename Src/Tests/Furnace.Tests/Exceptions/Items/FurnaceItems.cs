using System.Collections.Generic;
using NUnit.Framework;

namespace Furnace.Tests.Exceptions.Items
{
    [TestFixture]
    public abstract class FurnaceItems : FurnaceExceptionTests
    {
        public class NullContentTypeException : FurnaceItems
        {
            const string Message = "FurnaceItems.NullContentTypeException thrown.";

            public override string ExcationName
            {
                get
                {
                    return "FurnaceItems.NullContentTypeException";
                }
            }

            [SetUp]
            public void InvalidContentTypeExceptionSetUp()
            {
                Sut = new Furnace.Items.FurnaceItems.NullContentTypeException();
            }

            [Test]
            public void WhenFurnaceExceptionIsTrown_WithOneReason_ThenLogMessage_IsCorrect()
            {
                Assert.That(Sut.Message, Is.EqualTo(Message));
            }
        }

        public class InvalidContentTypeException : FurnaceItems
        {
            const string BaseMessage = "FurnaceItems.InvalidContentTypeException thrown. InvalidReasons: ";

            protected IEnumerable<string> Reasons;

            public override string ExcationName
            {
                get { return "FurnaceItems.InvalidContentTypeException"; }
            }

            [SetUp]
            public void InvalidContentTypeExceptionSetUp()
            {
                Sut = new Furnace.Items.FurnaceItems.InvalidContentTypeException(YieldReasons());
            }

            [Test]
            public void WhenFurnaceExceptionIsTrown_WithOneReason_ThenLogMessage_IsCorrect()
            {
                const string reason = "Test Message One";
                const string message = BaseMessage + reason;
                Reasons = new List<string> { reason };

                Assert.That(Sut.Message, Is.EqualTo(message));
            }

            [Test]
            public void WhenFurnaceExceptionIsTrown_WithTwoReasons_ThenLogMessage_IsCorrect()
            {
                const string reasonOne = "Test Message One";
                const string reasonTwo = "Test Message Two";

                const string message = BaseMessage + reasonOne + ", " + reasonTwo;
                Reasons = new List<string> { reasonOne, reasonTwo };

                Assert.That(Sut.Message, Is.EqualTo(message));
            }
            
            private IEnumerable<string> YieldReasons()
            {
                foreach (var reasion in Reasons)
                {
                    yield return reasion;
                }
            }
        }
    }
}
