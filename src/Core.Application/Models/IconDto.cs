using Core.Domain.Enums;
using FluentValidation;

namespace Core.Application.Models
{
    public class IconDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IconType IconType { get; set; }
        public bool IsHidden { get; set; }

        public string IconUrl { get; set; } 
    }

    public class IconValidator : AbstractValidator<IconDto>
    {
        public IconValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Path).NotEmpty().When(x => x.IconType != IconType.Upload).MaximumLength(256);
        }
    }
}
