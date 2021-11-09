namespace topggcsharpchallenge
{
    public static class Constants
    {
        public const int EARTH_RADIUS_MILES = 3959;
        public const int TRAVEL_DISTANCE_FACTOR = 100;

        // Count could/should be configurable
        public const int EARTHQUAKE_COUNT_LIMIT = 10;

        // Url could/should be configurable
        public const string USGS_LATEST_REPORT_URL = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_month.csv";

        public const string DATE_FORMAT = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";
    }
}
