using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass.AndOneMember
{
    [TestFixture]
    public abstract class AndOneMemberTests : WithOneClassTests
    {
        protected abstract string AndOneMemberProjectPath { get; }
        protected abstract string ExpectedPropertyName { get; }
        protected abstract string ExpectedPropertyType { get; }

        protected override string OneClassProjectPath
        {
            get
            {
                return @"AndOneMember\" + AndOneMemberProjectPath;
            }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasOneProperty()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test").Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyName()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test"), Contains.Item(ExpectedPropertyName));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyType()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyType("Test", ExpectedPropertyName), Is.EqualTo(ExpectedPropertyType));
        }
    }
}
