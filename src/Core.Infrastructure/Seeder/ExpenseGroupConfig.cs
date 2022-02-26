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
    public class ExpenseGroupConfig : IEntityTypeConfiguration<ExpenseGroup>
    {
        public void Configure(EntityTypeBuilder<ExpenseGroup> builder)
        {
            var expenseGroups = new List<ExpenseGroup>()
            {
                new ExpenseGroup()
                {
                    Id = Guid.Parse("8D5D29E8-DD5A-4971-B1B0-A50A4BF4C73C"),
                    Name = "Grocery",
                    IsSystem = true,
                    IconId = Guid.Parse("8D5D29E8-DD5A-4971-B1B0-A50A4BF4C73C"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("D6552F54-0C69-431E-9907-34147DD2C029"),
                    Name = "Clothes",
                    IsSystem = true,
                    IconId = Guid.Parse("D6552F54-0C69-431E-9907-34147DD2C029"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("2613DB64-38D8-421C-9E73-C4FC2EB2C6DF"),
                    Name = "Education",
                    IsSystem = true,
                    IconId = Guid.Parse("2613DB64-38D8-421C-9E73-C4FC2EB2C6DF"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("6370331D-D544-41B8-AD67-A0CFC0756975"),
                    Name = "Eat Out",
                    IsSystem = true,
                    IconId = Guid.Parse("6370331D-D544-41B8-AD67-A0CFC0756975"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("EE676E1C-6A69-41AE-8B3B-B2DAC73B9751"),
                    Name = "Transport",
                    IsSystem = true,
                    IconId = Guid.Parse("EE676E1C-6A69-41AE-8B3B-B2DAC73B9751"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("5B311B51-D25D-459E-9D1C-B4E1B199EDAB"),
                    Name = "Utilities",
                    IsSystem = true,
                    IconId = Guid.Parse("5B311B51-D25D-459E-9D1C-B4E1B199EDAB"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("6550B905-6763-4E97-9038-50EC50D68853"),
                    Name = "Medicines",
                    IsSystem = true,
                    IconId = Guid.Parse("6550B905-6763-4E97-9038-50EC50D68853"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("84478CA2-0873-4DAC-A279-6CC2BD20B22C"),
                    Name = "Investment",
                    IsSystem = true,
                    IconId = Guid.Parse("84478CA2-0873-4DAC-A279-6CC2BD20B22C"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("F58FB384-E35E-4B15-BBFD-428642178FBC"),
                    Name = "Family",
                    IsSystem = true,
                    IconId = Guid.Parse("F58FB384-E35E-4B15-BBFD-428642178FBC"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("8EC17BDA-749C-4089-8511-BCE5CEA403AA"),
                    Name = "Furniture",
                    IsSystem = true,
                    IconId = Guid.Parse("8EC17BDA-749C-4089-8511-BCE5CEA403AA"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("68DC6416-6D0B-4E63-B9FF-42C68D3B96F4"),
                    Name = "Petro",
                    IsSystem = true,
                    IconId = Guid.Parse("68DC6416-6D0B-4E63-B9FF-42C68D3B96F4"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("E54521EC-1D1C-41F0-8353-BC3B62485F25"),
                    Name = "Car",
                    IsSystem = true,
                    IconId = Guid.Parse("E54521EC-1D1C-41F0-8353-BC3B62485F25"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("F348AA99-C779-4B7C-A8BD-D96502EE2692"),
                    Name = "Insurance",
                    IsSystem = true,
                    IconId = Guid.Parse("F348AA99-C779-4B7C-A8BD-D96502EE2692"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("BC0A7DB7-2EED-415F-9076-08DAB2E93933"),
                    Name = "Toys",
                    IsSystem = true,
                    IconId = Guid.Parse("BC0A7DB7-2EED-415F-9076-08DAB2E93933"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("1137EE8A-9A4E-4625-BE8E-0612B6A20BC4"),
                    Name = "Donate",
                    IsSystem = true,
                    IconId = Guid.Parse("1137EE8A-9A4E-4625-BE8E-0612B6A20BC4"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("EA2978EF-F900-4B01-B0F0-90AFE13E0A55"),
                    Name = "Households",
                    IsSystem = true,
                    IconId = Guid.Parse("EA2978EF-F900-4B01-B0F0-90AFE13E0A55"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("44A67DFD-6A7D-4DBE-B8CF-82D25DB8DBBC"),
                    Name = "Gardens",
                    IsSystem = true,
                    IconId = Guid.Parse("44A67DFD-6A7D-4DBE-B8CF-82D25DB8DBBC"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("70D3E625-CF3B-4ECA-A773-F1FB5E340C64"),
                    Name = "Sports",
                    IsSystem = true,
                    IconId = Guid.Parse("70D3E625-CF3B-4ECA-A773-F1FB5E340C64"),
                },
                new ExpenseGroup()
                {
                    Id = Guid.Parse("6B7C5AD3-82C5-4AFC-AD66-A2A895A4BF7B"),
                    Name = "Others",
                    IsSystem = true,
                    IconId = Guid.Parse("6B7C5AD3-82C5-4AFC-AD66-A2A895A4BF7B"),
                }
            };
            builder.HasData(expenseGroups);
        }
    }
}
