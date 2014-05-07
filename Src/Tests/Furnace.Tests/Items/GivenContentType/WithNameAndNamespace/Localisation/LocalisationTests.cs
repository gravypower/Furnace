using System.Globalization;
using Furnace.Items.Redis;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.Localisation
{
    [TestFixture]
    public class LocalisationTests : WithNameAndNamespaceTests
    {
        public LocalisationTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        [Test]
        public void WhenGetItemIsCalled_ThenCorrectKey_IsUsed()
        {
            //Assign
            const long id = 1L;
            var cultureInfo = CultureInfo.GetCultureInfo("ja-JP");
            AddPropityToContentType("SomeName", "string");

            //Act
            Sut.GetItem(id, ContentType, cultureInfo);

            //Assert
            if (Sut is FurnaceItemsSpy)
            {
                var spy = Sut as FurnaceItemsSpy;
                Assert.That(spy.AbstractGetItemLastCall.Ci, Is.EqualTo(cultureInfo));
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
