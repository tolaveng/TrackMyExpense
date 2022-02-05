using AutoMapper;
using Core.Application.Models;
using Core.Domain.Entities;

namespace Core.Application.Mapper
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<UserDto, AppUser>();
            CreateMap<AppUser, UserDto>()
                .ForMember(x => x.Password, x => x.MapFrom(z => string.Empty))
                ;

            CreateMap<SysAttribute, SysAttributeDto>().ReverseMap();
            CreateMap<PageHtml, PageHtmlDto>().ReverseMap();
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
            CreateMap<BudgetJar, BudgetJarDto>().ReverseMap();
        }
    }
}