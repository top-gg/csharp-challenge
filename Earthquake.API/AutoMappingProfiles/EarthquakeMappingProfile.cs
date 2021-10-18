using Earthquake.DTO;
using AutoMapper;
using Earthquake.Entities;

namespace Earthquake.API.AutoMappingProfiles
{
    public class EarthquakeMappingProfile : Profile
    {
        public EarthquakeMappingProfile()
        {
            CreateMap<EarthquakeEntity, EarthquakeDto>();
        }
    }
}
