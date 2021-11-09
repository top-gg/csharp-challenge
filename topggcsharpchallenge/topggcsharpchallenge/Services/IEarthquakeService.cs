using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    public interface IEarthquakeService
    {
        Task<IList<EarthquakeResponseModel>> Get(double latitude, double longitude, DateTime startDate, DateTime endDate);
    }
}
