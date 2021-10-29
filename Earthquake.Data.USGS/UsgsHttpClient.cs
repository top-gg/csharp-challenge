using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Earthquake.Data.USGS.GeoJSON;

namespace Earthquake.Data.USGS
{
    public class UsgsHttpClient : IUsgsHttpClient, IDisposable
    {
        private readonly HttpClient _client;
        private bool _disposed;

        public UsgsHttpClient(string baseAddress)
        {
            _client = new HttpClient {BaseAddress = new Uri(baseAddress)};
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public async Task<GeoJson> FindByCoordinatesBetweenDateRangeAsync(double latitude, double longitude,
            DateTime startDate, DateTime endDate, double maxRadiusInKm)
        {
            return await _client.GetFromJsonAsync<GeoJson>(
                @$"query?format=geojson&starttime={startDate}&endtime={endDate}&latitude={latitude}&longitude={longitude}&maxradiuskm={maxRadiusInKm}&orderby=time");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _client.Dispose();

            _disposed = true;
        }
    }
}