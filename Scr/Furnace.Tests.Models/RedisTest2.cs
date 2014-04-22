
namespace Furnace.Tests.Models
{
    public class RedisTest2
    {
        public string StringProperty2 { get; set; } = "123";

        public override string ToString()
        {
            return StringProperty2;
        }
    }
}
