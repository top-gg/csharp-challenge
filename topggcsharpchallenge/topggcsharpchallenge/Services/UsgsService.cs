using System.Net;
using System.Threading.Tasks;

namespace topggcsharpchallenge.Services
{
    public class UsgsService : IUsgsService
    {
        public async Task<byte[]> GetEarthquakeData()
        {
            string latestReportUrl = Constants.USGS_LATEST_REPORT_URL;

            using (var client = new WebClient())
            {
                return await client.DownloadDataTaskAsync(new System.Uri(latestReportUrl));
            }
        }
    }
}
