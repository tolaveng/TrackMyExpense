using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.Models
{
    public class CategoryDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        public bool IsSystem { get; set; }

        public Guid IconId { get; set; }
        public IconDto? Icon { get; set; }

        public bool Archived { get; set; }

        public static CategoryDto Clone(CategoryDto categoryDto)
        {
            return new CategoryDto() {
                Id = categoryDto.Id,
                UserId = categoryDto.UserId,
                Name = categoryDto.Name,
                IsSystem = categoryDto.IsSystem,
                IconId = categoryDto.IconId,
                Icon = categoryDto.Icon,
            };
        }
    }

    public class CategoryValidator : BasicValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
