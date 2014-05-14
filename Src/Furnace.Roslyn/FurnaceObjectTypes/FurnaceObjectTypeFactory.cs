﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Furnace.Models.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Furnace.ContentTypes.Roslyn.FurnaceObjectTypes
{
    public class FurnaceObjectTypeFactory
    {
        protected readonly string TemplateFilePath;
        protected readonly SyntaxNode TemplateClassRoot;

        private readonly List<SyntaxTree> _furnaceTypes;

        protected IEnumerable<SyntaxTree> FurnaceTypes
        {
            get { return _furnaceTypes; }
        }

        public FurnaceObjectTypeFactory(string templateFilePath)
        {
            GuardTemplatePath(templateFilePath);
            TemplateFilePath = templateFilePath;
            
            TemplateClassRoot = CSharpSyntaxTree.ParseFile(TemplateFilePath).GetRoot();
            _furnaceTypes = new List<SyntaxTree>();
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
            _furnaceTypes.Add(syntaxTree);
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
                .AddReferences(new MetadataFileReference(typeof(IFurnaceObjectType).Assembly.Location));

            foreach (var furnaceType in FurnaceTypes)
            {
                //var typeLocation = FindType()
                compilation = compilation.AddSyntaxTrees(furnaceType)
                    .AddReferences();
            }

            return compilation;
        }

        private static Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
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
