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
            string earthquakeCsv = usgsService.GetEarthquakeData();

            IList<EarthquakeResponseModel> earthquakes = ParseEarthquakes(earthquakeCsv);

            return earthquakes
                .Where(x => startDate <= x.Time && x.Time <= endDate)
                .Where(x => CalculateDistanceInSphere(x.Latitude, x.Longitude, latitude, longitude) <= x.Mag * Constants.TRAVEL_DISTANCE_FACTOR)
                .Take(Constants.EARTHQUAKE_COUNT_LIMIT)
                .OrderByDescending(x => x.Time)
                .ToList();
        }

        private double CalculateDistanceInSphere(double lat1, double long1, double lat2, double long2, int sphereRadius = Constants.EARTH_RADIUS_MILES)
        {
            int degreesInACircle = 180;
            double lat1rad = lat1 * Math.PI / degreesInACircle;
            double lat2rad = lat2 * Math.PI / degreesInACircle;
            double deltaLat = (lat1 - lat2) * Math.PI / degreesInACircle;
            double deltaLong = (long1 - long2) * Math.PI / degreesInACircle;

            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) + Math.Pow(Math.Sin(deltaLong / 2), 2) * Math.Cos(lat1rad) * Math.Cos(lat2rad);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            return sphereRadius * c;
        }

        private IList<EarthquakeResponseModel> ParseEarthquakes(string csv)
        {
            string[] allRows = csv.Split('\n');
            IList<EarthquakeResponseModel> result = new List<EarthquakeResponseModel>();
            IEnumerable<string> dataRows = allRows.TakeLast(allRows.Length - 1);
            foreach(string row in dataRows)
            {
                result.Add(ParseEarthquake(row));
            }

            return result;
        }

        private EarthquakeResponseModel ParseEarthquake(string row)
        {
            string[] columnData = row.Split(','); ;

            return new EarthquakeResponseModel()
            {
                Time = DateTime.Parse(columnData[0]),
                Latitude = double.Parse(columnData[1]),
                Longitude = double.Parse(columnData[2]),
                Depth = double.Parse(columnData[3]),
                Mag = double.Parse(columnData[4]),
                MagType = columnData[5],
                Nst = int.Parse(columnData[6]),
                Gap = int.Parse(columnData[7]),
                Dmin = double.Parse(columnData[8]),
                Rms = double.Parse(columnData[9]),
                Net = columnData[10],
                Id = columnData[11],
                Updated = DateTime.Parse(columnData[12]),
                Place = columnData[13],
                Type = columnData[14],
                HorizontalError = double.Parse(columnData[15]),
                DepthError = double.Parse(columnData[16]),
                MagError = double.Parse(columnData[17]),
                MagNst = int.Parse(columnData[18]),
                Status = columnData[19],
                LocationSource = columnData[20],
                MagSource = columnData[21],
            };
        }
    }
}
