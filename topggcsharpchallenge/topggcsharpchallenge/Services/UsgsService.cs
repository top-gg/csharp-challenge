using System.Net;

namespace topggcsharpchallenge.Services
{
    public class UsgsService : IUsgsService
    {
        public byte[] GetEarthquakeData()
        {
            string latestReportUrl = Constants.USGS_LATEST_REPORT_URL;

            using (var client = new WebClient())
            {
                return client.DownloadData(latestReportUrl);
            }
        }
    }
}
