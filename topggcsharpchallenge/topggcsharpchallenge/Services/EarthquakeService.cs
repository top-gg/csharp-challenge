using System;
using System.Collections.Generic;
using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    public class EarthquakeService : IEarthquakeService
    {
        IEnumerable<EarthquakeResponseModel> IEarthquakeService.Get(int latitude, int longitude, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
