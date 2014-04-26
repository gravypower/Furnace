namespace Furnace.Roslyn.Tests.ContentTypes.GivenProject.WithTwoClasses
{
    using NUnit.Framework;

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
