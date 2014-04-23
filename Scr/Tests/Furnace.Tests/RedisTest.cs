using NUnit.Framework;
using ServiceStack.Redis;

namespace Furnace.Tests
{
    [TestFixture]
    public class RedisTest
    {
        [Test]
        public void SomeTest()
        {
            using (var redisClient = new RedisClient("localhost"))
            {
                redisClient.Add("FOO", "BAR");
            }
        }
    }
}
