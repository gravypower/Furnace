using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.AndOneProperty
{
    [TestFixture]
    public class OfBoolean : AndOnePropertyTest
    {
        public OfBoolean(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }

        protected override string PropertyName
        {
            get { return "SomeName"; }
        }

        protected override string PropertyType
        {
            get { return "bool"; }
        }

        protected override object DefaultValue
        {
            get { return true; }
        }
    }
}
