using AutoMapper;
using Core.Application.Models;
using Core.Application.Providers.IProviders;
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
                .AfterMap<SetProfileImageWebUrl>()
                ;

            CreateMap<SysAttribute, SysAttributeDto>().ReverseMap();
            CreateMap<Currency, CurrencyDto>().ReverseMap();
            CreateMap<PageHtml, PageHtmlDto>().ReverseMap();
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
            CreateMap<BudgetJar, BudgetJarDto>().ReverseMap();
            CreateMap<IncomeBudgetJar, IncomeBudgetJarDto>().ReverseMap();
            CreateMap<ExpenseGroup, ExpenseGroupDto>().ReverseMap();
            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Attachment, AttachmentDto>().ReverseMap();

            CreateMap<Icon, IconDto>().AfterMap<SetIconWebUrl>().ReverseMap();
            //CreateMap<IconDto, Icon>();
        }
    }

    // Resolve Icon image url
    public class SetIconWebUrl : IMappingAction<Icon, IconDto>
    {
        private readonly IFileDirectoryProvider _fileDirectoryProvider;
        public SetIconWebUrl(IFileDirectoryProvider fileDirectoryProvider)
        {
            _fileDirectoryProvider = fileDirectoryProvider;
        }
        public void Process(Icon source, IconDto destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Path))
            {
                destination.IconUrl = string.Empty;
                return;
            }
            destination.IconUrl = _fileDirectoryProvider.GetIconUrl(source.IconType, source.Path);
        }
    }

    // Resove Profile Image
    public class SetProfileImageWebUrl : IMappingAction<AppUser, UserDto>
    {
        private readonly IFileDirectoryProvider _fileDirectoryProvider;
        public SetProfileImageWebUrl(IFileDirectoryProvider fileDirectoryProvider)
        {
            _fileDirectoryProvider = fileDirectoryProvider;
        }
        public void Process(AppUser source, UserDto destination, ResolutionContext context)
        {
            destination.ProfileImageUrl = _fileDirectoryProvider.GetProfileImageUrl(source.ProfileImage, string.Empty);
            destination.ProfileImageThumbnailUrl = _fileDirectoryProvider.GetProfileImageThumbnailUrl(source.ProfileImage, string.Empty);
        }
    }
}