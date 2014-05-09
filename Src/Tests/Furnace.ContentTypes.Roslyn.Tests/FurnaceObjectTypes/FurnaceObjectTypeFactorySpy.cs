using Furnace.ContentTypes.Roslyn.FurnaceObjectTypes;

namespace Furnace.ContentTypes.Roslyn.Tests.FurnaceObjectTypes
{
    public class FurnaceObjectTypeFactorySpy : FurnaceObjectTypeFactory
    {
        public FurnaceObjectTypeFactorySpy(string templteFilePath) : base(templteFilePath)
        {
        }
    }
}
