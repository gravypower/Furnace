using Furnace.Interfaces.ContentTypes;

namespace Furnace.Models.ContentTypes
{
    public class Property : IProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public object DefaultValue { get; set; }
    }
}
