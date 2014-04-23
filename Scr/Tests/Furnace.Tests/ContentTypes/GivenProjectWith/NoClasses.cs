using System.Linq;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith
{
    [TestFixture]
    public class GivenProjectWithNoClasses : ContentTypesTests
    {
        protected override string ProjectPath
        {
            get { return @"NoClasses\NoClasses.csproj"; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenOneItemReturned()
        {
            //Act
            var result = Sut.GetContentTypes();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
