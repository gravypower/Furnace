using Furnace.Interfaces.Items;
using ServiceStack.Text;

namespace Furnace.Items.Redis.Tests.Stubs
{
    public abstract class BaseStub : IFurnaceObjectType<long>
    {
        public IFurnaceItemInformation<long> FurnaceItemInformation { get; set; }
        public string Test { get; set; }
        public string BuildSerialisedString()
        {
            return TypeSerializer.SerializeToString(this);
        }
    }
}
