using System;
using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace
{
    [TestFixture]
    public abstract class WithNameAndNamespaceTests : GivenContentTypeTests
    {
        protected const string ContentTypeName = "SomeName";
        protected const string ContentTypeNamespace = "SomeNamespace";

        [SetUp]
        public void WithNameAndNamespaceTestsSetUp()
        {
            ContentType = new ContentType { Name = ContentTypeName, Namespace = ContentTypeNamespace };
        }

        protected void AddPropityToContentType(string propertyName = null, string type = null, object defaltValue = null)
        {
            var property = new Property
            {
                Name = propertyName,
                Type = type,
                DefaultValue = defaltValue
            };

            ContentType.Properties.Add(property);
        }
    }
}