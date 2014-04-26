using System.Linq;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember
{
    [TestFixture]
    public class DateTimeMember : AndOneMemberTests
    {
        protected override string ExpectedNamespace
        {
            get
            {
                return "AndOneDateTimeMember";
            }
        }

        protected override string AndOneMemberProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneDateTimeMember\WithOneClass.AndOneDateTimeMember.csproj";
            }
        }

        protected override string ExpectedPropertyName
        {
            get
            {
                return "DateTimeProperty";
            }
        }

        protected override string ExpectedPropertyType
        {
            get
            {
                return "DateTime";
            }
        }

        [TestFixture]
        public class AndDefaultValue : DateTimeMember
        {
            protected override string OneClassProjectPath
            {
                get
                {
                    return @"AndOneMember\WithOneClass.AndOneDateTimeMember.AndDefaultValue\WithOneClass.AndOneDateTimeMember.AndDefaultValue.csproj";
                }
            }

            protected override string ExpectedNamespace
            {
                get
                {
                    return "AndOneDateTimeMember.AndDefaultValue";
                }
            }

            [Test]
            public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyDefaultValue()
            {
                //Act
                var result = this.Sut.GetContentTypes().ToList();

                //Assert
                Assert.That(result.GetPropertyDefaultValue("Test", ExpectedPropertyName), Is.EqualTo("new DateTime(2014, 4, 25)"));
            }
        }
    }
}
