using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass.AndOneMember
{
    [TestFixture]
    public class IntegerMember : AndOneMemberTests
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

        protected override string ExpectedPropertyName
        {
            get
            {
                return "IntegerProperty";
            }
        }

        protected override string ExpectedPropertyType
        {
            get
            {
                return "int";
            }
        }

        protected override Type[] Types
        {
            get { return new [] { typeof (global::WithOneClass.AndOneIntegerMember.Test)}; }
        }

        [SetUp]
        public void AndOneIntegerMemberSetUp()
        {
            var loadType = new global::WithOneClass.AndOneIntegerMember.Test();
        }

        [TestFixture]
        public class AndDefaultValue : IntegerMember
        {
            protected override Type[] Types
            {
                get { return new [] {typeof(global::WithOneClass.AndOneIntegerMember.AndDefaultValue.Test)}; }
            }

            [SetUp]
            public void AndDefaultValueSetUp()
            {
                var loadType = new global::WithOneClass.AndOneIntegerMember.AndDefaultValue.Test();
            }

            protected override string OneClassProjectPath
            {
                get
                {
                    return @"AndOneMember\WithOneClass.AndOneIntegerMember.AndDefaultValue\WithOneClass.AndOneIntegerMember.AndDefaultValue.csproj";
                }
            }

            protected override string ExpectedNamespace
            {
                get
                {
                    return "AndOneIntegerMember.AndDefaultValue";
                }
            }

            [Test]
            public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyDefaultValue()
            {
                //Act
                var result = this.Sut.GetContentTypes().ToList();

                //Assert
                Assert.That(result.GetPropertyDefaultValue("Test", this.ExpectedPropertyName), Is.EqualTo("1"));
            }
        }
    }
}
