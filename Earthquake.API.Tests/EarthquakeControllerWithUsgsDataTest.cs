using AutoMapper;
using Earthquake.API.AutoMappingProfiles;
using Earthquake.API.Controllers;
using Earthquake.Data.USGS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace Earthquake.API.Tests
{
    [TestClass]
    public class EarthquakeControllerWithUsgsDataTest
    {
        private EarthquakeUsgsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            var usgsHttpClient = new UsgsHttpClient("https://earthquake.usgs.gov/fdsnws/event/1/");
            var dataContext = new UsgsDataContext(usgsHttpClient);
            var mapperProfile = new EarthquakeMappingProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(mapperConfiguration);

            _controller = new EarthquakeUsgsController(dataContext, mapper);
        }

        [TestMethod]
        public void EndPoint_ReturnsData_Is_True()
        {
            const double latitude = 38.5563316;
            const double longitude = -119.5276642;
            var startDate = DateTime.Parse("2021-10-12");
            var endDate = DateTime.Parse("2021-10-13");

            var data = _controller.FindByCoordinatesBetweenDateRange(latitude, longitude, startDate, endDate);

            Assert.IsNotNull(data.Result);
            Assert.IsNotNull(((ObjectResult) data.Result).Value);
        }

        [TestMethod]
        public void EndPoint_Returns404_Is_True()
        {
            const double latitude = 90;
            const double longitude = 90;
            var startDate = DateTime.Parse("2024-01-01");
            var endDate = DateTime.Parse("2025-01-01");

            var data = _controller.FindByCoordinatesBetweenDateRange(latitude, longitude, startDate, endDate);

            Assert.AreEqual((int)HttpStatusCode.NotFound, ((StatusCodeResult)data.Result).StatusCode);

        }
    }
}
