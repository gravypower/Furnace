namespace Furnace.Interfaces.Items
{
    public interface IFurnaceItemInformation<TKeyType>
    {
        TKeyType Id { get; set; }
        string ContentTypeFullName { get; set; }

        TKeyType ParentId { get; set; }
        string ParentContentTypeFullName { get; set; }
    }
}
