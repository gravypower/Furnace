using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithInheritance
{
    [TestFixture]
    public class AndTwoClasses : GivenProjectTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"WithInheritance\WithInheritance.TwoClasses\WithInheritance.AndTwoClasses.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "WithInheritance.AndTwoClasses"; }
        }

        protected override Type[] Types
        {
            get
            {
                return new[]
                {
                    typeof (global::WithInheritance.AndTwoClasses.Test1),
                    typeof (global::WithInheritance.AndTwoClasses.Test2)
                };
            }
        }

        [SetUp]
        public void AndDefaultValueSetUp()
        {
            var loadType1 = new global::WithInheritance.AndTwoClasses.Test1();
            var loadType2 = new global::WithInheritance.AndTwoClasses.Test2();
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasTwoProperty()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test1").Count, Is.EqualTo(1));
            Assert.That(result.GetPropertyNames("Test2").Count, Is.EqualTo(2));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyNames()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test1"), Contains.Item("StringProperty1"));

            Assert.That(result.GetPropertyNames("Test2"), Contains.Item("StringProperty1"));
            Assert.That(result.GetPropertyNames("Test2"), Contains.Item("StringProperty2"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturned_HasCorrectPropertyTypes()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyType("Test1", "StringProperty1"), Is.EqualTo("string"));

            Assert.That(result.GetPropertyType("Test2", "StringProperty1"), Is.EqualTo("string"));
            Assert.That(result.GetPropertyType("Test2", "StringProperty2"), Is.EqualTo("string"));
        }
    }
}
