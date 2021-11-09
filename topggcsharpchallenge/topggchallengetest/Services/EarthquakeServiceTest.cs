using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using topggcsharpchallenge;
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
        public async Task GetShouldBeSuccessfull()
        {
            int latitude = 0;
            int longitude = 0;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            IList<EarthquakeResponseModel> mockedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            byte[] csv = createCsv(mockedEarthquakeData);
            usgsServiceMock.Setup((x) => x.GetEarthquakeData()).Returns(Task.FromResult(csv));
            IList<EarthquakeResponseModel> expectedEarthquakeData = mockedEarthquakeData.OrderByDescending(x => x.Time).ToList();

            IList<EarthquakeResponseModel> actualEarthquakeData = await sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.EqualTo(expectedEarthquakeData));
            IList<DateTime> dates = actualEarthquakeData.Select(x => x.Time).ToList();
            for (int i = 0; i < dates.Count - 1; i++)
            {
                Assert.That(dates[i] >= dates[i + 1]);
            }
        }

        [Test]
        public async Task GetShouldReturnEmptyWhenIntervalBeforeAnyQuakes()
        {
            int latitude = 10;
            int longitude = 20;
            DateTime startDate = new DateTime(1111, 1, 1);
            DateTime endDate = new DateTime(1112, 1, 1);
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            byte[] csv = createCsv(expectedEarthquakeData);
            usgsServiceMock.Setup((x) => x.GetEarthquakeData()).Returns(Task.FromResult(csv));

            IEnumerable<EarthquakeResponseModel> actualEarthquakeData = await sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.Empty);
        }

        [Test]
        public async Task GetShouldReturnEmptyWhenIntervalAfterAnyQuakes()
        {
            int latitude = 10;
            int longitude = 20;
            DateTime startDate = new DateTime(2222, 1, 1);
            DateTime endDate = new DateTime(2223, 1, 1);
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            byte[] csv = createCsv(expectedEarthquakeData);
            usgsServiceMock.Setup((x) => x.GetEarthquakeData()).Returns(Task.FromResult(csv));

            IList<EarthquakeResponseModel> actualEarthquakeData = await sut .Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.Empty);
        }

        [Test]
        public async Task GetShouldReturnOnlyQuakesWithValidTime()
        {
            int latitude = 0;
            int longitude = 0;
            DateTime startDate = new DateTime(1995, 1, 1);
            DateTime endDate = new DateTime(2020, 1, 1);
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            byte[] csv = createCsv(expectedEarthquakeData);
            usgsServiceMock.Setup((x) => x.GetEarthquakeData()).Returns(Task.FromResult(csv));

            IList<EarthquakeResponseModel> actualEarthquakeData = await sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData.Count, Is.EqualTo(1));
            Assert.That(actualEarthquakeData[0], Is.EqualTo(expectedEarthquakeData[1]));
        }

        [Test]
        public async Task GetShouldReturnNoQuakesWhenThereAreNoneInRange()
        {
            int latitude = 90;
            int longitude = 90;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            byte[] csv = createCsv(expectedEarthquakeData);
            usgsServiceMock.Setup((x) => x.GetEarthquakeData()).Returns(Task.FromResult(csv));

            IList<EarthquakeResponseModel> actualEarthquakeData = await sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData, Is.Empty);
        }

        [Test]
        public async Task GetShouldReturnSomeQuakesWhenTheyAreInRange()
        {
            int latitude = 10;
            int longitude = 10;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocks();
            byte[] csv = createCsv(expectedEarthquakeData);
            usgsServiceMock.Setup((x) => x.GetEarthquakeData()).Returns(Task.FromResult(csv));

            IList<EarthquakeResponseModel> actualEarthquakeData = await sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData.Count, Is.EqualTo(1));
            Assert.That(actualEarthquakeData[0], Is.EqualTo(expectedEarthquakeData[1]));
        }

        [Test]
        public async Task GetShouldReturnNoMoreThanTheLimitOfResults()
        {
            int latitude = 0;
            int longitude = 0;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            IList<EarthquakeResponseModel> expectedEarthquakeData = getUsgsServiceGetEarthquakeDataMocksMany();
            byte[] csv = createCsv(expectedEarthquakeData);
            usgsServiceMock.Setup((x) => x.GetEarthquakeData()).Returns(Task.FromResult(csv));

            IList<EarthquakeResponseModel> actualEarthquakeData = await sut.Get(latitude, longitude, startDate, endDate);

            Assert.That(actualEarthquakeData.Count, Is.EqualTo(Constants.EARTHQUAKE_COUNT_LIMIT));
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
                    Mag = 0.00000001,
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
                    Mag = 20,
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
                    Status = "reviewed",
                    LocationSource = "ls",
                    MagSource = "ms"
                },
            };
        }

        private IList<EarthquakeResponseModel> getUsgsServiceGetEarthquakeDataMocksMany()
        {
            return new List<EarthquakeResponseModel>()
            {
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
                new EarthquakeResponseModel(),
            };
        }

        private byte[] createCsv(IList<EarthquakeResponseModel> quakes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("time,latitude,longitude,depth,mag,magType,nst,gap,dmin,rms,net,id,updated,place,type,horizontalError,depthError,magError,magNst,status,locationSource,magSource");
            foreach (EarthquakeResponseModel quake in quakes)
            {
                stringBuilder.Append("\n");
                stringBuilder.Append(quake.ToString());
            }


            return Encoding.ASCII.GetBytes(stringBuilder.ToString());
        }
    }
}
