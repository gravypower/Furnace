using System;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace.AndOneProperty
{
    [TestFixture]
    public class OfDateTime : AndOnePropertyTest
    {
        protected override string PropertyName
        {
            get { return "SomeName"; }
        }

        protected override string PropertyType
        {
            get { return "DateTime"; }
        }

        protected override object DefaultValue
        {
            get { return new DateTime(2014, 4, 2); }
        }
    }
}
