using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using topggcsharpchallenge.Models;
using topggcsharpchallenge.Services;

namespace topggcsharpchallengetest.Services
{
    [TestFixture]
    public class EarthquakeServiceTest
    {
        private readonly Mock<IUsgsService> usgsServiceMock = new Mock<IUsgsService>();

        private IEarthquakeService sut;

        [SetUp]
        public void SetUp()
        {
            sut = new EarthquakeService(usgsServiceMock.Object);
        }

        [Test]
        public void GetShouldBeSuccessfull()
        {
            int latitude = 10;
            int longitude = 20;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            usgsServiceMock.Setup((x) => x.getEarthquakeData()).Returns(expectedEarthquakeData);

            IEnumerable<EarthquakeResponseModel> actualEarthquakeData = sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.EqualTo(expectedEarthquakeData));
        }

        [Test]
        public void GetShouldReturnEmptyWhenIntervalBeforeAnyQuakes()
        {
            int latitude = 10;
            int longitude = 20;
            DateTime startDate = new DateTime(1111);
            DateTime endDate = new DateTime(1112);
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            usgsServiceMock.Setup((x) => x.getEarthquakeData()).Returns(expectedEarthquakeData);

            IEnumerable<EarthquakeResponseModel> actualEarthquakeData = sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.Empty);
        }

        [Test]
        public void GetShouldReturnEmptyWhenIntervalAfterAnyQuakes()
        {
            int latitude = 10;
            int longitude = 20;
            DateTime startDate = new DateTime(2222);
            DateTime endDate = new DateTime(2223);
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            usgsServiceMock.Setup((x) => x.getEarthquakeData()).Returns(expectedEarthquakeData);

            IEnumerable<EarthquakeResponseModel> actualEarthquakeData = sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.Empty);
        }

        private IList<EarthquakeResponseModel> getUsgsServiceGetEarthquakeDataMocks()
        {
            return new List<EarthquakeResponseModel>()
            {
                new EarthquakeResponseModel()
                {
                    Time = new DateTime(1993, 9, 29),
                    Latitude = 0,
                    Longitude = 0,
                    Depth = 123.456,
                    Mag = 654.312,
                    MagType = "md",
                    Nst = 1,
                    Gap = 2,
                    Dmin = 2.34,
                    Rms = 3.45,
                    Net = "nc",
                    Id = "nc73636400",
                    Updated = new DateTime(1993, 9, 29),
                    Place = "sofia",
                    Type = "earthquake",
                    HorizontalError = 0.111,
                    DepthError = 0.222,
                    MagError = 0.333,
                    MagNst = 4,
                    Status = "automatic",
                    LocationSource = "nc",
                    MagSource = "nc"
                },
                new EarthquakeResponseModel()
                {
                    Time = new DateTime(1996, 1, 11),
                    Latitude = 180,
                    Longitude = 180,
                    Depth = 0.000003,
                    Mag = 0.000004,
                    MagType = "dm",
                    Nst = 200,
                    Gap = 300,
                    Dmin = 0.000005,
                    Rms = 0.000006,
                    Net = "cn",
                    Id = "cn73636400",
                    Updated = new DateTime(1996, 1, 11),
                    Place = "nyc",
                    Type = "earthquake",
                    HorizontalError = 0.000007,
                    DepthError = 0.000008,
                    MagError = 0.000009,
                    MagNst = 400,
                    Status = "reviwed",
                    LocationSource = "ls",
                    MagSource = "ms"
                },
            };
        }
    }
}
