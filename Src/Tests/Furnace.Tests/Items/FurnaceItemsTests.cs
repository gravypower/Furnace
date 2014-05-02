using Furnace.Items;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.Items
{
    [TestFixture]
    public abstract class FurnaceItemsTests
    {
        protected IFurnaceItems Sut;
        protected IFurnaceItemRepository<string> ItemRepository;

        [SetUp]
        protected void SetUp()
        {
            ItemRepository = Substitute.For<IFurnaceItemRepository<string>>();
            Sut = new JsonBackedFurnaceItems(ItemRepository);
        }
    }
}
