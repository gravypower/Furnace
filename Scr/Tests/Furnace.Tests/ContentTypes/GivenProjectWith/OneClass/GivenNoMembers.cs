using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass
{
    [TestFixture]
    public class GivenNoMembers : OneClassTests
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
