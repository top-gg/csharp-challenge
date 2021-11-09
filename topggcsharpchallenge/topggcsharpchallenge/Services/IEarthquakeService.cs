using System;
using System.Collections.Generic;

using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    interface IEarthquakeService
    {
        IList<EarthquakeResponseModel> Get(int latitude, int longitude, DateTime startDate, DateTime endDate);
    }
}
