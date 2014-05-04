using Furnace.Items;
using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType
{
    using System.Linq;

    using Furnace.Models.Items;

    using Property = Furnace.Models.ContentTypes.Property;

    [TestFixture]
    public abstract class WithNo : GivenContentTypeTests
    {
        protected const string ContentTypeName = "SomeName";
        protected const string ContentTypeNamespace = "SomeNamespace";

        protected Property[] ContentTypeProperties = {new Property {Name = "SomeName", Type = "SomeType"} };

        [Test]
        public void WhenCreateItemIsCalled_ThenInvalidContentTypeException_IsThrown()
        {
            var ex = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.CreateItem(ContentType));
            AssertInvalidReasons(ex);
        }

        [Test]
        public void WhenGetItemIsCalled_ThenInvalidContentTypeException_IsThrown()
        {
            const long Id = 1L;
            var ex = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.GetItem(Id, ContentType));
            AssertInvalidReasons(ex);
        }

        [Test]
        public void WhenSetItemIsCalled_ThenInvalidContentTypeException_IsThrown()
        {
            const long Id = 1L;
            var item = new Item(ContentType);
            var ex = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.SetItem(Id, item));
            AssertInvalidReasons(ex);
        }

        protected abstract void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex);

        [TestFixture]
        public class Name : WithNo
        {
            [SetUp]
            public void WithNoNameSetUp()
            {
                ContentType = new ContentType { Namespace = ContentTypeNamespace, Properties = ContentTypeProperties };
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons.Count(), Is.EqualTo(1));
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
                Assert.That(ex.InvalidReasons.Count(), Is.EqualTo(1));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoProperties));
            }
        }

        [TestFixture]
        public class Namespace : WithNo
        {
            [SetUp]
            public void WithNoNameSetUp()
            {
                ContentType = new ContentType {Name = ContentTypeName, Properties = ContentTypeProperties };
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons.Count(), Is.EqualTo(1));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoNamespace));
            }
        }

        [TestFixture]
        public class NameOrProperties : WithNo
        {
            [SetUp]
            public void NameOrPropertiesSetUp()
            {
                ContentType = new ContentType { Namespace = ContentTypeNamespace };
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons.Count(), Is.EqualTo(2));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoName));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoProperties));
            }
        }

        [TestFixture]
        public class NameOrNamespace : WithNo
        {
            [SetUp]
            public void NameOrNamespaceSetUp()
            {
                ContentType = new ContentType { Properties = ContentTypeProperties };
            }

            protected override void AssertInvalidReasons(FurnaceItems.InvalidContentTypeException ex)
            {
                Assert.That(ex.InvalidReasons.Count(), Is.EqualTo(2));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoName));
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
                Assert.That(ex.InvalidReasons.Count(), Is.EqualTo(3));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoName));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoNamespace));
                Assert.That(ex.InvalidReasons, Contains.Item(FurnaceItems.InvalidContentTypeException.NoProperties));
            }
        }
    }
}
