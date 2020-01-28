using AutoMapper;
using CMMS.DTOs;
using CMMS.Models;

namespace CMMS.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();
            CreateMap<Division, DivisionDto>();
            CreateMap<ExclusionType, ExclusionTypeDto>();
            CreateMap<Entity, EntityDto>();
            CreateMap<EntityDto, Entity>();
        }
    }
}
