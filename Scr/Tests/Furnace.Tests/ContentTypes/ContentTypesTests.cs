using Furnace.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes
{
    [TestFixture]
    public abstract class ContentTypesTests
    {
        private const string BaseProjectPath = @"E:\Dev\Scr\Tests\TestModels\";
        protected IFurnaceContentTypes Sut;

        [SetUp]
        public void SetUp()
        {
            Sut = new RoslynContentTypes(BaseProjectPath + ProjectPath);
        }

        protected abstract string ProjectPath { get; }
    }
}
