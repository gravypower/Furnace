using System;
using System.Linq;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Furnace.Items;
using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject
{
    [TestFixture]
    public abstract class GivenProjectTests : ContentTypesTests
    {
        protected abstract string ExpectedNamespace { get; }

        protected ITypeFinder TypeFinder = new TypeFinder();

        protected abstract Type[] Types { get; }

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
                TypeFinder.FindType(BuildTypeFullName(contentType));
                Assert.That(type, Is.Not.Null);
            }
        }

        [Test]
        public void WhenAssemblyEmitted_ThenTypeCanBeCastedToOriginalType()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();
            Sut.CompileFurnaceContentTypes();

            //Assert
            foreach (var contentType in result)
            {
                var type = TypeFinder.FindType(BuildTypeFullName(contentType));
                var instance = Activator.CreateInstance(type);

                Assert.That(instance, Is.AssignableTo(Types.Single(x=>x.Name == contentType.Name)));
            }
        }

        [Test]
        public void WhenAssemblyEmitted_ThenTypeCanBeCastedToIFurnaceObjectType()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();
            Sut.CompileFurnaceContentTypes();

            //Assert
            foreach (var contentType in result)
            {
                var type = TypeFinder.FindType(BuildTypeFullName(contentType));
                var instance = Activator.CreateInstance(type);

                Assert.That(instance, Is.AssignableTo<IFurnaceObjectType<long>>());
            }
        }

        private static string BuildTypeFullName(ContentType contentType)
        {
            return contentType.Namespace + "." + FurnaceTypeWriter.FurnaceTypeIdentifier + contentType.Name;
        }
    }
}
