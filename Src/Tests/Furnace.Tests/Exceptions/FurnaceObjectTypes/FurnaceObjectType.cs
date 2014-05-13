using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using NUnit.Framework;

namespace Furnace.Tests.Exceptions.FurnaceObjectTypes
{
    [TestFixture]
    public abstract class FurnaceObjectType : FurnaceExceptionTests
    {
        public class TemplatePathException : FurnaceObjectType
        {
            public override string BaseMessage
            {
                get { return "FurnaceObjectTypeFactory.TemplatePathException thrown. InvalidReasons: "; }
            }

            [SetUp]
            public void InvalidContentTypeExceptionSetUp()
            {
                Sut = new FurnaceObjectTypeFactory.TemplatePathException(YieldReasons());
            }
        }
    }
}
