using System.Globalization;
using Furnace.Tests.Items.FurnaceItemsSpies;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.Localisation
{
    [TestFixture]
    public class LocalisationTests : WithNameAndNamespaceTests
    {
        public long Id = 1L;
        public CultureInfo CultureInfo;
        protected IFurnaceItemsSpy Spy;

        public LocalisationTests(string furnaceItemsType) : base(furnaceItemsType)
        {
        }

        [SetUp]
        public void LocalisationTestsSetUp()
        {
            Spy = Sut as IFurnaceItemsSpy;
            if (Spy == null)
            {
                Assert.Fail("Can't spy on Sut");
            }
        }

        [Test]
        public void WhenGetItemIsCalled_ThenCorrectKey_IsUsed()
        {
            //Assign
            CultureInfo = CultureInfo.GetCultureInfo("ja-JP");
            AddPropityToContentType("SomeName", "string");

            //Act
            Sut.GetItem(Id, ContentType, CultureInfo);

            //Assert
            Spy.AssertWhenGetItemIsCalled_ThenCorrectKey_IsUsed(this);
        }
    }
}
