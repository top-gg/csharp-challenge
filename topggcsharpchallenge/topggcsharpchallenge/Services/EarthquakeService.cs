using System;
using System.Collections.Generic;
using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    class EarthquakeService : IEarthquakeService
    {
        private readonly IUsgsService usgsService;

        public EarthquakeService(IUsgsService usgsService)
        {
            this.usgsService = usgsService;
        }

        IEnumerable<EarthquakeResponseModel> IEarthquakeService.Get(int latitude, int longitude, DateTime startDate, DateTime endDate)
        {
            IEnumerable<EarthquakeResponseModel> earthquakeData = usgsService.getEarthquakeData();
            return earthquakeData;
        }
    }
}
