using AutoMapper;
using WebOdev.Models;

namespace WebOdev.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RandevuModel, RandevuModelDto>().ReverseMap();
        }
    }
}
