using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    [TestFixture]
    public class AndNoMembers : WithOneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndNoMembers\WithOneClass.AndNoMembers.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndNoMembers"; }
        }
    }
}
