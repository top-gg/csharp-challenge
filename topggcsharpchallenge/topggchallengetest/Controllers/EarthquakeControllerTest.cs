using Moq;
using NUnit.Framework;
using System;
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
            int latitude = 10;
            int longitude = 20;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.Now;

            sut.Get(latitude, longitude, startDate, endDate);

            earthquakeServiceMock.Verify((x) => x.Get(latitude, longitude, startDate, endDate), Times.Once);
        }
    }
}
