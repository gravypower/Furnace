using System.Globalization;
using Furnace.Items.Redis;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.Redis;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.Localisation
{
    [TestFixture]
    public class LocalisationTests : WithNameAndNamespaceTests
    {
        public LocalisationTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        [Test]
        public void SomeTest()
        {
            //Assign
            const long id = 1L;
            var ci = CultureInfo.GetCultureInfo("ja-JP");
            AddPropityToContentType("SomeName", "string");

            //Act
            Sut.GetItem(id, ContentType, ci);

            //Assert
            if (Sut is FurnaceItemsSpy)
            {
                var spy = Sut as FurnaceItemsSpy;
                Assert.That(spy.AbstractGetItemLastCall.Ci, Is.EqualTo(ci));
            }

            if (Sut is RedisBackedFurnaceItems)
            {
                var key = RedisBackedFurnaceItems.CreateItemKey(id, ContentType);
                var spy = Sut as RedisBackedFurnaceItemsSpy;
                if (spy != null)
                    spy.Client.Received().GetValue(key);
            }

        }
    }
}
