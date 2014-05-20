namespace Furnace.Interfaces.ContentTypes
{
    public interface IProperty
    {
        string Name { get; set; }
        string Type { get; set; }
        object DefaultValue { get; set; }
    }
}
