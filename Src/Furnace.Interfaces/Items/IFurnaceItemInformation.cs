namespace Furnace.Interfaces.Items
{
    public interface IFurnaceItemInformation<TKeyType>
    {
        TKeyType Id { get; set; }
        string ContentTypeFullName { get; set; }
        string ContentTypeVersion { get; set; }
    }

}
