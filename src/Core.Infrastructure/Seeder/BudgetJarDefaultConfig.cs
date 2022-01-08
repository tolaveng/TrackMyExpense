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
                    BudgetJarId = 1,
                    Name = "Necessities",
                    Percentage = 55,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    BudgetJarId = 2,
                    Name = "Long Term Saving",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    BudgetJarId = 3,
                    Name = "Wants",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    BudgetJarId = 4,
                    Name = "Education",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    BudgetJarId = 5,
                    Name = "Financial Freedom",
                    Percentage = 10,
                    Amount = 0,
                    IncomeId = null,
                    IsSystem = true,
                },
                new BudgetJar()
                {
                    BudgetJarId = 6,
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
