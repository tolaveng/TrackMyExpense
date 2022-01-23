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
                    Id = Guid.NewGuid(),
                    Name = "Necessities",
                    Percentage = 55,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.NewGuid(),
                    Name = "Long Term Saving",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.NewGuid(),
                    Name = "Wants",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.NewGuid(),
                    Name = "Education",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.NewGuid(),
                    Name = "Financial Freedom",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    Id = Guid.NewGuid(),
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
