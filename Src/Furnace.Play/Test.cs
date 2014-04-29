﻿using Furnace.ContentTypes.Model;
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
            var propity = new FurnaceContentTypeProperty {Name = propityName};

            var contentType = new FurnaceContentType();
            contentType.Properties.Add(propity);

            dynamic item = new Item(contentType);
            const string propityValue = "SomeValue";
            item.AddPropity(propityName, propityValue);
            Assert.That(item.SomeName, Is.EqualTo(propityValue));
        }

        [Test]
        public void CantAddAnItem()
        {
            const string propityName = "SomeName";
            var contentType = new FurnaceContentType();
            dynamic item = new Item(contentType);
            const string propityValue = "SomeValue";
            
            Assert.That(item.AddPropity(propityName, propityValue), Is.False);
        }
    }
}