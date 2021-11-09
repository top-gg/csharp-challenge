using System;
using System.Collections.Generic;

using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    public interface IEarthquakeService
    {
        IList<EarthquakeResponseModel> Get(double latitude, double longitude, DateTime startDate, DateTime endDate);
    }
}
