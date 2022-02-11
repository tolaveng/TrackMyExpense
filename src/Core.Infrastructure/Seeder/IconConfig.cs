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
                    IsSystem = true,
                },
                new Icon()
                {
                    Id = Guid.Parse("AA618108-0BAD-42E9-B80A-B8E904478B99"),
                    Name = "Long Term Saving",
                    Path = "/assets/icons/long-term-saving.png",
                    IconType = IconType.Asset,
                    IsSystem = true,
                },
                new Icon()
                {
                    Id = Guid.Parse("E0822B72-A427-445F-ACC0-5DC08C8C3929"),
                    Name = "Wants",
                    Path = "/assets/icons/wants.png",
                    IconType = IconType.Asset,
                    IsSystem = true,
                },
                new Icon()
                {
                    Id = Guid.Parse("2613DB64-38D8-421C-9E73-C4FC2EB2C6DF"),
                    Name = "Education",
                    Path = "/assets/icons/education.png",
                    IconType = IconType.Asset,
                    IsSystem = true,
                },
                new Icon()
                {
                    Id = Guid.Parse("0A55E9F4-ED2A-4AE5-8249-2AA9368EFE88"),
                    Name = "Financial Freedom",
                    Path = "/assets/icons/financial-freedom.png",
                    IconType = IconType.Asset,
                    IsSystem = true,
                }
            };

            // Categories
            icons.Add(new Icon()
            {
                Id = Guid.Parse("8D5D29E8-DD5A-4971-B1B0-A50A4BF4C73C"),
                Name = "Grocery",
                Path = "/assets/icons/grocery.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("D6552F54-0C69-431E-9907-34147DD2C029"),
                Name = "Clothes",
                Path = "/assets/icons/clothes.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("6370331D-D544-41B8-AD67-A0CFC0756975"),
                Name = "Eat Out",
                Path = "/assets/icons/eat-out.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("EE676E1C-6A69-41AE-8B3B-B2DAC73B9751"),
                Name = "Transport",
                Path = "/assets/icons/transport.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("5B311B51-D25D-459E-9D1C-B4E1B199EDAB"),
                Name = "Utilities",
                Path = "/assets/icons/utilities.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("6550B905-6763-4E97-9038-50EC50D68853"),
                Name = "Medicines",
                Path = "/assets/icons/medicine.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("84478CA2-0873-4DAC-A279-6CC2BD20B22C"),
                Name = "Investment",
                Path = "/assets/icons/investment.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("F58FB384-E35E-4B15-BBFD-428642178FBC"),
                Name = "Family",
                Path = "/assets/icons/family.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("8EC17BDA-749C-4089-8511-BCE5CEA403AA"),
                Name = "Furniture",
                Path = "/assets/icons/furniture.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("68DC6416-6D0B-4E63-B9FF-42C68D3B96F4"),
                Name = "Petro",
                Path = "/assets/icons/petro.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            icons.Add(new Icon()
            {
                Id = Guid.Parse("E54521EC-1D1C-41F0-8353-BC3B62485F25"),
                Name = "Car",
                Path = "/assets/icons/car.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("F348AA99-C779-4B7C-A8BD-D96502EE2692"),
                Name = "Insurance",
                Path = "/assets/icons/insurance.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });
            
            icons.Add(new Icon()
            {
                Id = Guid.Parse("BC0A7DB7-2EED-415F-9076-08DAB2E93933"),
                Name = "Toys",
                Path = "/assets/icons/toy.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("1137EE8A-9A4E-4625-BE8E-0612B6A20BC4"),
                Name = "Donate",
                Path = "/assets/icons/donate.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("EA2978EF-F900-4B01-B0F0-90AFE13E0A55"),
                Name = "Households",
                Path = "/assets/icons/household-items.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("44A67DFD-6A7D-4DBE-B8CF-82D25DB8DBBC"),
                Name = "Gardens",
                Path = "/assets/icons/garden.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            // Custom
            icons.Add(new Icon()
            {
                Id = Guid.Parse("05269476-13C6-451D-9259-5BCF2480EF1E"),
                Name = "Baby",
                Path = "/assets/icons/baby.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("4006BE63-25D4-4425-9702-2BA10DC721BB"),
                Name = "Travel",
                Path = "/assets/icons/travel.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("52AFBF57-54D3-4FE6-A680-40375679B5FF"),
                Name = "Holiday",
                Path = "/assets/icons/holiday.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("9392F6A9-F9AA-4D9E-9AB2-8D89BA455EA9"),
                Name = "Vaccation",
                Path = "/assets/icons/vaccation.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("56F4421E-10B6-4940-B594-01DE569FDE12"),
                Name = "Saving",
                Path = "/assets/icons/Saving.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("79CF43BD-C6D4-4834-B677-52C9B359473E"),
                Name = "Honey Moon",
                Path = "/assets/icons/honeymoon.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("4BB8D462-187A-4370-BB1E-E0FB99D233E0"),
                Name = "Bakery",
                Path = "/assets/icons/bakery.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("476501C2-92DF-4E9F-A863-DFD93DD937C0"),
                Name = "Marry",
                Path = "/assets/icons/marry.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("2F67A4C3-7186-47DA-9EAD-407BE93A675E"),
                Name = "Gas",
                Path = "/assets/icons/gas.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("B028A67A-B2E3-44A7-953F-DE1C0959262B"),
                Name = "Electricity",
                Path = "/assets/icons/electricity.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("F0FFAF46-DDE9-47E0-A9F1-D64785D4DFE7"),
                Name = "Gift",
                Path = "/assets/icons/gift.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("E1FC64F8-3154-4880-9732-37B5615592BD"),
                Name = "Shirt",
                Path = "/assets/icons/shirt.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("8470C1BC-D85C-4F94-9133-B1BA2945F7F2"),
                Name = "Jeans",
                Path = "/assets/icons/jeans.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("EFF825BC-673F-43E3-A422-1DF323BF274D"),
                Name = "Tools",
                Path = "/assets/icons/tools.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            icons.Add(new Icon()
            {
                Id = Guid.Parse("6B7C5AD3-82C5-4AFC-AD66-A2A895A4BF7B"),
                Name = "Others",
                Path = "/assets/icons/others.png",
                IconType = IconType.Asset,
                IsSystem = true,
            });

            builder.HasData(icons);
        }
    }
}
