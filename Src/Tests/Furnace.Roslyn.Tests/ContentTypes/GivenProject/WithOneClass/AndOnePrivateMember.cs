using System.Linq;
using NUnit.Framework;

namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithOneClass
{
    [TestFixture]
    public class AndOnePrivateMember : WithOneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndOnePrivateMember\WithOneClass.AndOnePrivateMember.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOnePrivateMember"; }
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
