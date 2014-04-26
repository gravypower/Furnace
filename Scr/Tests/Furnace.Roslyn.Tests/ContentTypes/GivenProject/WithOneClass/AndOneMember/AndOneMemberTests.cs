namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember
{
    using System.Linq;

    using NUnit.Framework;

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
                return @"AndOneMember\" + this.AndOneMemberProjectPath;
            }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasOneProperty()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test").Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyName()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test"), Contains.Item(this.ExpectedPropertyName));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyType()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyType("Test", this.ExpectedPropertyName), Is.EqualTo(this.ExpectedPropertyType));
        }
    }
}
