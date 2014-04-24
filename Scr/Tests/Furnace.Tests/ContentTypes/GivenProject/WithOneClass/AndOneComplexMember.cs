namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass
{
    using Furnace.Tests.ContentTypes.GivenProjectWith.OneClass;

    public class AndOneComplexMember : WithOneClassTests
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
