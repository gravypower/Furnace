using Furnace.Models.Items;
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

        public Stub(FurnaceItemInformation<long> furnaceItemInformation)
        {
            FurnaceItemInformation = furnaceItemInformation;
        }

        public FurnaceItemInformation<long> FurnaceItemInformation { get; set; }
    }
}
