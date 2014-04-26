namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithOneClass
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public abstract class WithOneClassTests : GivenProjectTests
    {
        protected abstract string OneClassProjectPath { get; }

        protected override string ProjectPath
        {
            get { return @"WithOneClass\" + this.OneClassProjectPath; }
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
        public void WhenGetContentTypesIsCalled_ThenTheOneItemReturned_HasCorrectName()
        {
            //Act
            var result = this.Sut.GetContentTypes();

            //Assert
            Assert.That(result.First().Name, Is.EqualTo("Test"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturned_HasCorrectNumberOfNamespace()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheOneItemReturned_HasCorrectNamespace()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace, Contains.Item("WithOneClass." + this.ExpectedNamespace));
        }
    }
}
