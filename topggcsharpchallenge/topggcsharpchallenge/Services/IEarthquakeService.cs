using System;

namespace topggcsharpchallenge.Services
{
    public interface IEarthquakeService
    {
        void Get(int latitude, int longitude, DateTime startDate, DateTime endDate);
    }
}
