using System;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithTwoClasses
{
    [TestFixture]
    public class GivenNoMembers : WithTwoClassesTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"WithTwoClasses\WithTwoClasses.AndNoMembers\WithTwoClasses.AndNoMembers.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndNoMembers"; }
        }

        protected override Type[] Types
        {
            get
            {
                return new[]
                {
                    typeof (global::WithTwoClasses.AndNoMembers.Test1),
                    typeof (global::WithTwoClasses.AndNoMembers.Test2)
                };
            }
        }

        [SetUp]
        public void AndOneMemberSetUp()
        {
            var loadType1 = new global::WithTwoClasses.AndNoMembers.Test1();
            var loadType2 = new global::WithTwoClasses.AndNoMembers.Test2();
        }
    }
}
