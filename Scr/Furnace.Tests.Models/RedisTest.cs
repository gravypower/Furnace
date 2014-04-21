namespace Furnace.Tests.Models
{
    public class RedisTest
    {
        public int t { get; set; }
        public string StringProperty { get; set; }

        public RedisTest2 RedisTest2Property { get; set; }

        public override string ToString()
        {
            return StringProperty;
        }
    }
}
