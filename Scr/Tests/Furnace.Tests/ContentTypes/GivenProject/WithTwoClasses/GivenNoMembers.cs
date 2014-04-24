namespace Furnace.Tests.ContentTypes.GivenProject.WithTwoClasses
{
    using NUnit.Framework;

    [TestFixture]
    public class GivenNoMembers : WithTwoClassesTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"TwoClasses\TwoClasses.WithNoMembers\TwoClasses.WithNoMembers.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "WithNoMembers"; }
        }
    }
}
