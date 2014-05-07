using NUnit.Framework;
using ServiceStack.Text;

namespace Furnace.Play
{
    using System;
    using System.Dynamic;

    using Furnace.Models.ContentTypes;

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
            var someType1 = new SomeType1 { StringProp = "StringProp", StringProp1 = "StringProp1" };

            var json = TypeSerializer.SerializeToString(someType1);

            var someType = TypeSerializer.DeserializeFromString<SomeType>(json);

            Assert.That(someType.StringProp, Is.EqualTo(someType1.StringProp));
        }

        [Test]
        public void CanSerializeToJSONFromItem_ThenCanDeserializeToSomeType()
        {
            var contentType = new ContentType
            {
                Name = "SomeName",
                Properties = new[] { new Property { Name = "StringProp", Type = "string", DefaultValue = "Hello" } },
                Namespace = "SomeNamesapce"
            };

            var item = new Item(contentType);

            item.AddPropity("StringProp", "StringPropTest");

            var json = JsonSerializer.SerializeToString(item);

            Console.WriteLine(json);

            var t = JsonSerializer.DeserializeFromString<SomeType>(json);

            //Assert.That(t.StringProp, Is.EqualTo("StringPropTest"));
            

            var tt = JsonSerializer.DeserializeFromString<ExpandoObject>(json);
            var ttt = new Item(tt);
        }

        [Test]
        public void WhenTypedGetItemIsCalled_ThenTheReturnedItem_IsCorrect()
        {
            //Assign
            const string propityValue = "NotDefaultValue";
            const string returnJon = "{\"Test\":\"" + propityValue + "\"}";

            //Act
            var result = JsonSerializer.DeserializeFromString<Stub>(returnJon);

            //Assert
            Assert.That(result.Test, Is.EqualTo(propityValue));
        }

        public class Stub
        {
            public string Test { get; set; }
        }
    }
}
