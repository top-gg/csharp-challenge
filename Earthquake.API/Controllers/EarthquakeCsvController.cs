using AutoMapper;
using Earthquake.Data.CSV;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Earthquake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakeCsvController : EarthquakeBaseController
    {
        public EarthquakeCsvController(ICsvDataContext csvDataContext, IMapper mapper, IDistributedCache cache) : base(csvDataContext, mapper, cache)
        {
        }
    }
}