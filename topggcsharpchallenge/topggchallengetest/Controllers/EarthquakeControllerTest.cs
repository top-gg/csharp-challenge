using Moq;
using NUnit.Framework;
using topggcsharpchallenge.Controllers;
using topggcsharpchallenge.Services;

namespace topggcsharpchallengetest.Controllers
{
    [TestFixture]
    public class EarthquakeControllerTest
    {
        Mock<IEarthquakeService> earthquakeServiceMock = new Mock<IEarthquakeService>();
        EarthquakeController sut;

        [SetUp]
        public void SetUp()
        {
            sut = new EarthquakeController(earthquakeServiceMock.Object);
        }

        [Test]
        public void GetShouldBeSuccessfull()
        {
            sut.Get();

            earthquakeServiceMock.Verify((x) => x.Get(), Times.Once);
        }
    }
}
