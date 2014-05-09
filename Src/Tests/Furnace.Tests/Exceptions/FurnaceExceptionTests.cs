using System.Collections.Generic;
using Furnace.Models.Exceptions;
using NUnit.Framework;

namespace Furnace.Tests.Exceptions
{
    [TestFixture]
    public abstract class FurnaceExceptionTests
    {
        public abstract string BaseMessage { get; }
        public FurnaceException Sut;
        protected IEnumerable<string> Reasons;

        [SetUp]
        public void SetUp()
        {
            Reasons = new List<string>();
        }

        [Test]
        public void WhenFurnaceExceptionIsTrown_ThenLogMessage_IsNotEmpty()
        {
            try
            {
                throw Sut;
            }
            catch (FurnaceException ex)
            {
                Assert.That(ex.Message, Is.Not.Empty);
            }
        }

        [Test]
        public void WhenFurnaceExceptionIsTrown_WithOneReason_ThenLogMessage_IsCorrect()
        {
            const string reason = "Test Message One";
            var message = BaseMessage + reason;
            Reasons = new List<string> { reason };

            Assert.That(Sut.Message, Is.EqualTo(message));
        }

        [Test]
        public void WhenFurnaceExceptionIsTrown_WithTwoReasons_ThenLogMessage_IsCorrect()
        {
            const string reasonOne = "Test Message One";
            const string reasonTwo = "Test Message Two";

            var message = BaseMessage + reasonOne + ", " + reasonTwo;
            Reasons = new List<string> { reasonOne, reasonTwo };

            Assert.That(Sut.Message, Is.EqualTo(message));
        }

        protected IEnumerable<string> YieldReasons()
        {
            foreach (var reasion in Reasons)
            {
                yield return reasion;
            }
        }
    }
}
