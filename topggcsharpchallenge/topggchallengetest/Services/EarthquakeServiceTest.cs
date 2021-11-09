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
        readonly Mock<IUsgsService> usgsServiceMock = new Mock<IUsgsService>();

        IEarthquakeService sut;

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
            DateTime endDate = DateTime.Now;
            IEnumerable<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            usgsServiceMock.Setup((x) => x.getEarthquakeData()).Returns(expectedEarthquakeData);

            IEnumerable<EarthquakeResponseModel> actualEarthquakeData = sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.EqualTo(expectedEarthquakeData));
        }

        private IEnumerable<EarthquakeResponseModel> getUsgsServiceGetEarthquakeDataMocks()
        {
            return new List<EarthquakeResponseModel>()
            {
                new EarthquakeResponseModel()
                {
                    Time = DateTime.MinValue,
                    Latitude = 0.1234,
                    Longitude = 4.3210,
                    Depth = 123.456,
                    Mag = 654.312,
                    MagType = "md",
                    Nst = 1,
                    Gap = 2,
                    Dmin = 2.34,
                    Rms = 3.45,
                    Net = "nc",
                    Id = "nc73636400",
                    Updated = DateTime.Now,
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
                    Time = DateTime.Now,
                    Latitude = 0.000001,
                    Longitude = 0.000002,
                    Depth = 0.000003,
                    Mag = 0.000004,
                    MagType = "dm",
                    Nst = 200,
                    Gap = 300,
                    Dmin = 0.000005,
                    Rms = 0.000006,
                    Net = "cn",
                    Id = "cn73636400",
                    Updated = DateTime.MaxValue,
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
