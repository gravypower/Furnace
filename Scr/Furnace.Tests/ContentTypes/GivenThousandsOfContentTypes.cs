using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using ServiceStack;

namespace Furnace.Tests.ContentTypes
{
    [TestFixture]
    public class GivenThousandsOfContentTypes : Tests
    {
        protected const string TypeName = "Test.Type.{0}";
        protected const int NumberOfTypes = 5000;

        [SetUp]
        public void GivenThousandsOfContentTypesSetUp()
        {
            var typeList = new List<string>();

            for (var i = 0; i < NumberOfTypes; i++)
            {
                typeList.Add(TypeName.FormatWith(i));
            }

            TypeFinder.FindTypes().Returns(typeList);
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenThousandsFurnaceContentTypeReturned()
        {
            //Act
            var result = Sut.GetContentTypes();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(NumberOfTypes));
        }
    }
}
