using System.Globalization;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Items.Redis.Tests.Localisation
{
    [TestFixture]
    public abstract class LocalisationTests : RedisBackedFurnaceItemsTests
    {
        protected CultureInfo CultureInfo;

        protected LocalisationTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        public class GivenJapaneseAsTheCulture : LocalisationTests
        {
            public GivenJapaneseAsTheCulture(string furnaceItemsType) : base(furnaceItemsType)
            {
                //Assign
                var culture = "ja-JP";
                CultureInfo = CultureInfo.GetCultureInfo(culture);
            }

            [Test]
            public void WhenGetItemIsCalledThenTheReturnedItem_IsCorrect()
            {
                const long id = 1L;

                const string propityValue = "こんにいちわ";
                var returnJon = new Stub { Test = propityValue }.BuildSerialisedString();

                var key = RedisBackedFurnaceItems.CreateItemKey(id, typeof(Stub));

                Client.Hashes[key][CultureInfo.Name].Returns(returnJon);

                //Act
                var result = Sut.GetItem<Stub>(id, CultureInfo);

                //Assert
                Assert.That(result.Test, Is.EqualTo(propityValue));
            }
        }
    }
}
