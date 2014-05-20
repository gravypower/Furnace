using Furnace.Interfaces.Items;
using ServiceStack.Text;

namespace Furnace.Items.Redis.Tests
{
    public class Stub : IFurnaceObjectType<long>
    {
        public string Test { get; set; }

        public string BuildSerialisedString()
        {
            return TypeSerializer.SerializeToString(this);
        }

        public Stub(IFurnaceItemInformation<long> furnaceItemInformation)
        {
            FurnaceItemInformation = furnaceItemInformation;
        }

        public IFurnaceItemInformation<long> FurnaceItemInformation { get; set; }
    }
}
