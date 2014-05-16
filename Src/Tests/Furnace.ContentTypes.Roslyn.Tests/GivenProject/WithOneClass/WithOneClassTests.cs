using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    [TestFixture]
    public abstract class WithOneClassTests : GivenProjectTests
    {
        protected abstract string OneClassProjectPath { get; }
        

        protected override string ProjectPath
        {
            get { return @"WithOneClass\" + OneClassProjectPath; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenOneItemReturned()
        {
            //Act
            var result = this.Sut.GetContentTypes();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenOneItemReturnedHasCorrectName()
        {
            //Act
            var result = Sut.GetContentTypes();

            //Assert
            Assert.That(result.First().Name, Is.EqualTo("Test"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTwoItemsReturnedHaveCorrectNumberOfNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenOneItemReturnedHasCorrectNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace, Contains.Item("WithOneClass." + ExpectedNamespace));
        }
    }
}
