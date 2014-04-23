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
                var classes = root.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .ToArray();

                if (!classes.Any())
                    continue;

                var model = document.GetSemanticModelAsync().Result;
                var structType = model.GetDeclaredSymbol(classes.Single());
                var typeName = structType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

                var t = new FurnaceContentType { Name = typeName };

                foreach (var property in root.DescendantNodes().OfType<PropertyDeclarationSyntax>())
                {
                    //Console.WriteLine(property.ExplicitInterfaceSpecifier);
                    //set.Add(property.Identifier.Text, property.Type.ToString());
                    //mockType[property.Identifier.Text] = property.Identifier.Text;
                    t.Properties.Add(property.Identifier.Text);
                }

                contentTypes.Add(t);
            }

            return contentTypes;
        }
    }
}
