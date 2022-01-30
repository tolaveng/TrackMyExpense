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
    public class BudgetJarDefaultConfig : IEntityTypeConfiguration<BudgetJar>
    {
        public void Configure(EntityTypeBuilder<BudgetJar> builder)
        {
            var jars = new List<BudgetJar>() {
                new BudgetJar()
                {
                    Id = Guid.Parse("2f32317b-7ce2-469b-91fc-a277d300f667"),
                    Name = "Necessities",
                    Percentage = 55,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("4adc7f4f-d3cd-4188-826c-410b729cfe8c"),
                    Name = "Long Term Saving",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("4ecd52ce-ba4d-45df-bd3b-ce7a412e118d"),
                    Name = "Wants",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("7e7ad24e-cbf2-4a31-affe-cafa5c1a325c"),
                    Name = "Education",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("eee63caf-e26a-4265-817c-259d47e14aba"),
                    Name = "Financial Freedom",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("f20c473d-1fbf-4666-a88a-2f77594e1ea4"),
                    Name = "Others",
                    Percentage = 5,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                }
            };
            builder.HasData(jars);
        }
    }
}
