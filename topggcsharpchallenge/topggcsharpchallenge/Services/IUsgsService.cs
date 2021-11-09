using System.Collections.Generic;

using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    interface IUsgsService
    {
        IList<EarthquakeResponseModel> getEarthquakeData();
    }
}
