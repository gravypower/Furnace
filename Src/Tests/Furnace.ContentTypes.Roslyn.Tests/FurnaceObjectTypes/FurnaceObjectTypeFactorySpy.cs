using System.Collections.Generic;
using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;
using Furnace.ContentTypes.Roslyn.Models;
using Microsoft.CodeAnalysis;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    public class FurnaceObjectTypeFactorySpy : FurnaceObjectTypeFactory
    {
        public new SyntaxNode TemplateClassRoot
        {
            get { return base.TemplateClassRoot; }
        }

        public new IEnumerable<FurnaceType> FurnaceTypes
        {
            get { return base.FurnaceTypes; }
        }

        public FurnaceObjectTypeFactorySpy(string templateFilePath) : base(templateFilePath)
        {
        }

        public FurnaceObjectTypeFactorySpy(string templateFilePath, ITypeFinder typeFinder) : base(templateFilePath, typeFinder)
        {
        }
    }
}
