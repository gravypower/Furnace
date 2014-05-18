namespace Furnace.Items.Redis.Tests
{
    public class Stub
    {
        public string Test { get; set; }

        public string BuildSerialisedString()
        {
            return "{Test:" + Test + "}";
        }
    }
}
