namespace Furnace.Roslyn
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

    using System;
    using ContentTypes;
    using Models.ContentTypes;

    public class RoslynContentTypes : IFurnaceContentTypes
    {
        private readonly Project _project;

        public RoslynContentTypes(string projectPath)
        {
            if(string.IsNullOrEmpty(projectPath))
                throw new InvalidProjectPathException();

            var workspace = MSBuildWorkspace.Create();
            _project = workspace.OpenProjectAsync(projectPath).Result;
        }

        public IEnumerable<ContentType> GetContentTypes()
        {
            foreach (var document in _project.Documents)
            {
                var documentRoot = document
                    .GetSyntaxRootAsync()
                    .Result;

                var classes = documentRoot
                    .GetClassDeclarationSyntax()
                    .ToList();

                if (!classes.Any())
                    continue;

                var model = document.GetSemanticModelAsync().Result;
                foreach (var classDeclarationSyntax in classes)
                {
                    var symbol = model.GetDeclaredSymbol(classDeclarationSyntax);

                    var contentType = new ContentType
                                          {
                                              Name = symbol.ToMinimallyQualified(),
                                              Namespace = symbol.GetNamespace()
                                          };

                    foreach (var property in documentRoot.GetPropertyDeclarationSyntax())
                    {
                        contentType.Properties.Add(property.GetFurnaceContentTypeProperty());
                    }

                    yield return contentType;
                }
            }
        }

        public class InvalidProjectPathException : Exception
        {
        }
    }
}
