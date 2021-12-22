using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Database.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
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
