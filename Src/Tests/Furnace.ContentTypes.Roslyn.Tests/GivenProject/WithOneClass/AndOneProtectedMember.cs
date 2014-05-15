using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    [TestFixture]
    public class AndOneProtectedMember : WithOneClassTests
    {
        [SetUp]
        public void AndOneProtectedMemberSetUp()
        {
            var loadType = new global::WithOneClass.AndOneProtectedMember.Test();
        }

        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndOneProtectedMember\WithOneClass.AndOneProtectedMember.csproj";
            }
        }

        protected override Type Type
        {
            get { return typeof(global::WithOneClass.AndOneProtectedMember.Test); }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOneProtectedMember"; }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasNoProperties()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test").Count, Is.EqualTo(0));
        }
    }
}
