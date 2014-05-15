using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass.AndOneMember
{
    [TestFixture]
    public class DateTimeMember : AndOneMemberTests
    {
        protected override Type Type
        {
            get { return typeof(global::WithOneClass.AndOneDateTimeMember.Test); }
        }

        [SetUp]
        public void DateTimeMemberSetUp()
        {
            var loadType = new global::WithOneClass.AndOneDateTimeMember.Test();
        }

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

            protected override Type Type
            {
                get { return typeof(global::WithOneClass.AndOneDateTimeMember.AndDefaultValue.Test); }
            }

            [SetUp]
            public void AndDefaultValueSetUp()
            {
                var loadType = new global::WithOneClass.AndOneDateTimeMember.AndDefaultValue.Test();
            }

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
                Assert.That(result.GetPropertyDefaultValue("Test", this.ExpectedPropertyName), Is.EqualTo("new DateTime(2014, 4, 25)"));
            }
        }
    }
}
