using Furnace.Models.Items;

namespace Furnace.Items
{
    public interface IFurnaceObjectType<TKeyType>
    {
        FurnaceItemInformation<TKeyType> FurnaceItemInformation { get; set; }
    }
}
