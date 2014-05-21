using System;
using System.Collections.Generic;
using System.Globalization;
using Furnace.Interfaces.Configuration;
using Furnace.Interfaces.ContentTypes;
using Furnace.Interfaces.Items;
using Furnace.Items;
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
            public IContentType ContentType { get; set; }
            public CultureInfo Ci { get; set; }
        }

        public FurnaceItemsSpy(IFurnaceSiteConfiguration siteConfiguration) : base(siteConfiguration)
        {
        }

        public AbstractGetItemInfo AbstractGetItemLastCall { get; private set; }

        public override IItem<long> AbstractGetItem(long id, IContentType contentType, CultureInfo ci)
        {
            AbstractGetItemLastCall = new AbstractGetItemInfo
            {
                Id = id,
                ContentType = contentType,
                Ci = ci
            };

            return null;
        }

        protected override IItem<long> NewItem(IContentType contentType)
        {
            return new Item(contentType);
        }

        public override TRealType GetItem<TRealType>(long id)
        {
            return default(TRealType);
        }

        public override TRealType GetItem<TRealType>(long id, CultureInfo cultureInfo)
        {
            return default(TRealType);
        }

        public override void AbstractSetItem(long id, IItem<long> item)
        {
        }

        public override IEnumerable<IItem<long>> GetItemChildren<TRealType>(long id)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<IItem<long>> GetItemChildren(long id, Type type)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IItem<long>> GetItemSiblings<T>(long id)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<IItem<long>> GetItemSiblings(long id, Type type)
        {
            throw new NotImplementedException();
        }

        public override IItem<long> GetItemParent<T>(long id)
        {
            throw new NotImplementedException();
        }

        public override IItem<long> GetItemParent(long id, Type type)
        {
            throw new NotImplementedException();
        }

        public void AssertWhenGetItemIsCalled_ThenCorrectKey_IsUsed(LocalisationTests fixture)
        {
            Assert.That(AbstractGetItemLastCall.Ci, Is.EqualTo(fixture.CultureInfo));
        }
    }
}
