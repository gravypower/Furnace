using System.Globalization;
using Furnace.Items;
using Furnace.Models.ContentTypes;
using Furnace.Models.Items;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.Items
{
    using Furnace.Items.Redis;

    using ServiceStack.Redis;

    [TestFixture("AbstractFurnaceItems")]
    [TestFixture("RedisBackedFurnaceItems")]
    public abstract class FurnaceItemsTests
    {
        protected IFurnaceItems<long> Sut;

        private readonly string _furnaceItemsType;

        protected FurnaceItemsTests(string furnaceItemsType)
        {
            _furnaceItemsType = furnaceItemsType;
        }

        [SetUp]
        protected void SetUp()
        {
            switch (_furnaceItemsType)
            {
                case "AbstractFurnaceItems":
                    Sut = new FurnaceItemsSpy();
                    break;
                case "RedisBackedFurnaceItems":
                    {
                        var client = Substitute.For<IRedisClient>();
                        Sut = new RedisBackedFurnaceItemsSpy(client);
                    }
                    break;
            }
        }
    }
}
