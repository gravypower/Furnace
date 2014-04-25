namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember
{
    using NUnit.Framework;

    [TestFixture]
    public class IntegerMember : AndOneMemberTests
    {
        protected override string ExpectedNamespace
        {
            get
            {
                return "AndOneIntegerMember";
            }
        }

        protected override string AndOneMemberProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneIntegerMember\WithOneClass.AndOneIntegerMember.csproj";
            }
        }

        protected override string ExpectedPropertyName
        {
            get
            {
                return "IntegerProperty";
            }
        }

        protected override string ExpectedPropertyType
        {
            get
            {
                return "int";
            }
        }
    }
}
