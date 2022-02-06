using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Seeder
{
    public class IconConfig : IEntityTypeConfiguration<Icon>
    {
        public void Configure(EntityTypeBuilder<Icon> builder)
        {
            var icons = new List<Icon>()
            {
                new Icon()
                {
                    Id = Guid.Parse("B0445780-DB7C-4D1E-9D42-3B125422C1A2"),
                    Name = "Necessities",
                    Path = "/assets/icons/necessities.png",
                    IconType = IconType.Asset,
                    IconCategory = IconCategory.BudgetJar,
                    Ordinal = 1,
                },
                new Icon()
                {
                    Id = Guid.Parse("AA618108-0BAD-42E9-B80A-B8E904478B99"),
                    Name = "Long Term Saving",
                    Path = "/assets/icons/long-term-saving.png",
                    IconType = IconType.Asset,
                    IconCategory = IconCategory.BudgetJar,
                    Ordinal = 2,
                },
                new Icon()
                {
                    Id = Guid.Parse("E0822B72-A427-445F-ACC0-5DC08C8C3929"),
                    Name = "Wants",
                    Path = "/assets/icons/wants.png",
                    IconType = IconType.Asset,
                    IconCategory = IconCategory.BudgetJar,
                    Ordinal = 3,
                },
                new Icon()
                {
                    Id = Guid.Parse("2613DB64-38D8-421C-9E73-C4FC2EB2C6DF"),
                    Name = "Education",
                    Path = "/assets/icons/education.png",
                    IconType = IconType.Asset,
                    IconCategory = IconCategory.BudgetJar,
                    Ordinal = 4,
                },
                new Icon()
                {
                    Id = Guid.Parse("0A55E9F4-ED2A-4AE5-8249-2AA9368EFE88"),
                    Name = "Financial Freedom",
                    Path = "/assets/icons/financial-freedom.png",
                    IconType = IconType.Asset,
                    IconCategory = IconCategory.BudgetJar,
                    Ordinal = 5,
                },
                new Icon()
                {
                    Id = Guid.Parse("C4D34C7E-3AB4-46F7-9050-5574D6B312BC"),
                    Name = "Others",
                    Path = "/assets/icons/others.png",
                    IconType = IconType.Asset,
                    IconCategory = IconCategory.BudgetJar,
                    Ordinal = 6,
                }
            };
            builder.HasData(icons);
        }
    }
}
