using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    [TestFixture]
    public abstract class WithOneClassTests : GivenProjectTests
    {
        protected abstract string OneClassProjectPath { get; }
        protected abstract Type Type { get; }

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
            var result = Sut.GetContentTypes();

            //Assert
            Assert.That(result.First().Name, Is.EqualTo("Test"));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheTwoItemsReturned_HasCorrectNumberOfNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheOneItemReturned_HasCorrectNamespace()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            var typeNamespace = result.Select(x => x.Namespace);
            Assert.That(typeNamespace, Contains.Item("WithOneClass." + ExpectedNamespace));
        }


        [Test]
        public void WhenAssemblyEmitted_HasCorrectTypes()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();
            Sut.CompileFurnaceContentTypes();

            //Assert
            foreach (var contentType in result)
            {
                var type =
                TypeFinder.FindType("Furnace.ContentTypes.Roslyn.FurnaceObjectTypes." +
                                    Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.FurnaceTypeIdentifier + contentType.Name);
                Assert.That(type, Is.Not.Null); 
            }
        }

        [Test]
        public void WhenAssemblyEmitted_TypeCanBeCastedToOriginalType()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();
            Sut.CompileFurnaceContentTypes();

            //Assert
            foreach (var contentType in result)
            {
                var type =
                TypeFinder.FindType("Furnace.ContentTypes.Roslyn.FurnaceObjectTypes." +
                                    Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.FurnaceTypeIdentifier + contentType.Name);
                Assert.That(type.BaseType.FullName, Is.EqualTo(Type.FullName));
            }
        }

        
    }
}
