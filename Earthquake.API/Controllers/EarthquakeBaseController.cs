using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Earthquake.Data;
using Earthquake.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Earthquake.API.Controllers
{
    public abstract class EarthquakeBaseController : ControllerBase
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        protected EarthquakeBaseController(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EarthquakeDto>> FindByCoordinatesBetweenDateRange(
                [FromQuery(Name = "lat")] double latitude, 
                [FromQuery(Name = "long")] double longitude,
                [FromQuery(Name = "start_date")] DateTime startDate,
                [FromQuery(Name = "end_date")] DateTime endDate
            )
        {
            try
            {
                // This multiplier was an arbitrary number.
                const byte magnitudeMultiplier = 100;

                var earthquakes = _dataContext.FindByCoordinatesBetweenDateRange(latitude, longitude, startDate.ToUniversalTime(),
                    endDate.ToUniversalTime(), magnitudeMultiplier);
                if (earthquakes.Any())
                {
                    var earthquakesDto = _mapper.Map<IEnumerable<EarthquakeDto>>(earthquakes);
                    return Ok(earthquakesDto);
                }
            }
            catch (DataContextException)
            {
                //TODO: Do something with the dataContextException
            }
            catch (Exception)
            {
                //TODO: Do something with other exceptions
            }

            return NotFound();
        }
    }
}
