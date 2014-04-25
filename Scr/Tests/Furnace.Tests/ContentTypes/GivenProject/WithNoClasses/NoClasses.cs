namespace Furnace.Tests.ContentTypes.GivenProject.WithNoClasses
{
    using System.Linq;

    using NUnit.Framework;

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
            var result = this.Sut.GetContentTypes();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
