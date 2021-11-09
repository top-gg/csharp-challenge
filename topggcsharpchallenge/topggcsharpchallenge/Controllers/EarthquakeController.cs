using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using topggcsharpchallenge.Models;
using topggcsharpchallenge.Services;

namespace topggcsharpchallenge.Controllers
{
    [ApiController]
    [Route("earthquakes")]
    class EarthquakeController : ControllerBase
    {
        private IEarthquakeService earthquakeService;

        public EarthquakeController(IEarthquakeService earthquakeService)
        {
            this.earthquakeService = earthquakeService;
        }

        [HttpGet]
        public IEnumerable<EarthquakeResponseModel> Get(int latitude, int longitude, DateTime startDate, DateTime endDate)
        {
            return earthquakeService.Get(latitude, longitude, startDate, endDate);
        }
    }
}
