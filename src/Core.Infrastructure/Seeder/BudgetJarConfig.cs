using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Core.Infrastructure.Seeder
{
    public class BudgetJarConfig : IEntityTypeConfiguration<BudgetJar>
    {
        public void Configure(EntityTypeBuilder<BudgetJar> builder)
        {
            var jars = new List<BudgetJar>() {
                new BudgetJar()
                {
                    Id = Guid.Parse("2f32317b-7ce2-469b-91fc-a277d300f667"),
                    UserId = Guid.Empty,
                    Name = "Necessities",
                    Percentage = 55,
                    IsDefault = true,
                    IconId = Guid.Parse("B0445780-DB7C-4D1E-9D42-3B125422C1A2"),
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("4adc7f4f-d3cd-4188-826c-410b729cfe8c"),
                    UserId = Guid.Empty,
                    Name = "Long Term Saving",
                    Percentage = 10,
                    IsDefault = true,
                    IconId = Guid.Parse("AA618108-0BAD-42E9-B80A-B8E904478B99"),
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("4ecd52ce-ba4d-45df-bd3b-ce7a412e118d"),
                    UserId = Guid.Empty,
                    Name = "Wants",
                    Percentage = 10,
                    IsDefault = true,
                    IconId = Guid.Parse("E0822B72-A427-445F-ACC0-5DC08C8C3929"),
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("7e7ad24e-cbf2-4a31-affe-cafa5c1a325c"),
                    UserId = Guid.Empty,
                    Name = "Education",
                    Percentage = 10,
                    IsDefault = true,
                    IconId = Guid.Parse("2613DB64-38D8-421C-9E73-C4FC2EB2C6DF"),
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("eee63caf-e26a-4265-817c-259d47e14aba"),
                    UserId = Guid.Empty,
                    Name = "Financial Freedom",
                    Percentage = 10,
                    IsDefault = true,
                    IconId = Guid.Parse("0A55E9F4-ED2A-4AE5-8249-2AA9368EFE88"),
                },
                new BudgetJar()
                {
                    Id = Guid.Parse("6B7C5AD3-82C5-4AFC-AD66-A2A895A4BF7B"),
                    UserId = Guid.Empty,
                    Name = "Others",
                    Percentage = 5,
                    IsDefault = true,
                    IconId = Guid.Parse("6B7C5AD3-82C5-4AFC-AD66-A2A895A4BF7B"),
                }
            };
            builder.HasData(jars);
        }
    }
}
