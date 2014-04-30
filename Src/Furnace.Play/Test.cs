using System.Dynamic;
using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.Play
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void AddAnItem()
        {
            const string propityName = "SomeName";
            var propity = new Property {Name = propityName};

            var contentType = new ContentType();
            contentType.Properties.Add(propity);

            var item = new Item(contentType);
            const string propityValue = "SomeValue";
            item.AddPropity(propityName, propityValue);
            Assert.That(((dynamic)item).SomeName, Is.EqualTo(propityValue));
        }

        [Test]
        public void CantAddAnItem()
        {
            const string propityName = "SomeName";
            var contentType = new ContentType();
            var item = new Item(contentType);
            const string propityValue = "SomeValue";

            Assert.That(item.AddPropity(propityName, propityValue), Is.False);
        }
    }
}
