using AutoMapper;

namespace NZWalks.api.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile() 
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();
        }
    }
}
