using System.Linq;
using NUnit.Framework;

namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithOneClass
{
    [TestFixture]
    public class AndOneProtectedMember : WithOneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneProtectedMember\WithOneClass.AndOneProtectedMember.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOneProtectedMember"; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasNoProperties()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test").Count, Is.EqualTo(0));
        }
    }
}
