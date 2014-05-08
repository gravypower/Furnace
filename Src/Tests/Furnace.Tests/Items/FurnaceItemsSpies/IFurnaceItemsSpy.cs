using Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.Localisation;

namespace Furnace.Tests.Items.FurnaceItemsSpies
{
    public interface IFurnaceItemsSpy
    {
        void AssertWhenGetItemIsCalled_ThenCorrectKey_IsUsed(LocalisationTests fixture);
    }
}
