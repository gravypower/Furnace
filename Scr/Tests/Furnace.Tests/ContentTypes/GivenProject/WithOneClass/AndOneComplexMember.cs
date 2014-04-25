﻿namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass
{
    public class AndOneComplexMember : WithOneClassTests
    {
        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneComplexMember\WithOneClass.AndOneComplexMember.csproj";
            }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOneComplexMember"; }
        }
    }
}
