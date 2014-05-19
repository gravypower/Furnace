using System.Collections.Generic;
using System.IO;
using Furnace.ContentTypes.Roslyn.Models;
using Furnace.Items;
using Furnace.Models.Exceptions;
using Furnace.Models.Items;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Furnace.ContentTypes.Roslyn.FurnaceObjectTypes
{
    public interface IFurnaceObjectTypeFactory
    {
        IEnumerable<FurnaceType> FurnaceTypes { get; }
        void AddFurnaceType(string fullName);
        CSharpCompilation Compile(string assemblyName);
    }

    public class FurnaceObjectTypeFactory : IFurnaceObjectTypeFactory
    {
        protected readonly string TemplateFilePath;
        protected readonly SyntaxNode TemplateClassRoot;

        private readonly List<FurnaceType> _furnaceTypes;

        public IEnumerable<FurnaceType> FurnaceTypes
        {
            get { return _furnaceTypes; }
        }

        private readonly ITypeFinder _typeFinder;

        public FurnaceObjectTypeFactory(string templateFilePath, ITypeFinder typeFinder)
        {
            GuardTemplatePath(templateFilePath);
            TemplateFilePath = templateFilePath;

            TemplateClassRoot = CSharpSyntaxTree.ParseFile(TemplateFilePath).GetRoot();
            _furnaceTypes = new List<FurnaceType>();

            _typeFinder = typeFinder;
        }

        public FurnaceObjectTypeFactory(string templateFilePath)
            :this(templateFilePath, new TypeFinder())
        {            
        }

        private static void GuardTemplatePath(string templatePath)
        {
            if (templatePath == null)
                throw new TemplatePathException(new[] {TemplatePathException.NullTemplatePath});

            if (templatePath == string.Empty)
                throw new TemplatePathException(new[] {TemplatePathException.EmptyTemplatePath});

            if (!File.Exists(templatePath))
                throw new TemplatePathException(new[] { TemplatePathException.InvalidTemplatePath });
        }

        public void AddFurnaceType(string fullName)
        {
            GuardFullName(fullName);

            var syntaxRewriter = new FurnaceTypeWriter(fullName);
            var syntaxTree = SyntaxFactory.SyntaxTree(syntaxRewriter.Visit(TemplateClassRoot));
            _furnaceTypes.Add(new FurnaceType {FullName = fullName, SyntaxTree = syntaxTree});
        }

        private static void GuardFullName(string fullName)
        {
            if (fullName == null)
                throw new FullNameException(new[] { FullNameException.NullFullName });

            if (fullName == string.Empty)
                throw new FullNameException(new[] { FullNameException.EmptyFullName });
        }

        public CSharpCompilation Compile(string assemblyName)
        {
            var compilation = CSharpCompilation.Create(assemblyName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(new MetadataFileReference(typeof (IFurnaceObjectType<long>).Assembly.Location))
                .AddReferences(new MetadataFileReference(typeof(FurnaceItemInformation<long>).Assembly.Location))
                .AddReferences(new MetadataFileReference(typeof (object).Assembly.Location));

            foreach (var furnaceType in FurnaceTypes)
            {
                var typeLocation = _typeFinder.FindType(furnaceType.FullName).Assembly.Location;
                compilation = compilation.AddSyntaxTrees(furnaceType.SyntaxTree)
                    .AddReferences(new MetadataFileReference(typeLocation));
            }

            return compilation;
        }

        public class TemplatePathException : ReasonsFurnaceException
        {
            public const string NullTemplatePath = "Templte path was null";

            public const string EmptyTemplatePath = "Templte path was Empty";

            public const string InvalidTemplatePath = "Templte path was Empty";

            public TemplatePathException(IEnumerable<string> reasons) : base(reasons)
            {
            }
        }

        public class FullNameException : ReasonsFurnaceException
        {
            public const string NullFullName = "Full name path was null";

            public const string EmptyFullName = "Full name path was Empty";

            public FullNameException(IEnumerable<string> reasons) : base(reasons)
            {
            }
        }
    }
}
