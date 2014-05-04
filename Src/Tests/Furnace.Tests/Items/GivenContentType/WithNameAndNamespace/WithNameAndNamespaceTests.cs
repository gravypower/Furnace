﻿using Furnace.Models.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.Items.GivenContentType.WithNameAndNamespace
{
    [TestFixture]
    public abstract class WithNameAndNamespaceTests : GivenContentTypeTests
    {
        protected const string ContentTypeName = "SomeType";
        protected const string ContentTypeNamespace = "SomeNamespace";

        [SetUp]
        public void WithNameAndNamespaceTestsSetUp()
        {
            ContentType = new ContentType { Name = ContentTypeName, Namespace = ContentTypeNamespace };
        }
    }
}