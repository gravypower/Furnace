using System;
using System.Linq;
using NUnit.Framework;

namespace Furnace.ContentTypes.Roslyn.Tests.GivenProject.WithOneClass
{
    [TestFixture]
    public class AndOnePrivateMember : WithOneClassTests
    {
        [SetUp]
        public void AndOnePrivateMemberSetUp()
        {
            var loadType = new global::WithOneClass.AndOnePrivateMember.Test();
        }

        protected override string OneClassProjectPath
        {
            get
            {
                return @"WithOneClass.AndOnePrivateMember\WithOneClass.AndOnePrivateMember.csproj";
            }
        }

        protected override Type[] Types
        {
            get { return new []{typeof(global::WithOneClass.AndOnePrivateMember.Test)}; }
        }

        protected override string ExpectedNamespace
        {
            get { return "AndOnePrivateMember"; }
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
