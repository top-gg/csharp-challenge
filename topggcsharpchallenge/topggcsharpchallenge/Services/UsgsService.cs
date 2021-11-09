using System.Net;

namespace topggcsharpchallenge.Services
{
    class UsgsService : IUsgsService
    {
        public string GetEarthquakeData()
        {
            string latestReportUrl = Constants.USGS_LATEST_REPORT_URL;

            using (var client = new WebClient())
            {
                return client.DownloadString(latestReportUrl);
            }
        }
    }
}
