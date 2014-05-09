namespace Furnace.Play.Roslyn
{
    public class FurnaceItem : IFurnaceItem
    {
        public FurnaceItemInformation FurnaceItemInformation { get; private set; }

        public void SetFurnaceItem(FurnaceItemInformation itemInformation)
        {
            FurnaceItemInformation = itemInformation;
        }
    }
}
