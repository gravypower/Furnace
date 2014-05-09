namespace Furnace.Play.Roslyn
{
    public interface IFurnaceItem
    {
        FurnaceItemInformation FurnaceItemInformation { get; }
        void SetFurnaceItem(FurnaceItemInformation itemInformation);
    }
}
