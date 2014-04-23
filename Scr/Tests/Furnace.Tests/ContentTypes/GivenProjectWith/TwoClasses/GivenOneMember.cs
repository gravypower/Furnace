using System.Linq;
using NUnit.Framework;

namespace Furnace.Tests.ContentTypes.GivenProjectWith.TwoClasses
{
    [TestFixture]
    public class GivenOneMember : Tests
    {
        protected override string ProjectPath
        {
            get
            {
                return @"TwoClasses.WithOneMember\TwoClasses.WithOneMember.csproj";
            }
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasOneProperty()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result[0].Properties.Count, Is.EqualTo(1));
            Assert.That(result[1].Properties.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenGetContentTypesIsCalled_ThenTheFurnaceContentTypeReturnedHasCorrectPropertyNames()
        {
            //Act
            var result = Sut.GetContentTypes().ToList();

            //Assert
            Assert.That(result[1].Properties.Select(x => x), Contains.Item("StringProperty1"));
            Assert.That(result[0].Properties.Select(x => x), Contains.Item("StringProperty2"));
        }
    }
}
