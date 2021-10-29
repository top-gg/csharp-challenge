using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Earthquake.Data;
using Earthquake.DTO;
using Earthquake.Entities;
using Earthquake.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Earthquake.API.Controllers
{
    public abstract class EarthquakeBaseController : ControllerBase
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        protected EarthquakeBaseController(IDataContext dataContext, IMapper mapper, IDistributedCache cache)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EarthquakeDto>>> FindByCoordinatesBetweenDateRange(
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

                var cacheKey = $"{latitude}#{longitude}#{startDate}#{endDate}";
                var earthquakes = await _cache.GetAsync<IEnumerable<EarthquakeEntity>>(cacheKey);

                if (earthquakes == null)
                {
                    earthquakes = _dataContext.FindByCoordinatesBetweenDateRange(latitude, longitude,
                        startDate.ToUniversalTime(),
                        endDate.ToUniversalTime(), magnitudeMultiplier);
                    await _cache.SetAsync(cacheKey, earthquakes, TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(10) );
                }

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