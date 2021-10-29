using AutoMapper;
using Earthquake.Data.USGS;
using Microsoft.AspNetCore.Mvc;

namespace Earthquake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakeUsgsController : EarthquakeBaseController
    {
        public EarthquakeUsgsController(IUsgsDataContext usgsDataContext, IMapper mapper) : base(usgsDataContext,
            mapper)
        {
        }
    }
}