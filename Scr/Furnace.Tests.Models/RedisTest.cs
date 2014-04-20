using Furnace.Attributes;

namespace Furnace.Tests.Models
{
    [FurnaceTypeId("7b75ea1e-c0e6-40d8-b98b-92c72078603a")]
    public class RedisTest
    {
        public string StringProperty { get; set; }

        public override string ToString()
        {
            return StringProperty;
        }
    }
}
