namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass.OneMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class GivenOneMemberTests : OneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneMember\OneClass.WithOneMember\OneClass.WithOneMember.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "WithOneMember"; }
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
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasCorrectPropertyName()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test"), Contains.Item("StringProperty"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasCorrectPropertyType()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyTypes("Test", "StringProperty"), Is.EqualTo("string"));
        }
    }
}
