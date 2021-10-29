using AutoMapper;
using Earthquake.Data.CSV;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Earthquake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakeCsvController : EarthquakeBaseController
    {
        public EarthquakeCsvController(ILogger<EarthquakeCsvController> logger, ICsvDataContext csvDataContext, IMapper mapper, IDistributedCache cache) : base(logger, csvDataContext, mapper, cache)
        {
        }
    }
}