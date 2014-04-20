
using Furnace.Attributes;

namespace Furnace.Tests.Models
{
    [FurnaceTypeId("3e696e60-d7b6-48b0-a187-cc6d4de532ef")]
    public class RedisTest2
    {
        public string StringProperty2 { get; set; }

        public override string ToString()
        {
            return StringProperty2;
        }
    }
}
