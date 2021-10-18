using AutoMapper;
using Earthquake.Data.CSV;
using Microsoft.AspNetCore.Mvc;

namespace Earthquake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EarthquakeCsvController : EarthquakeBaseController
    {
        public EarthquakeCsvController(ICsvDataContext csvDataContext, IMapper mapper) : base(csvDataContext, mapper)
        {
        }
    }
}