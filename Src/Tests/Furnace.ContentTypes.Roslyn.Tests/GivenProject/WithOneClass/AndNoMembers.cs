using System;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    [TestFixture]
    public class AndNoMembers : WithOneClassTests
    {
        [SetUp]
        public void AndNoMembersSetUp()
        {
            var loadType = new global::WithOneClass.AndNoMembers.Test();
        }

        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndNoMembers\WithOneClass.AndNoMembers.csproj";
            }
        }

        protected override Type Type
        {
            get { return typeof (global::WithOneClass.AndNoMembers.Test); }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndNoMembers"; }
        }
    }
}
