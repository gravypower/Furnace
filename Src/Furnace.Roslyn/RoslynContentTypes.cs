using System;
using System.Collections.Generic;
using System.Linq;
using Furnace.ContentTypes.Roslyn.Extensions;
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

        public RoslynContentTypes(string projectPath)
        {
            if (string.IsNullOrEmpty(projectPath))
                throw new InvalidProjectPathException();

            var workspace = MSBuildWorkspace.Create();
            _project = workspace.OpenProjectAsync(projectPath).Result;
        }

        public IEnumerable<ContentType> GetContentTypes()
        {
            var contentTypes = new List<ContentType>();
            var classes = new List<ClassType>();
            foreach (var document in _project.Documents)
            {
                var documentRoot = document
                    .GetSyntaxRootAsync()
                    .Result;

                if (!documentRoot.GetClassDeclarationSyntax().Any())
                    continue;

                var model = document.GetSemanticModelAsync().Result;

                foreach (var classDeclarationSyntax in documentRoot.GetClassDeclarationSyntax())
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

        private static ContentType BuildContentType(ClassType classType)
        {
            var symbol = classType.SemanticModel.GetDeclaredSymbol(classType.ClassDeclarationSyntax);

            var contentType = new ContentType
            {
                Name = symbol.ToMinimallyQualified(),
                Namespace = symbol.GetNamespace()
            };

            foreach (var property in classType.DocumentRoot.GetPropertyDeclarationSyntax())
            {
                if (CheckModifiers(property))
                    continue;

                contentType.Properties.Add(property.GetFurnaceContentTypeProperty());
            }

            return contentType;
        }

        private static bool CheckModifiers(BasePropertyDeclarationSyntax property)
        {
            return  CheckModifiers(property, PrivateModifier) || 
                    CheckModifiers(property, ProtectedModifier) ||
                    CheckModifiers(property, InternalModifier);
        }

        private static bool CheckModifiers(BasePropertyDeclarationSyntax property, string modifier)
        {
            return property.Modifiers.Any(x => string.Compare(x.Text, modifier, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        public class InvalidProjectPathException : Exception
        {
        }
    }
}
