using System.Globalization;
using Furnace.Items.Redis.Tests.Stubs;
using Furnace.Models.Items;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Items.Redis.Tests.Localisation
{
    [TestFixture]
    public abstract class LocalisationTests : RedisBackedFurnaceItemsTests
    {
        protected CultureInfo CultureInfo;
        protected Stub CultureStub;
        protected Stub DefultCultureStub;
        protected long Id;
        protected string Key;

        [SetUp]
        public void LocalisationTestsSetUp()
        {
            Id = 99L;
            Key = RedisBackedFurnaceItems.CreateItemKey(Id, typeof(Stub));
            var fi = new FurnaceItemInformation<long>();
            DefultCultureStub = new Stub
            {
                
                Test = "Hello"
            };
            Client.Hashes[Key][SiteConfiguration.DefaultSiteCulture.Name].Returns(DefultCultureStub.BuildSerialisedString());
        }

        public class GivenJapaneseAsTheCulture : LocalisationTests
        {
            [SetUp]
            public void GivenJapaneseAsTheCultureSetUp()
            {
                CultureInfo = CultureInfo.GetCultureInfo("fr-FR");
                var fi = new FurnaceItemInformation<long>();
                CultureStub = new Stub
                {
                    FurnaceItemInformation = fi,
                    Test = "こんにいちわ"
                };
            }
        }

        public class GivenFrenchAsTheCulture : LocalisationTests
        {
            [SetUp]
            public void GivenJapaneseAsTheCultureSetUp()
            {
                CultureInfo = CultureInfo.GetCultureInfo("ja-JP");
                var fi = new FurnaceItemInformation<long>();
                CultureStub = new Stub
                {
                    FurnaceItemInformation = fi,
                    Test = "bonjour"
                };
            }
        }

        [Test]
        public void WhenGetItemWithCultureInfoIsCalled_ThenTheReturnedItem_IsCorrect()
        {
            //Assign
            Client.Hashes[Key][CultureInfo.Name].Returns(CultureStub.BuildSerialisedString());

            //Act
            var result = Sut.GetItem<Stub>(Id, CultureInfo);

            //Assert
            Assert.That(result.Test, Is.EqualTo(CultureStub.Test));
        }

        [Test]
        public void WhenGetItemIsCalled_ThenTheReturnedItem_IsDefaultCulture()
        {
            //Act
            var result = Sut.GetItem<Stub>(Id);

            //Assert
            Assert.That(result.Test, Is.EqualTo(DefultCultureStub.Test));
        }
    }
}
