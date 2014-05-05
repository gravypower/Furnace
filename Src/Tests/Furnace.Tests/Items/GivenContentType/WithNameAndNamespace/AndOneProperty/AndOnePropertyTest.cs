using System.Linq;
using Furnace.Items;

using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.AndOneProperty
{
    [TestFixture]
    public abstract class AndOnePropertyTest : WithNameAndNamespaceTests
    {
        protected AndOnePropertyTest(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }

        protected abstract string PropertyName { get; }
        protected abstract string PropertyType { get; }
        protected abstract object DefaultValue { get; }

        #region CreateItem
        [Test]
        public void AndNoType_WhenCreateItemIsCalled_ThenInvalidPropertyException_IsThrown()
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
        public void AndNoName_WhenCreateItemIsCalled_ThenInvalidPropertyException_IsThrown()
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
        public void AndNoDefaultValue_WhenCreateItemIsCalled_ThenValue_IsNull()
        {
            //Assign
            AddPropityToContentType(PropertyName, PropertyType);

            //Act
            var item = Sut.CreateItem(ContentType);

            //Assert
            Assert.That(item[PropertyName], Is.Null);
        }

        [Test]
        public void AndHasDefaultValue_WhenCreateItemIsCalled_ThenValue_IsDefault()
        {
            //Assign
            AddPropityToContentType(PropertyName, PropertyType, DefaultValue);

            //Act
            var item = Sut.CreateItem(ContentType);

            //Assert
            Assert.That(item[PropertyName], Is.EqualTo(DefaultValue));
        }
        #endregion

        #region GetItem

        [Test]
        public void NoItemWithId_WhenGetItemIsCalled_ThenNullReturned()
        {
            //Assign
            const long Id = 1L;
            AddPropityToContentType(PropertyName, PropertyType);

            //Act
            var result = Sut.GetItem(Id, ContentType);

            Assert.That(result, Is.Null);
        }
        #endregion
    }
}
