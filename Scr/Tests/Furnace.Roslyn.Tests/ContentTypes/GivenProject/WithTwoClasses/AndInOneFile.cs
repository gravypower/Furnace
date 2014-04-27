namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithTwoClasses
{
    using NUnit.Framework;

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
