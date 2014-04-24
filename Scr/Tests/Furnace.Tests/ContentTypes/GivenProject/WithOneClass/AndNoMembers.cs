namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass
{
    using Furnace.Tests.ContentTypes.GivenProjectWith.OneClass;

    using NUnit.Framework;

    [TestFixture]
    public class AndNoMembers : WithOneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"OneClass.WithNoMembers\OneClass.WithNoMembers.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "WithNoMembers"; }
        }
    }
}
