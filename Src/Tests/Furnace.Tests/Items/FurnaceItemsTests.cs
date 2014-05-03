using Furnace.Items;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.Items
{
    [TestFixture]
    public abstract class FurnaceItemsTests
    {
        protected IFurnaceItems<string> Sut;

        [SetUp]
        protected void SetUp()
        {
            Sut = Substitute.For<FurnaceItems<string>>();
        }
    }
}
