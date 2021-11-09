using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualBasic.FileIO;

using topggcsharpchallenge.Models;

namespace topggcsharpchallenge.Services
{
    public class EarthquakeService : IEarthquakeService
    {
        private readonly IUsgsService usgsService;

        public EarthquakeService(IUsgsService usgsService)
        {
            this.usgsService = usgsService;
        }

        IList<EarthquakeResponseModel> IEarthquakeService.Get(double latitude, double longitude, DateTime startDate, DateTime endDate)
        {
            byte[] earthquakeCsv = usgsService.GetEarthquakeData();

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

        private IList<EarthquakeResponseModel> ParseEarthquakes(byte[] csv)
        {
            IList<EarthquakeResponseModel> result = new List<EarthquakeResponseModel>();
            using (TextFieldParser parser = new TextFieldParser(new MemoryStream(csv)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                bool headerSkipped = false;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (!headerSkipped)
                    {
                        headerSkipped = true;
                        continue;
                    }

                    result.Add(ParseEarthquake(fields));
                }
            }

            return result;
        }

        private EarthquakeResponseModel ParseEarthquake(string[] fields)
        {
            return new EarthquakeResponseModel()
            {
                Time = parseToDateTimeOrDefault(fields[0]),
                Latitude = parseToDoubleOrDefault(fields[1]),
                Longitude = parseToDoubleOrDefault(fields[2]),
                Depth = parseToDoubleOrDefault(fields[3]),
                Mag = parseToDoubleOrDefault(fields[4]),
                MagType = fields[5],
                Nst = parseToIntegerOrDefault(fields[6]),
                Gap = parseToDoubleOrDefault(fields[7]),
                Dmin = parseToDoubleOrDefault(fields[8]),
                Rms = parseToDoubleOrDefault(fields[9]),
                Net = fields[10],
                Id = fields[11],
                Updated = parseToDateTimeOrDefault(fields[12]),
                Place = fields[13],
                Type = fields[14],
                HorizontalError = parseToDoubleOrDefault(fields[15]),
                DepthError = parseToDoubleOrDefault(fields[16]),
                MagError = parseToDoubleOrDefault(fields[17]),
                MagNst = parseToIntegerOrDefault(fields[18]),
                Status = fields[19],
                LocationSource = fields[20],
                MagSource = fields[21]
            };
        }

        private double parseToDoubleOrDefault(string i)
        {
            return i != string.Empty ? double.Parse(i) : 0;
        }

        private int parseToIntegerOrDefault(string i)
        {
            return i != string.Empty ? int.Parse(i) : 0;
        }

        private DateTime parseToDateTimeOrDefault(string date)
        {
            return date != string.Empty ? DateTime.Parse(date) : new DateTime();
        }
    }
}
