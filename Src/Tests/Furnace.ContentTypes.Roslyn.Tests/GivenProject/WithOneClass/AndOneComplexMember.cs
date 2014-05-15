using System;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    public class AndOneComplexMember : WithOneClassTests
    {
        [SetUp]
        public void AndOneComplexMemberSetUp()
        {
            var loadType = new global::WithOneClass.AndOneComplexMember.Test();
        }

        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneComplexMember\WithOneClass.AndOneComplexMember.csproj";
            }
        }

        protected override Type Type
        {
            get { return typeof(global::WithOneClass.AndOneComplexMember.Test); }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOneComplexMember"; }
        }
    }
}
