using System;

namespace Earthquake.Infrastructure
{
    /*
     * Copied and modified from https://gist.github.com/jammin77/033a332542aa24889452
     * benjammin72@gmail.com
     */
    public static class Haversine
    {
        private const ushort EarthRadiusInMiles = 3959;

        public static double CalculateDistanceInMiles(double firstCoordinatesLatitude, double firstCoordinatesLongitude,
            double secondCoordinatesLatitude, double secondCoordinatesLongitude)
        {
            var latitudeDiff = ToRadian(secondCoordinatesLatitude - firstCoordinatesLatitude);
            var longitudeDiff = ToRadian(secondCoordinatesLongitude - firstCoordinatesLongitude);

            var a = Math.Sin(latitudeDiff / 2) * Math.Sin(latitudeDiff / 2) +
                    Math.Cos(ToRadian(firstCoordinatesLatitude)) * Math.Cos(ToRadian(secondCoordinatesLatitude)) *
                    Math.Sin(longitudeDiff / 2) * Math.Sin(longitudeDiff / 2);
            var c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            var distance = EarthRadiusInMiles * c;

            return distance;
        }

        private static double ToRadian(double val)
        {
            return Math.PI / 180 * val;
        }
    }
}