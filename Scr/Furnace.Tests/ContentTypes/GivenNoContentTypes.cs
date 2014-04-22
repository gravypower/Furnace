using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes
{
    public class GivenNoContentTypes : Tests
    {
        [SetUp]
        public void GivenNoContentTypesSetUp()
        {
            TypeFinder.FindTypes().Returns(new List<string>());
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenNoFurnaceContentTypeReturned()
        {
            //Act
            var contentTypes = Sut.GetContentTypes();

            //Assert
            Assert.That(contentTypes, Is.Empty);
        }
    }
}
