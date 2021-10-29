using AutoMapper;
using Earthquake.Data.USGS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Earthquake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakeUsgsController : EarthquakeBaseController
    {
        public EarthquakeUsgsController(ILogger<EarthquakeUsgsController> logger, IUsgsDataContext usgsDataContext, IMapper mapper, IDistributedCache cache) : base(logger, usgsDataContext,
            mapper, cache)
        {
        }
    }
}