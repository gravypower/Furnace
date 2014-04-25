namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass
{
    using NUnit.Framework;

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
