using Core.Domain.Constants;
using Core.Infrastructure.Database.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Seeder
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppIdentityRole>
    {
        public void Configure(EntityTypeBuilder<AppIdentityRole> builder)
        {
            // Seed Default Base Role
            var adminRole = new AppIdentityRole()
            {
                Id = Guid.Parse("9B78CE40-633A-48B5-99E3-D1CC5C753FBE"),
                Name = UserBaseRole.Admin,
                NormalizedName = UserBaseRole.Admin.ToUpper()
            };

            
            var userRole = new AppIdentityRole()
            {
                Id = Guid.Parse("6A9AE0F3-285D-450B-96E5-413362FAE4A6"),
                Name = UserBaseRole.User,
                NormalizedName = UserBaseRole.User.ToUpper()
            };

            builder.HasData(adminRole, userRole);
        }
    }
}
