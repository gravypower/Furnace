using System.Linq;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.GivenRealClasses.TestClasses;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.GivenRealClasses
{
    [TestFixture]
    public class GivenRealClassesTests : FurnaceObjectTypeFactoryTests
    {
        private const string BaseNamespace = "Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.GivenRealClasses.TestClasses";
        [SetUp]
        public void GivenValidTemplatePathTestsSetUp()
        {
            Sut = new FurnaceObjectTypeFactorySpy(TemplateFilePath);
        }

        [Test]
        public void GivenOnceFurnaceType_WhenCompileIsCalled_ThenCompilationCorrectName()
        {
            //Act
            const string assemblyName = "TestAssemblyName";
            var compilation = Sut.Compile(assemblyName);

            //Assert
            Assert.That(compilation.AssemblyName, Is.EqualTo(assemblyName));
        }

        [Test]
        public void GivenOnceFurnaceType_WhenCompileIsCalled_ThenCompilationCorrectOutputKindIsDynamicallyLinkedLibrary()
        {
            //Act
            const string assemblyName = "TestAssemblyName";
            var compilation = Sut.Compile(assemblyName);

            //Assert
            Assert.That(compilation.Options.OutputKind, Is.EqualTo(OutputKind.DynamicallyLinkedLibrary));
        }

        [Test]
        public void GivenOneFurnaceType_WhenCompileIsCalled_ThenCompilationHasSyntaxTree()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "OneClassOne");

            //Act
            var compilation = Sut.Compile(assemblyName);

            //Assert
            Assert.That(compilation.SyntaxTrees, Contains.Item(Spy.FurnaceTypes.First()));

        }

        [Test]
        public void GivenTwoFurnaceType_WhenCompileIsCalled_ThenCompilationHasSyntaxTrees()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            Sut.AddFurnaceType(BaseNamespace + "ClassTwo");

            //Act
            var compilation = Sut.Compile(assemblyName);

            //Assert
            foreach (var furnaceType in Spy.FurnaceTypes)
            {
                Assert.That(compilation.SyntaxTrees, Contains.Item(furnaceType));
            }
        }

        [Test]
        public void GivenoneFurnaceType_WhenCompileIsCalled_ThenCompilationHasReferences()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");

            //Act
            var compilation = Sut.Compile(assemblyName);

            //Assert
            Assert.That(compilation.References, Has.Some.Matches<MetadataReference>(x=>x.Display == typeof(IFurnaceObjectType).Assembly.Location));
            Assert.That(compilation.References, Has.Some.Matches<MetadataReference>(x => x.Display == typeof(ClassOne).Assembly.Location));
        }
    }
}
