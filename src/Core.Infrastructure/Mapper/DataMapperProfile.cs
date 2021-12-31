using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Database.Identity;

namespace Core.Infrastructure.Mapper
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<AppUser, AppIdentityUser>()
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.Username))
                ;

            CreateMap<AppIdentityUser, AppUser>()
                .ForMember(x => x.Username, x => x.MapFrom(z => z.UserName))
                .ForMember(x => x.Password, x => x.MapFrom(z => string.Empty))
                ;
        }
    }
}
