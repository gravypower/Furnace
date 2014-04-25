namespace Furnace.Tests.ContentTypes.GivenProject.WithOneClass.AndOneMember
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public abstract class AndOneMemberTests : WithOneClassTests
    {
        protected abstract string AndOneMemberProjectPath { get; }
        protected override string OneClassProjectPath
        {
            get
            {
                return @"AndOneMember\" + AndOneMemberProjectPath;
            }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasOneProperty()
        {
            //Act
            var result = this.Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result.GetPropertyNames("Test").Count, Is.EqualTo(1));
        }
    }
}
