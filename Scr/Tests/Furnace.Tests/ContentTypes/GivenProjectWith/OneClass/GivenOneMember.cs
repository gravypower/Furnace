using System.Linq;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass
{
    [TestFixture]
    public class GivenOneMember : Tests
    {      
        protected override string ProjectPath
        {
            get { return @"OneClass.WithOneMember\OneClass.WithOneMember.csproj"; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasOneProperty()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.First().Properties.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasCorrectPropertyName()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.First().Properties.First(), Is.EqualTo("StringProperty"));
        }
    }
}
