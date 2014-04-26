namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithTwoClasses.AndOneMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class AndDefaultValue : WithTwoClassesTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"WithTwoClasses\AndOneMember\WithTwoClasses.AndOneMember.AndDefaultValue\WithTwoClasses.AndOneMember.AndDefaultValue.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOneMember.AndDefaultValue"; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyDefaultValue()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyDefaultValue("Test1", "StringProperty1"), Is.EqualTo("StringProperty1Default"));
            Assert.That(result.GetPropertyDefaultValue("Test2", "StringProperty2"), Is.EqualTo("StringProperty2Default"));
        }
    }
}
