using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Microsoft.CodeAnalysis;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    public class FurnaceObjectTypeFactorySpy : FurnaceObjectTypeFactory
    {
        public new SyntaxNode TemplateClassRoot
        {
            get { return base.TemplateClassRoot; }
        }

        public FurnaceObjectTypeFactorySpy(string templteFilePath) : base(templteFilePath)
        {
        }
    }
}
