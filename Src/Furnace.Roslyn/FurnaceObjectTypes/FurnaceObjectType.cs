using Furnace.Items;
using Furnace.Models.Items;

namespace SomeNamespaceToBeReplaces
{
    public class FurnaceObjectType : IFurnaceObjectType<long>
    {
        public FurnaceItemInformation<long> FurnaceItemInformation { get; set; }
    }
}