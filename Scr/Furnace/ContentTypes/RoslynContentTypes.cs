using System.Collections.Generic;
using System.Linq;
using Furnace.ContentTypes.Model;
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
                var classes = root.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .ToArray();

                if (!classes.Any())
                    continue;

                var model = document.GetSemanticModelAsync().Result;
                var structType = model.GetDeclaredSymbol(classes.Single());

                var contentType = new FurnaceContentType
                {
                    Name = structType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                    Namespace = structType.ContainingNamespace.ToDisplayString()
                };

                foreach (var property in root.DescendantNodes().OfType<PropertyDeclarationSyntax>())
                {
                    var furnaceContentTypeProperty = new FurnaceContentTypeProperty();

                    furnaceContentTypeProperty.Name = property.Identifier.Text;
                    furnaceContentTypeProperty.Type = property.Type.ToString();

                    if (property.Initializer != null)
                    {
                        var defaultValue = property.Initializer.ToString();
                        furnaceContentTypeProperty.DefaultValue = defaultValue.Substring(3, defaultValue.Length - 4);
                    }


                    contentType.Properties.Add(furnaceContentTypeProperty);
                }

                contentTypes.Add(contentType);
            }

            return contentTypes;
        }
    }
}
