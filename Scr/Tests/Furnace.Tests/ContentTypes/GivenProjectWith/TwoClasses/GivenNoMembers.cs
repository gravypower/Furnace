using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.TwoClasses
{
    [TestFixture]
    public class GivenNoMembers :Tests
    {
        protected override string ProjectPath
        {
            get
            {
                return
                    @"TwoClasses.WithNoMembers\TwoClasses.WithNoMembers.csproj";
            }
        }
    }
}
