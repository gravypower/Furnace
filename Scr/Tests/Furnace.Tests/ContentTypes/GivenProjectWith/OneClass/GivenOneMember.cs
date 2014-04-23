using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.OneClass
{
    [TestFixture]
    public class GivenOneMember : Tests
    {      
        protected override string ProjectPath
        {
            get { return @"OneClass.WithOneMember\OneClass.WithOneMember.csproj"; }
        }
    }
}
