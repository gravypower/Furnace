using System.Globalization;
using Furnace.Items;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;

namespace Furnace.Tests.Items
{
    public class FurnaceItemsSpy : FurnaceItems<long>
    {
        public class AbstractGetItemInfo
        {
            public long Id { get; set; }
            public ContentType ContentType { get; set; }
            public CultureInfo Ci { get; set; }
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

        public override void AbstractSetItem(long id, Item item)
        {
        }
    }
}
