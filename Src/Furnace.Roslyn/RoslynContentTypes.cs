using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Furnace.ContentTypes.Roslyn.Extensions;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Furnace.ContentTypes.Roslyn.Models;
using Furnace.Interfaces.ContentTypes;
using Furnace.Models.ContentTypes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace Furnace.ContentTypes.Roslyn
{
    public class RoslynContentTypes : IFurnaceContentTypes
    {
        protected const string PrivateModifier = "private";
        protected const string ProtectedModifier = "protected";
        protected const string InternalModifier = "internal";

        private readonly Project _project;

        protected readonly IFurnaceObjectTypeFactory ObjectTypeFactory;
        public const string AssemblyName = "FurnaceObjectTypes";

        public RoslynContentTypes(string projectPath, string templateFilePath)
        {
            if (string.IsNullOrEmpty(projectPath))
                throw new InvalidProjectPathException();

            var workspace = MSBuildWorkspace.Create();
            _project = workspace.OpenProjectAsync(projectPath).Result;

            ObjectTypeFactory = new FurnaceObjectTypeFactory(templateFilePath);
        }

        public IEnumerable<IContentType> GetContentTypes()
        {
            var contentTypes = new List<IContentType>();
            var classes = new List<ClassType>();
            foreach (var document in _project.Documents)
            {
                var documentRoot = document
                    .GetSyntaxRootAsync()
                    .Result;

                if (!documentRoot.ClassDeclarationNodes().Any())
                    continue;

                var model = document.GetSemanticModelAsync().Result;

                foreach (var classDeclarationSyntax in documentRoot.ClassDeclarationNodes())
                {
                    classes.Add(new ClassType
                    {
                        DocumentRoot = documentRoot,
                        ClassDeclarationSyntax = classDeclarationSyntax,
                        SemanticModel = model
                    });
                }
            }

            contentTypes.AddRange(
                    classes.Where(x => x.ClassDeclarationSyntax.BaseList == null)
                    .Select(BuildContentType));

            foreach (var classType in classes.Where(x => x.ClassDeclarationSyntax.BaseList != null))
            {
                var contentType = BuildContentType(classType);

                if (classType.ClassDeclarationSyntax.BaseList != null)
                {
                    foreach (var typeSyntax in classType.ClassDeclarationSyntax.BaseList.Types)
                    {
                        var baseType = typeSyntax.GetText().ToString().Trim();
                        foreach (var baseProperty in contentTypes.Single(x => x.Name == baseType).Properties)
                        {
                            contentType.Properties.Add(baseProperty);
                        }
                    }
                }

                contentTypes.Add(contentType);
            }

            return contentTypes;
        }

        public void CompileFurnaceContentTypes()
        {
            var compilation = ObjectTypeFactory.Compile(AssemblyName);
            using (var memoryStream = new MemoryStream())
            {
                compilation.Emit(memoryStream);
                memoryStream.Flush();
                Assembly.Load(memoryStream.GetBuffer());
            }
        }

        private ContentType BuildContentType(ClassType classType)
        {
            var symbol = classType.SemanticModel.GetDeclaredSymbol(classType.ClassDeclarationSyntax);

            var contentType = new ContentType
            {
                Name = symbol.ToMinimallyQualified(),
                Namespace = symbol.GetNamespace()
            };

            foreach (var property in classType.DocumentRoot.PropertyDeclarationNodes())
            {
                if (IsNotPublicModifier(property))
                    continue;

                contentType.Properties.Add(property.GetFurnaceContentTypeProperty());
            }

            ObjectTypeFactory.AddFurnaceType(contentType.FullName);

            return contentType;
        }

        private static bool IsNotPublicModifier(BasePropertyDeclarationSyntax property)
        {
            return  IsNotPublicModifier(property, PrivateModifier) || 
                    IsNotPublicModifier(property, ProtectedModifier) ||
                    IsNotPublicModifier(property, InternalModifier);
        }

        private static bool IsNotPublicModifier(BasePropertyDeclarationSyntax property, string modifier)
        {
            return property.Modifiers.Any(x => string.Compare(x.Text, modifier, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        public class InvalidProjectPathException : Exception
        {
        }
    }
}
