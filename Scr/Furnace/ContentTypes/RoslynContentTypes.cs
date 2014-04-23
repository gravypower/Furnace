using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace Furnace.ContentTypes
{
    public class RoslynContentTypes : IFurnaceContentTypes
    {
        private readonly Project _project;

        public RoslynContentTypes(string projectPath)
        {
            var workspace = MSBuildWorkspace.Create();
            _project = workspace.OpenProjectAsync(projectPath).Result;
        }

        public IEnumerable<FurnaceContentType> GetContentTypes()
        {
            var contentTypes = new List<FurnaceContentType>();
            foreach (var document in _project.Documents)
            {
                var root = document.GetSyntaxRootAsync().Result;
                var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();


                if (!classes.Any())
                    continue;

                var model = document.GetSemanticModelAsync().Result;
                var structType = model.GetDeclaredSymbol(classes.Single());
                 var typeName = structType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

                contentTypes.Add(new FurnaceContentType {Name = typeName });
            }

            return contentTypes;
        }
    }
}
