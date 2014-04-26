namespace Furnace.Roslyn
{
    using System.Collections.Generic;
    using System.Linq;

    using ContentTypes.Model;

    using Extensions;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

    public class RoslynContentTypes : IFurnaceContentTypes
    {
        private readonly Project project;

        public RoslynContentTypes(string projectPath)
        {
            var workspace = MSBuildWorkspace.Create();
            project = workspace.OpenProjectAsync(projectPath).Result;
        }

        public IEnumerable<FurnaceContentType> GetContentTypes()
        {
            foreach (var document in project.Documents)
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
                var symbol = model.GetDeclaredSymbol(classes.Single());

                var contentType = new FurnaceContentType
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
}
