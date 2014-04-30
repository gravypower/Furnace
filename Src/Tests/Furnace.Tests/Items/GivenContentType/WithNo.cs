using Furnace.Items;
using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType
{
    [TestFixture]
    public abstract class WithNo : GivenContentTypeTests
    {
        protected const string ContentTypeName = "SomeName";
        protected const string ContentTypeNamespace = "SomeNamespace";

        [Test]
        public void WhenCreateItemIsCalled_ThenInvalidContentTypeException_IsThrown()
        {
            var ex = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.CreateItem(ContentType));
            AssertInvalidReasons(ex);
        }

        protected abstract void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex);

        [TestFixture]
        public class Name : WithNo
        {
            [SetUp]
            public void WithNoNameSetUp()
            {
                ContentType = new ContentType { Namespace = ContentTypeNamespace };
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoName));
            }
        }

        [TestFixture]
        public class Namespace : WithNo
        {
            [SetUp]
            public void WithNoNameSetUp()
            {
                ContentType = new ContentType {Name = ContentTypeName};
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoNamespace));
            }
        }

        [TestFixture]
        public class NameOrNamesapce : WithNo
        {
            [SetUp]
            public void NameOrNamesapceSetUp()
            {
                ContentType = new ContentType();
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoName));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoNamespace));
            }
        }
    }
}
