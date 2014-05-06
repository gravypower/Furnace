using System.Linq;
using Furnace.Roslyn.Tests.ContentTypes;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithNoClasses
{
    [TestFixture]
    public class GivenProjectWithNoClasses : ContentTypesTests
    {
        protected override string ProjectPath
        {
            get { return @"WithNoClasses\WithNoClasses.csproj"; }
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
