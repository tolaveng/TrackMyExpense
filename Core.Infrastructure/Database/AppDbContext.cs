﻿using Core.Infrastructure.Database.Config;
using Core.Infrastructure.Database.Schema;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database
{
    // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IAppDbContextConfigurator _configurator;

        public AppDbContext(IAppDbContextConfigurator configurator, IWebHostEnvironment environment)
        {
            _environment = environment;
            _configurator = configurator;
        }

        // https://www.npgsql.org/efcore/index.html
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _configurator.Configure(optionsBuilder);
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<RecurrentExpense> RecurrentExpenses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Not use, Table per type (TPT), use the default one to avoid performance issue
            // https://docs.microsoft.com/en-us/ef/core/modeling/inheritance
            // modelBuilder.Entity<RecurrentExpense>().ToTable("RecurrentExpense");

            // Change the table names of .Net Core Identity, so that they look nicer.
            modelBuilder.Entity<AppUser>(entity => { entity.ToTable("Users"); });
            modelBuilder.Entity<AppRole>(entity => { entity.ToTable("Roles"); });
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });

            // Relationship: https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
            modelBuilder.Entity<Expense>()
                .HasOne(x => x.Category)
                .WithMany()                         // Without navigation property: WithMany(x => x.Expenses)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.Expenses)
                .WithOne()
                .HasForeignKey(x => x.UserId);

            // Seed Default Data
            modelBuilder.ApplyConfiguration(new AppRoleConfig());
        }
    }
}
