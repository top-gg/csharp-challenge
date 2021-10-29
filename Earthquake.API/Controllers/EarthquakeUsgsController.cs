using AutoMapper;
using Earthquake.Data.USGS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Earthquake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakeUsgsController : EarthquakeBaseController
    {
        public EarthquakeUsgsController(IUsgsDataContext usgsDataContext, IMapper mapper, IDistributedCache cache) : base(usgsDataContext,
            mapper, cache)
        {
        }
    }
}