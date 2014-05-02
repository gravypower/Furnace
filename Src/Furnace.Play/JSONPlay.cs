using NUnit.Framework;
using ServiceStack.Text;

namespace Furnace.Play
{
    public class SomeType
    {
        public string StringProp { get; set; }
    }

    public class SomeType1 : SomeType
    {
        public string StringProp1 { get; set; }
    }

    [TestFixture]
    public class JSONPlay
    {
        [Test]
        public void CanSerializeToJSONFromSomeType1_ThenCanDeserializeToSomeType()
        {
            var someType1 = new SomeType1 {StringProp = "StringProp", StringProp1 = "StringProp1"};

            var json = TypeSerializer.SerializeToString(someType1);

            var someType = TypeSerializer.DeserializeFromString<SomeType>(json);

            Assert.That(someType.StringProp, Is.EqualTo(someType1.StringProp));
        }

    }
}
