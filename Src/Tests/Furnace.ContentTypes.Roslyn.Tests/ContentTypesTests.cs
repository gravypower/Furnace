using Furnace.Interfaces.ContentTypes;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests
{
    [TestFixture]
    public abstract class ContentTypesTests
    {
        private const string BaseProjectPath = @"C:\GitHub\Furnace\Src\Tests\TestProjects\";
        //private const string BaseProjectPath = @"D:\GitHub\Furnace\Src\Tests\TestProjects\";
        //private const string BaseProjectPath = @"E:\Dev\Src\Tests\TestProjects\";

        protected abstract string ProjectPath { get; }
        protected IFurnaceContentTypes Sut;

        [SetUp]
        public void SetUp()
        {
            Sut = new RoslynContentTypes(BaseProjectPath + this.ProjectPath);
        }
    }
}
