using Microsoft.AspNetCore.Mvc;
using System;
using topggcsharpchallenge.Services;

namespace topggcsharpchallenge.Controllers
{
    [ApiController]
    [Route("earthquakes")]
    public class EarthquakeController : ControllerBase
    {
        private IEarthquakeService earthquakeService;

        public EarthquakeController(IEarthquakeService earthquakeService)
        {
            this.earthquakeService = earthquakeService;
        }

        [HttpGet]
        public void Get(int latitude, int longitude, DateTime startDate, DateTime endDate)
        {
            earthquakeService.Get(latitude, longitude, startDate, endDate);
        }
    }
}
