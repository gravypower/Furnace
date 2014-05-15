using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass.AndOneMember
{
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

        protected override Type Type
        {
            get { return typeof (global::WithOneClass.AndOneStringMember.Test); }
        }

        [SetUp]
        public void AndOneStringMemberSetUp()
        {
            var loadType = new global::WithOneClass.AndOneStringMember.Test();
        }

        [TestFixture]
        public class AndDefaultValue : StringMember
        {

            [SetUp]
            public void AndDefaultValueSetUp()
            {
                var loadType = new global::WithOneClass.AndOneStringMember.AndDefaultValue.Test();
            }

            protected override Type Type
            {
                get { return typeof(global::WithOneClass.AndOneStringMember.AndDefaultValue.Test); }
            }

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
