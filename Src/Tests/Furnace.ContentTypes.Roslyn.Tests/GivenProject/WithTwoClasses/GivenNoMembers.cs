using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithTwoClasses
{
    [TestFixture]
    public class GivenNoMembers : WithTwoClassesTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"WithTwoClasses\WithTwoClasses.AndNoMembers\WithTwoClasses.AndNoMembers.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndNoMembers"; }
        }
    }
}
