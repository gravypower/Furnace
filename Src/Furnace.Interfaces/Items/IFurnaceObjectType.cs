namespace Furnace.Interfaces.Items
{
    public interface IFurnaceObjectType<TKeyType>
    {
        IFurnaceItemInformation<TKeyType> FurnaceItemInformation { get; set; }
    }
}
