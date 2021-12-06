using Core.Domain.Enums;
using Core.Infrastructure.Database.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.Config
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Seed Default Base Role
            var developerRole = new AppRole()
            {
                Id = Guid.Parse("9B78CE40-633A-48B5-99E3-D1CC5C753FBE"),
                Name = UserBaseRole.Developer.ToString(),
                NormalizedName = UserBaseRole.Developer.ToString().ToUpper()
            };

            var supportRole = new AppRole()
            {
                Id = Guid.Parse("9F50E6A8-E115-489B-8B4B-DBC70B2FBBFC"),
                Name = UserBaseRole.Support.ToString(),
                NormalizedName = UserBaseRole.Support.ToString().ToUpper()
            };

            var userRole = new AppRole()
            {
                Id = Guid.Parse("6A9AE0F3-285D-450B-96E5-413362FAE4A6"),
                Name = UserBaseRole.User.ToString(),
                NormalizedName = UserBaseRole.User.ToString().ToUpper()
            };

            builder.HasData(
                developerRole,
                supportRole,
                userRole
            );
        }
    }
}
