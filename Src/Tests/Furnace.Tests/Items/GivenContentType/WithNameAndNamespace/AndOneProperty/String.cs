using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.AndOneProperty
{
    [TestFixture]
    public class String : AndOnePropertyTest
    {
        protected override string PropertyName
        {
            get { return "SomeName"; }
        }

        protected override string PropertyType
        {
            get { return "string"; }
        }

        protected override object DefaultValue
        {
            get { return "SomeValue"; }
        }
    }
}
