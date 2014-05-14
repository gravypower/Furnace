using System.Collections.Generic;
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

        public new IEnumerable<SyntaxTree> FurnaceTypes
        {
            get { return base.FurnaceTypes; }
        }

        public FurnaceObjectTypeFactorySpy(string templateFilePath) : base(templateFilePath)
        {
        }
    }
}
