namespace Furnace.Models.Items
{
    public class FurnaceItemInformation<TKeyType>
    {
        public TKeyType Id { get; set; }
        public string ContentTypeFullName { get; set; }
        public string ContentTypeVersion { get; set; }
    }
}
