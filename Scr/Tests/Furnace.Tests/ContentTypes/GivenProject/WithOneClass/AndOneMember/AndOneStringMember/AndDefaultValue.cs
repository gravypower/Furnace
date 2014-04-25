namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember.AndOneStringMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class AndDefaultValue : AndWithOneStringMemberTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"AndOneMember\WithOneClass.AndOneStringMember.AndDefaultValue\WithOneClass.AndOneStringMember.AndDefaultValue.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get
            {
                return "AndOneStringMember.AndDefaultValue";
            }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyDefaultValue()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyDefaultValue("Test", "StringProperty"), Is.EqualTo("StringPropertyDefault"));
        }
    }
}
