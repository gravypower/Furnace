using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using NUnit.Framework;

namespace Furnace.Tests.Exceptions.FurnaceObjectTypes
{
    [TestFixture]
    public abstract class FurnaceObjectType : FurnaceExceptionTests
    {
        public class TempltePathException : FurnaceObjectType
        {
            public override string BaseMessage
            {
                get { return "FurnaceObjectTypeFactory.TempltePathException thrown. InvalidReasons: "; }
            }

            [SetUp]
            public void InvalidContentTypeExceptionSetUp()
            {
                Sut = new FurnaceObjectTypeFactory.TempltePathException(YieldReasons());
            }
        }
    }
}
