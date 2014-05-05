using Furnace.Models.ContentTypes;

namespace Furnace.Tests.Items.GivenContentType
{
    public abstract class GivenContentTypeTests : FurnaceItemsTests
    {
        protected ContentType ContentType;

        protected GivenContentTypeTests(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }

        protected void AddPropityToContentType(string propertyName = null, string type = null, object defaltValue = null)
        {
            var property = new Property
            {
                Name = propertyName,
                Type = type,
                DefaultValue = defaltValue
            };

            ContentType.Properties.Add(property);
        }
    }
}
