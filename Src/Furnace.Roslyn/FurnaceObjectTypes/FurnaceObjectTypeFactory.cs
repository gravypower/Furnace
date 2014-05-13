using System.Collections.Generic;
using System.IO;
using Furnace.Models.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Furnace.ContentTypes.Roslyn.FurnaceObjectTypes
{
    public class FurnaceObjectTypeFactory
    {
        protected string TemplateFilePath;
        protected SyntaxNode TemplateClassRoot;

        private readonly List<SyntaxNode> _furnaceTypes;

        protected IEnumerable<SyntaxNode> FurnaceTypes
        {
            get { return _furnaceTypes; }
        }

        public FurnaceObjectTypeFactory(string templateFilePath)
        {
            GuardTemplatePath(templateFilePath);
            TemplateFilePath = templateFilePath;
            
            TemplateClassRoot = CSharpSyntaxTree.ParseFile(TemplateFilePath).GetRoot();
            _furnaceTypes = new List<SyntaxNode>();
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
            _furnaceTypes.Add(syntaxRewriter.Visit(TemplateClassRoot));
        }

        private static void GuardFullName(string fullName)
        {
            if (fullName == null)
                throw new FullNameException(new[] { FullNameException.NullFullName });

            if (fullName == string.Empty)
                throw new FullNameException(new[] { FullNameException.EmptyFullName });
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
