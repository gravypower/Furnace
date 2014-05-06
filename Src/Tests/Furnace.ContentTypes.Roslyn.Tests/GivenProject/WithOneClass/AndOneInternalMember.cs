using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    [TestFixture]
    public class AndOneInternalMember : WithOneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneInternalMember\WithOneClass.AndOneInternalMember.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOneInternalMember"; }
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
