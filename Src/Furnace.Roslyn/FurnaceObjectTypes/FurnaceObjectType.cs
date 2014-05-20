using Furnace.Interfaces.Items;

namespace SomeNamespaceToBeReplaces
{
    public class FurnaceObjectType : IFurnaceObjectType<long>
    {
        public IFurnaceItemInformation<long> FurnaceItemInformation { get; set; }
    }
}