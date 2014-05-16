using System;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithTwoClasses
{
    [TestFixture]
    public class AndInOneFile : WithTwoClassesTests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"WithTwoClasses\WithTwoClasses.AndInOneFile\WithTwoClasses.AndInOneFile.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndInOneFile"; }
        }

        protected override Type[] Types
        {
            get
            {
                return new[]
                {
                    typeof (global::WithTwoClasses.AndInOneFile.Test1),
                    typeof (global::WithTwoClasses.AndInOneFile.Test2)
                };
            }
        }

        [SetUp]
        public void AndOneMemberSetUp()
        {
            var loadType1 = new global::WithTwoClasses.AndInOneFile.Test1();
            var loadType2 = new global::WithTwoClasses.AndInOneFile.Test2();
        }
    }
}
