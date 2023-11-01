using API.Entities;
using AutoMapper;

namespace API.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, AppUser>();
        }
    }
}
