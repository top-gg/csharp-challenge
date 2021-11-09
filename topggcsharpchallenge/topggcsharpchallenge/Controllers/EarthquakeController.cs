using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using topggcsharpchallenge.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EarthquakeResponseModel>> Get(
            [FromQuery(Name = "lat")] double latitude,
            [FromQuery(Name = "long")] double longitude,
            [FromQuery(Name = "start_date")] DateTime startDate,
            [FromQuery(Name = "end_date")] DateTime endDate)
        {
            IList<EarthquakeResponseModel> quakes = earthquakeService.Get(latitude, longitude, startDate, endDate);
            if (quakes.Count == 0)
            {
                return NotFound();
            }

            return new OkObjectResult(quakes);
        }
    }
}
