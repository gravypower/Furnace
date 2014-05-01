using System;
using System.Linq;
using Furnace.Items;
using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace
{
    [TestFixture]
    public class AndOneProperty : WithNameAndNamespaceTests
    {
        [Test]
        public void WithNoType_ThenInvalidPropertyException_IsThrown()
        {
            //Assign
            const string propertyName = "SomeName";
            AddPropityToContentType(propertyName);

            //Act
            var exception = Assert.Throws<FurnaceItems.InvalidContentTypeException>(()=> Sut.CreateItem(ContentType));

            //Assert
            var invalidReason = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoType, propertyName);
            Assert.That(exception.InvalidReasons.First(), Is.EqualTo(invalidReason));
        }

        [Test]
        public void WithNoName_ThenInvalidPropertyException_IsThrown()
        {
            //Assign
            const string propertyType = "string";
            AddPropityToContentType(type: propertyType);

            //Act
            var exception = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.CreateItem(ContentType));

            //Assert
            var invalidReason = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoName, propertyType);
            Assert.That(exception.InvalidReasons.First(), Is.EqualTo(invalidReason));
        }
    }
}
