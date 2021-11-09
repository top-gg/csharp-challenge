using System;
using System.Collections.Generic;
using System.Linq;
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

        IList<EarthquakeResponseModel> IEarthquakeService.Get(int latitude, int longitude, DateTime startDate, DateTime endDate)
        {
            IList<EarthquakeResponseModel> earthquakeData = usgsService.getEarthquakeData();
            return earthquakeData.Where(x => startDate <= x.Time && x.Time <= endDate).ToList();
        }
    }
}
