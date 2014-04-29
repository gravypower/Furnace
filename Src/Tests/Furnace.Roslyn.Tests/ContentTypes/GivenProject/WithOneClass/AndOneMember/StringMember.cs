namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class StringMember : AndOneMemberTests
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
            get
            {
                return "AndOneStringMember";
            }
        }

        protected override string ExpectedPropertyName
        {
            get
            {
                return "StringProperty";
            }
        }

        protected override string ExpectedPropertyType
        {
            get
            {
                return "string";
            }
        }

        [TestFixture]
        public class AndDefaultValue : StringMember
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
                Assert.That(result.GetPropertyDefaultValue("Test", this.ExpectedPropertyName), Is.EqualTo("StringPropertyDefault"));
            }
        }
    }
}
