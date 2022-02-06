using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Seeder
{
    public class BudgetJarTemplateConfig : IEntityTypeConfiguration<BudgetJarTemplate>
    {
        public void Configure(EntityTypeBuilder<BudgetJarTemplate> builder)
        {
            var jars = new List<BudgetJarTemplate>() {
                new BudgetJarTemplate()
                {
                    Id = Guid.Parse("2f32317b-7ce2-469b-91fc-a277d300f667"),
                    Name = "Necessities",
                    Percentage = 55,
                    IsSystem = true,
                    IconId = Guid.Parse("B0445780-DB7C-4D1E-9D42-3B125422C1A2"),
                },
                new BudgetJarTemplate()
                {
                    Id = Guid.Parse("4adc7f4f-d3cd-4188-826c-410b729cfe8c"),
                    Name = "Long Term Saving",
                    Percentage = 10,
                    IsSystem = true,
                    IconId = Guid.Parse("AA618108-0BAD-42E9-B80A-B8E904478B99"),
                },
                new BudgetJarTemplate()
                {
                    Id = Guid.Parse("4ecd52ce-ba4d-45df-bd3b-ce7a412e118d"),
                    Name = "Wants",
                    Percentage = 10,
                    IsSystem = true,
                    IconId = Guid.Parse("E0822B72-A427-445F-ACC0-5DC08C8C3929"),
                },
                new BudgetJarTemplate()
                {
                    Id = Guid.Parse("7e7ad24e-cbf2-4a31-affe-cafa5c1a325c"),
                    Name = "Education",
                    Percentage = 10,
                    IsSystem = true,
                    IconId = Guid.Parse("2613DB64-38D8-421C-9E73-C4FC2EB2C6DF"),
                },
                new BudgetJarTemplate()
                {
                    Id = Guid.Parse("eee63caf-e26a-4265-817c-259d47e14aba"),
                    Name = "Financial Freedom",
                    Percentage = 10,
                    IsSystem = true,
                    IconId = Guid.Parse("0A55E9F4-ED2A-4AE5-8249-2AA9368EFE88"),
                },
                new BudgetJarTemplate()
                {
                    Id = Guid.Parse("f20c473d-1fbf-4666-a88a-2f77594e1ea4"),
                    Name = "Others",
                    Percentage = 5,
                    IsSystem = true,
                    IconId = Guid.Parse("C4D34C7E-3AB4-46F7-9050-5574D6B312BC"),
                }
            };
            builder.HasData(jars);
        }
    }
}
