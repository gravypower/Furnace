using Furnace.Models.Exceptions;
using NUnit.Framework;

namespace Furnace.Tests.Exceptions
{
    [TestFixture]
    public abstract class FurnaceExceptionTests
    {
        public abstract string ExcationName { get; }
        public FurnaceException Sut;

        [Test]
        public void WhenFurnaceExceptionIsTrown_ThenLogMessage_IsNotEmpty()
        {
            try
            {
                throw Sut;
            }
            catch (FurnaceException ex)
            {
                Assert.That(ex.ExceptionName, Is.EqualTo(ExcationName));
            }
        }
    }
}
