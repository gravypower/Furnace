using Furnace.Items;
using NUnit.Framework;

namespace Furnace.Tests.Items
{
    [TestFixture]
    public abstract class FurnaceItemsTests
    {
        protected IFurnaceItems Sut;

        [SetUp]
        protected void SetUp()
        {
            Sut = new FurnaceItems();
        }

        //[Test]
        //public void WhenCreateItemIsCalled_ThenTheItemsReturned_HasCorrectName()
        //{
        //    //const string contentTypeName = "SomeContentTypeName";
        //    //var contentType = new FurnaceContentType {ExceptionName = contentTypeName};

        //    //const string propertyName = "SomePropertyName";
        //    //var property = new FurnaceContentTypeProperty();
        //    //property.ExceptionName = propertyName;
        //}
    }
}
