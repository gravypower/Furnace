using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithTwoClasses
{
    [TestFixture]
    public class AndInOneFile : WithTwoClassesTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"WithTwoClasses\WithTwoClasses.AndInOneFile\WithTwoClasses.AndInOneFile.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndInOneFile"; }
        }
    }
}
