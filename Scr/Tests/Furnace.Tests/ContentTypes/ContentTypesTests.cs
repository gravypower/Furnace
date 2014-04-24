using Furnace.ContentTypes;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes
{
    [TestFixture]
    public abstract class ContentTypesTests
    {
        private const string BaseProjectPath = @"D:\GitHub\Furnace\Scr\Tests\TestProjects\";

        protected abstract string ProjectPath { get; }
        protected IFurnaceContentTypes Sut;

        [SetUp]
        public void SetUp()
        {
            Sut = new RoslynContentTypes(BaseProjectPath + ProjectPath);
        }
    }
}
