using AutoMapper;
using Earthquake.DTO;
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