namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass
{
    public class GivenOneComplexMember : OneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"OneClass.WithOneComplexMember\OneClass.WithOneComplexMember.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "WithOneComplexMember"; }
        }
    }
}
