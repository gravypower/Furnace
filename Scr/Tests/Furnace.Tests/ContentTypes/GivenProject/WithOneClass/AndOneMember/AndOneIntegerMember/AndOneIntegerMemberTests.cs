namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember.AndOneIntegerMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class AndOneIntegerMemberTests : AndOneMemberTests
    {
        protected override string ExpectedNamespace
        {
            get
            {
                return "AndOneIntegerMember";
            }
        }

        protected override string AndOneMemberProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneIntegerMember\WithOneClass.AndOneIntegerMember.csproj";
            }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyName()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test"), Contains.Item("IntegerProperty"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyType()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyTypes("Test", "IntegerProperty"), Is.EqualTo("int"));
        }
    }
}
