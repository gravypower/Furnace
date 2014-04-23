using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass
{
    [TestFixture]
    public class GivenNoMembers : Tests
    {
        protected override string ProjectPath
        {
            get { return @"OneClass.WithNoMembers\OneClass.WithNoMembers.csproj"; }
        }
    }
}
