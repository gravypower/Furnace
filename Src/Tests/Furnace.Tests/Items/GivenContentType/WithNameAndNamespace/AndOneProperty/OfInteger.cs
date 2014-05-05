using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.AndOneProperty
{
    [TestFixture]
    public class OfInteger : AndOnePropertyTest
    {
        public OfInteger(string furnaceItemsType)
            : base(furnaceItemsType)
        {
        }

        protected override string PropertyName
        {
            get { return "SomeName"; }
        }

        protected override string PropertyType
        {
            get { return "int"; }
        }

        protected override object DefaultValue
        {
            get { return 1; }
        }
    }
}
