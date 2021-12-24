using AutoMapper;
using Core.Application.Models;
using Core.Domain.Entities;

namespace Core.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<UserDto, AppUser>();
        }
    }
}