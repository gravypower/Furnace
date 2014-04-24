using System.Linq;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass
{
    [TestFixture]
    public abstract class OneClassTests : GivenProjectWithTests
    {
        protected abstract string OneClassProjectPath { get; }

        protected override string ProjectPath
        {
            get { return @"OneClass\" + OneClassProjectPath; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenOneItemReturned()
        {
            //Act
            var result = Sut.GetContentTypes();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheOneItemReturnedHasCorrectName()
        {
            //Act
            var result = Sut.GetContentTypes();

            //Assert
            Assert.That(result.First().Name, Is.EqualTo("Test"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturnedHasCorrectNumberOfNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheOneItemReturnedHasCorrectNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace, Contains.Item("OneClass." + ExpectedNamespace));
        }
    }
}
