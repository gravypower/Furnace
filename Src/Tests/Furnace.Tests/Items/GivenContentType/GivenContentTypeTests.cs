using Furnace.Models.ContentTypes;

namespace Furnace.Tests.Items.GivenContentType
{
    public abstract class GivenContentTypeTests : FurnaceItemsTests
    {
        public ContentType ContentType;

        protected GivenContentTypeTests(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }
    }
}
