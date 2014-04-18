namespace Furnace.Tests.Models
{
    public class RedisTest
    {
        public string StringProperty { get; set; }

        public override string ToString()
        {
            return StringProperty;
        }
    }
}
