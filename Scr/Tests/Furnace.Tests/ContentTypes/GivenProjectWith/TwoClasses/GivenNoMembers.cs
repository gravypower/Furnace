using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.TwoClasses
{
    [TestFixture]
    public class GivenNoMembers : TwoClassesTests
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
