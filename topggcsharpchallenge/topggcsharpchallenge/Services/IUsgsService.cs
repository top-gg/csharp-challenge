using System.Threading.Tasks;

namespace topggcsharpchallenge.Services
{
    public interface IUsgsService
    {
        Task<byte[]> GetEarthquakeData();
    }
}
