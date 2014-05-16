using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithTwoClasses.AndOneMember
{
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

        protected override Type []Types
        {
            get
            {
                return new[]
                {
                    typeof (global::WithTwoClasses.AndOneMember.AndDefaultValue.Test1),
                    typeof (global::WithTwoClasses.AndOneMember.AndDefaultValue.Test2)
                };
            }
        }

        [SetUp]
        public void AndDefaultValueSetUp()
        {
            var loadType1 = new global::WithTwoClasses.AndOneMember.AndDefaultValue.Test1();
            var loadType2 = new global::WithTwoClasses.AndOneMember.AndDefaultValue.Test2();
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
