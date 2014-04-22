using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes
{
    [TestFixture]
    public class GivenOneContentType : Tests
    {
        protected const string TypeName = "Test.Type";

        [SetUp]
        public void GivenOneContentTypeSetUp()
        {
            TypeFinder.FindTypes().Returns(new List<string> { TypeName });
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenOneFurnaceContentTypeReturned()
        {
            //Act
            var contentTypes = Sut.GetContentTypes();

            //Assert
            var furnaceContentTypes = contentTypes.ToArray();

            Assert.That(furnaceContentTypes.Count(), Is.EqualTo(1));
            Assert.That(furnaceContentTypes.First().Name, Is.EqualTo(TypeName));
        }
    }
}
