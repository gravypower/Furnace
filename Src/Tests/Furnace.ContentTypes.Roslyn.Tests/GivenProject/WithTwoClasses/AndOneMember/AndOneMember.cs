using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithTwoClasses.AndOneMember
{
    [TestFixture]
    public class AndOneMember : WithTwoClassesTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"WithTwoClasses\AndOneMember\WithTwoClasses.AndOneMember\WithTwoClasses.AndOneMember.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOneMember"; }
        }

        protected override Type[] Types
        {
            get
            {
                return new[]
                {
                    typeof (global::WithTwoClasses.AndOneMember.Test1),
                    typeof (global::WithTwoClasses.AndOneMember.Test2)
                };
            }
        }

        [SetUp]
        public void AndOneMemberSetUp()
        {
            var loadType1 = new global::WithTwoClasses.AndOneMember.Test1();
            var loadType2 = new global::WithTwoClasses.AndOneMember.Test2();
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasOneProperty()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test1").Count, Is.EqualTo(1));
            Assert.That(result.GetPropertyNames("Test2").Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyNames()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test1"), Contains.Item("StringProperty1"));
            Assert.That(result.GetPropertyNames("Test2"), Contains.Item("StringProperty2"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyType()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyType("Test1", "StringProperty1"), Is.EqualTo("string"));
            Assert.That(result.GetPropertyType("Test2", "StringProperty2"), Is.EqualTo("string"));
        }

        
        //[Test]
        //public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasCorrectPropertyDefaultValue()
        //{
        //    //Act
        //    var result = Sut.GetContentTypes().ToList();

        //    //Assert
        //    Assert.That(result.GetPropertyDefaultValue("Test1", "StringProperty1"), Is.EqualTo("StringProperty1Default"));
        //    Assert.That(result.GetPropertyDefaultValue("Test2", "StringProperty2"), Is.EqualTo("StringProperty2Default"));
        //}
    }
}
