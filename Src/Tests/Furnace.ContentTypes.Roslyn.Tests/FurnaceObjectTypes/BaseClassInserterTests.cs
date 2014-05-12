using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Microsoft.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    [TestFixture]
    public class BaseClassInserterTests
    {
        public BaseClassInserter Sut;
        public string BaseClass = "SomeClass";
        public SyntaxNode Node;

        [SetUp]
        public void SetUp()
        {
            Node = Substitute.For<SyntaxNode>();
            Sut = new BaseClassInserter(BaseClass);
        }

        [Test]
        public void GivenNullBaseClass_WhenBaseClassInserterCreated_BaseClassExceptionThrown()
        {
            var ex = Assert.Throws<BaseClassInserter.BaseClassException>(() => new BaseClassInserter(null));

            Assert.That(ex.InvalidReasons, Contains.Item(BaseClassInserter.BaseClassException.NullBaseClass));
        }

        [Test]
        public void GivenEmptyBaseClass_WhenBaseClassInserterCreated_TempltePathExceptionThrown()
        {
            var ex = Assert.Throws<BaseClassInserter.BaseClassException>(() => new BaseClassInserter(string.Empty));

            Assert.That(ex.InvalidReasons, Contains.Item(BaseClassInserter.BaseClassException.EmptyBaseClass));
        }

        [Test]
        public void SomeTest()
        {
            Sut.Visit(Node);
        }
    }
}
