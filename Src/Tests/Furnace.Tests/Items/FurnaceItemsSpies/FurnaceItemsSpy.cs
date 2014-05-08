using System.Globalization;
using Furnace.Configuration;
using Furnace.Items;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;
using Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.Localisation;
using NUnit.Framework;

namespace Furnace.Tests.Items.FurnaceItemsSpies
{
    public class FurnaceItemsSpy : FurnaceItems<long>, IFurnaceItemsSpy
    {
        public class AbstractGetItemInfo
        {
            public long Id { get; set; }
            public ContentType ContentType { get; set; }
            public CultureInfo Ci { get; set; }
        }

        public FurnaceItemsSpy(IFurnaceSiteConfiguration siteConfiguration) : base(siteConfiguration)
        {
        }

        public AbstractGetItemInfo AbstractGetItemLastCall { get; private set; }

        public override Item AbstractGetItem(long id, ContentType contentType, CultureInfo ci)
        {
            AbstractGetItemLastCall = new AbstractGetItemInfo
            {
                Id = id,
                ContentType = contentType,
                Ci = ci
            };

            return null;
        }

        public override TRealType GetItem<TRealType>(long id)
        {
            return default(TRealType);
        }

        public override TRealType GetItem<TRealType>(long id, CultureInfo cultureInfo)
        {
            return default(TRealType);
        }

        public override void AbstractSetItem(long id, Item item)
        {
        }

        public void AssertWhenGetItemIsCalled_ThenCorrectKey_IsUsed(LocalisationTests fixture)
        {
            Assert.That(AbstractGetItemLastCall.Ci, Is.EqualTo(fixture.CultureInfo));
        }
    }
}
