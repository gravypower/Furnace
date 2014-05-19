using System.Linq;
using Furnace.Items;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace
{
    [TestFixture]
    public class AndTwoProperties : WithNameAndNamespaceTests
    {
        public AndTwoProperties(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }

        [Test]
        public void WhenCreateItemIsCalled_WithNoType_ThenInvalidPropertyException_IsThrown()
        {
            //Assign
            const string propertyNameOne = "SomeNameOne";
            ContentType.AddPropity(propertyNameOne);

            const string propertyNameTwo = "SomeNameTwo";
            ContentType.AddPropity(propertyNameTwo);

            //Act
            var exception = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.CreateItem(ContentType));

            //Assert
            var reasons = exception.InvalidReasons.ToList();

            var invalidReasonOne = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoType, propertyNameOne);
            Assert.That(reasons[0], Is.EqualTo(invalidReasonOne));

            var invalidReasonTwo = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoType, propertyNameTwo);
            Assert.That(reasons[1], Is.EqualTo(invalidReasonTwo));
        }

        [Test]
        public void WhenCreateItemIsCalled_WithNoName_ThenInvalidPropertyException_IsThrown()
        {
            //Assign
            const string propertyTypeOne = "string";
            ContentType.AddPropity(type: propertyTypeOne);

            const string propertyTypeTwo = "string";
            ContentType.AddPropity(type: propertyTypeTwo);

            //Act
            var exception = Assert.Throws<FurnaceItems.InvalidContentTypeException>(() => Sut.CreateItem(ContentType));

            //Assert
            var reasons = exception.InvalidReasons.ToList();

            var invalidReasonOne = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoName, propertyTypeOne);
            Assert.That(reasons[0], Is.EqualTo(invalidReasonOne));

            var invalidReasonTwo = string.Format(FurnaceItems.InvalidContentTypeException.PropertyHasNoName, propertyTypeTwo);
            Assert.That(reasons[1], Is.EqualTo(invalidReasonTwo));
        }

    }
}
