namespace Furnace.Tests.ContentTypes.GivenProject.WithTwoClasses
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public abstract class WithTwoClassesTests : GivenProjectTests
    {
        [Test]
        public void WhenGetContentTypesIsCalled_ThenTwoItemsReturned()
        {
            //Act
            var result = this.Sut.GetContentTypes();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturned_HasCorrectName()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            var names = result.Select(x => x.Name);
            Assert.That(names, Contains.Item("Test1"));
            Assert.That(names, Contains.Item("Test2"));
        }


        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturned_HasCorrectNumberOfNamespace()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturned_HasCorrectNamespace()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace, Contains.Item("WithTwoClasses." + this.ExpectedNamespace));
            Assert.That(typeNamespace, Contains.Item("WithTwoClasses." + this.ExpectedNamespace));
        }
    }
}