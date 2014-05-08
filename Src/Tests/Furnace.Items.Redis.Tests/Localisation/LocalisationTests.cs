using System.Globalization;
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

        protected LocalisationTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        [SetUp]
        public void LocalisationTestssetUp()
        {
            Id = 99L;
            Key = RedisBackedFurnaceItems.CreateItemKey(Id, typeof(Stub));
            DefultCultureStub = new Stub {Test = "Hello"};
            Client.Hashes[Key][SiteConfiguration.DefaultSiteCulture.Name].Returns(DefultCultureStub.BuildSerialisedString());
        }

        public class GivenJapaneseAsTheCulture : LocalisationTests
        {
            public GivenJapaneseAsTheCulture(string furnaceItemsType) : base(furnaceItemsType)
            {
            }

            [SetUp]
            public void GivenJapaneseAsTheCultureSetUp()
            {
                CultureInfo = CultureInfo.GetCultureInfo("fr-FR");
                CultureStub = new Stub { Test = "こんにいちわ" };
            }
        }

        public class GivenFrenchAsTheCulture : LocalisationTests
        {
            public GivenFrenchAsTheCulture(string furnaceItemsType) : base(furnaceItemsType)
            {
            }

            [SetUp]
            public void GivenJapaneseAsTheCultureSetUp()
            {
                CultureInfo = CultureInfo.GetCultureInfo("ja-JP");
                CultureStub = new Stub { Test = "bonjour" };
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
