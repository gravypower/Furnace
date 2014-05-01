using System.Linq;
using Furnace.Items;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.AndOneProperty
{
    [TestFixture]
    public abstract class AndOnePropertyTest : WithNameAndNamespaceTests
    {
        protected abstract string PropertyName { get; }
        protected abstract string PropertyType { get; }
        protected abstract object DefaultValue { get; }

        [Test]
        public void WithNoType_ThenInvalidPropertyException_IsThrown()
        {
            //Assign
            AddPropityToContentType(PropertyName);

            //Act
            var exception = Assert.Throws<FurnaceItems.InvalidContentTypeException>(()=> Sut.CreateItem(ContentType));

            //Assert
            var invalidReason = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoType, PropertyName);
            Assert.That(exception.InvalidReasons.First(), Is.EqualTo(invalidReason));
        }

        [Test]
        public void WithNoName_ThenInvalidPropertyException_IsThrown()
        {
            //Assign
            AddPropityToContentType(type: PropertyType);

            //Act
            var exception = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.CreateItem(ContentType));

            //Assert
            var invalidReason = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoName, PropertyType);
            Assert.That(exception.InvalidReasons.First(), Is.EqualTo(invalidReason));
        }

        [Test]
        public void WithNoDefaultValue_ThenValue_IsNull()
        {
            //Assign
            AddPropityToContentType(PropertyName, PropertyType);

            //Act
            var item = Sut.CreateItem(ContentType);

            //Assert
            Assert.That(item[PropertyName], Is.Null);
        }

        [Test]
        public void WithDefaultValue_ThenValue_IsDefault()
        {
            //Assign
            AddPropityToContentType(PropertyName, PropertyType, DefaultValue);

            //Act
            var item = Sut.CreateItem(ContentType);

            //Assert
            Assert.That(item[PropertyName], Is.EqualTo(DefaultValue));
        }
    }
}
