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

        [Test]
        public void WhenGetItemIsCalled_ThenInvalidContentTypeException_IsThrown()
        {
            var key = "SomeKey";
            var ex = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.GetItem(key, ContentType));
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
        public class Properties : WithNo
        {
            [SetUp]
            public void PropertiesSetUp()
            {
                ContentType = new ContentType { Name = ContentTypeName , Namespace = ContentTypeNamespace};
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoProperties));
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
        public class NameOrNamespaceOrProperties : WithNo
        {
            [SetUp]
            public void NameOrNamespaceOrPropertiesSetUp()
            {
                ContentType = new ContentType();
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoName));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoNamespace));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoNamespace));
            }
        }
    }
}
