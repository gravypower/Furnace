namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass.OneMember
{
    using System.Linq;

    using Furnace.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember;

    using NUnit.Framework;

    [TestFixture]
    public class AndWithDefaultValue : AndWithOneMemberTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneMember\OneClass.WithOneMember.AndWithDefaultValue\OneClass.WithOneMember.AndWithDefaultValue.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get
            {
                return "WithOneMember.AndWithDefaultValue";
            }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasCorrectPropertyDefaultValue()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyDefaultValue("Test", "StringProperty"), Is.EqualTo("StringPropertyDefault"));
        }
    }
}
