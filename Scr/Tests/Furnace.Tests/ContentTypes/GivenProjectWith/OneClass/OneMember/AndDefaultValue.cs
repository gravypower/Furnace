namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass.OneMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class AndDefaultValue : GivenOneMemberTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneMember\OneClass.WithOneMember.AndDefaultValue\OneClass.WithOneMember.AndDefaultValue.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get
            {
                return "WithOneMember.AndDefaultValue";
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
