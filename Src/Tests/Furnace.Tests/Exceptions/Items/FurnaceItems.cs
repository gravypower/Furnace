using System.Collections.Generic;
using NUnit.Framework;

namespace Furnace.Tests.Exceptions.Items
{
    [TestFixture]
    public abstract class FurnaceItems : FurnaceExceptionTests
    {
        public class InvalidContentTypeException : FurnaceItems
        {
            public override string BaseMessage
            {
                get { return "FurnaceItems.InvalidContentTypeException thrown. InvalidReasons: "; }
            }

            [SetUp]
            public void InvalidContentTypeExceptionSetUp()
            {
                Sut = new Furnace.Items.FurnaceItems.InvalidContentTypeException(YieldReasons());
            }
        }
    }
}
