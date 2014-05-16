using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.GivenRealClasses.TestClasses;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using NSubstitute;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.GivenRealClasses
{
    [TestFixture]
    public class GivenRealClassesTests : FurnaceObjectTypeFactoryTests
    {
        private const string BaseNamespace = "Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes.GivenRealClasses.TestClasses.";

        protected ITypeFinder TypeFinder;

        [SetUp]
        public void GivenValidTemplatePathTestsSetUp()
        {
            TypeFinder = Substitute.For<ITypeFinder>();
            Sut = new FurnaceObjectTypeFactorySpy(TemplateFilePath, TypeFinder);
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
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");

            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            //Assert
            Assert.That(compilation.SyntaxTrees, Contains.Item(Spy.FurnaceTypes.First().SyntaxTree));

        }

        [Test]
        public void GivenTwoFurnaceType_WhenCompileIsCalled_ThenCompilationHasSyntaxTrees()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            Sut.AddFurnaceType(BaseNamespace + "ClassTwo");

            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());
            TypeFinder.FindType(BaseNamespace + "ClassTwo").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            //Assert
            foreach (var furnaceType in Spy.FurnaceTypes)
            {
                Assert.That(compilation.SyntaxTrees, Contains.Item(furnaceType.SyntaxTree));
            }
        }

        [Test]
        public void GivenoneFurnaceType_WhenCompileIsCalled_ThenCompilationHasReferences()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            //Assert
            Assert.That(compilation.References, Has.Some.Matches<MetadataReference>(x=>x.Display == typeof(IFurnaceObjectType).Assembly.Location));
            Assert.That(compilation.References, Has.Some.Matches<MetadataReference>(x => x.Display == typeof(ClassOne).Assembly.Location));
            Assert.That(compilation.References, Has.Some.Matches<MetadataReference>(x => x.Display == typeof(object).Assembly.Location));
        }

        [Test]
        public void GivenoneFurnaceType_WhenCompileIsCalled_ThenCompilationCanEmitWithNoDiagnosticsMessages()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            EmitResult emitResult;
            using (var memStream = new MemoryStream())
            {
                emitResult = compilation.Emit(memStream);
            }

            //Assert
            Assert.That(emitResult.Diagnostics, Is.Empty);
            Assert.That(emitResult.Success, Is.True);
        }

        [Test]
        public void GivenoneFurnaceType_WhenCompileIsCalled_ThenCompilationCanEmitAssembly()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            Assembly assembly = null;
            using (var memoryStream = new MemoryStream())
            {
                compilation.Emit(memoryStream);
                memoryStream.Flush();
                assembly = Assembly.Load(memoryStream.GetBuffer());
            }

            //Assert
            Assert.That(assembly, Is.Not.Null);
        }

        [Test]
        public void GivenoneFurnaceType_WhenAssemblyEmitted_HasCorrectType()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            Assembly assembly = null;
            using (var memoryStream = new MemoryStream())
            {
                compilation.Emit(memoryStream);
                memoryStream.Flush();
                assembly = Assembly.Load(memoryStream.GetBuffer());
            }

            //Assert
            var type = assembly.GetTypes().FirstOrDefault(t => t.FullName.Equals(BaseNamespace + Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.FurnaceTypeIdentifier + "ClassOne"));
            Assert.That(type, Is.Not.Null);
        }

        [Test]
        public void GivenoneFurnaceType_WhenAssemblyEmitted_TypeCanBeCastedToOriginalType()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            Assembly assembly = null;
            using (var memoryStream = new MemoryStream())
            {
                compilation.Emit(memoryStream);
                memoryStream.Flush();
                assembly = Assembly.Load(memoryStream.GetBuffer());
            }

            //Assert
            var type = assembly.GetTypes().FirstOrDefault(t => t.FullName.Equals(BaseNamespace + Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.FurnaceTypeIdentifier + "ClassOne"));
            Assert.That(type, Is.Not.Null);
            var instance = Activator.CreateInstance(type);

            Assert.That(instance, Is.AssignableTo<ClassOne>());
        }

        [Test]
        public void GivenoneFurnaceType_WhenAssemblyEmitted_TypeCanBeCastedToIFurnaceObjectType()
        {
            //Assign
            const string assemblyName = "TestAssemblyName";
            Sut.AddFurnaceType(BaseNamespace + "ClassOne");
            TypeFinder.FindType(BaseNamespace + "ClassOne").Returns(GetType());

            //Act
            var compilation = Sut.Compile(assemblyName);

            Assembly assembly = null;
            using (var memoryStream = new MemoryStream())
            {
                compilation.Emit(memoryStream);
                memoryStream.Flush();
                assembly = Assembly.Load(memoryStream.GetBuffer());
            }

            //Assert
            var type = assembly.GetTypes().FirstOrDefault(t => t.FullName.Equals(BaseNamespace + Roslyn.FurnaceObjectTypes.FurnaceTypeWriter.FurnaceTypeIdentifier + "ClassOne"));
            Assert.That(type, Is.Not.Null);
            var instance = Activator.CreateInstance(type);

            Assert.That(instance, Is.AssignableTo<IFurnaceObjectType>());
        }
    }
}
