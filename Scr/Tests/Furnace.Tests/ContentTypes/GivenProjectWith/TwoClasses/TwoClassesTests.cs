using System.Linq;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.TwoClasses
{
    [TestFixture]
    public abstract class TwoClassesTests : GivenProjectWithTests
    {
        [Test]
        public void WhenGetContentTypesIsCalled_ThenTwoItemsReturned()
        {
            //Act
            var result = Sut.GetContentTypes();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturnedHasCorrectName()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var names = result.Select(x => x.Name);
            Assert.That(names, Contains.Item("Test1"));
            Assert.That(names, Contains.Item("Test2"));
        }


        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturnedHasCorrectNumberOfNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturnedHasCorrectNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace, Contains.Item("TwoClasses." + ExpectedNamespace));
            Assert.That(typeNamespace, Contains.Item("TwoClasses." + ExpectedNamespace));
        }
    }
}