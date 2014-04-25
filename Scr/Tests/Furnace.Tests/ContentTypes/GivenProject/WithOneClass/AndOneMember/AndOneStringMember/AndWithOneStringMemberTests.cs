namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember.AndOneStringMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class AndWithOneStringMemberTests : AndOneMemberTests
    {
        protected override string AndOneMemberProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneStringMember\WithOneClass.AndOneStringMember.csproj";
            }
        }


        protected override string ExpectedNamespace
        {
            get { return "AndOneStringMember"; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyName()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test"), Contains.Item("StringProperty"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyType()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyTypes("Test", "StringProperty"), Is.EqualTo("string"));
        }
    }
}
