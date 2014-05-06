using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass.AndOneMember
{
    [TestFixture]
    public class BooleanMember : AndOneMemberTests
    {
        protected override string ExpectedNamespace
        {
            get
            {
                return "AndOneBooleanMember";
            }
        }

        protected override string AndOneMemberProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneBooleanMember\WithOneClass.AndOneBooleanMember.csproj";
            }
        }

        protected override string ExpectedPropertyName
        {
            get
            {
                return "BooleanProperty";
            }
        }

        protected override string ExpectedPropertyType
        {
            get
            {
                return "bool";
            }
        }
    }
}
