using Core.Domain.Entities;
using Core.Infrastructure.Configurations;
using Core.Infrastructure.Database.Identity;
using Core.Infrastructure.Seeder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Infrastructure.Database
{
    // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0

    public class AppDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, Guid>
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

        public DbSet<SysAttribute> SysAttributes { get; set; }
        public DbSet<PageHtml> PageHtmls { get; set; }
        public DbSet<BudgetJar> BudgetJars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<RecurrentExpense> RecurrentExpenses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Not use, Table per type (TPT), use the default one to avoid performance issue
            // https://docs.microsoft.com/en-us/ef/core/modeling/inheritance
            // modelBuilder.Entity<RecurrentExpense>().ToTable("RecurrentExpense");

            // Change the table names of .Net Core Identity, so that they look nicer.
            modelBuilder.Entity<AppIdentityUser>(entity => { entity.ToTable("Users"); });
            modelBuilder.Entity<AppIdentityRole>(entity => { entity.ToTable("Roles"); });
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });


            // Relationship:
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
            modelBuilder.Entity<Expense>()
                .HasMany(x => x.Categories)
                .WithMany(x => x.Expenses);
            modelBuilder.Entity<Expense>().HasMany(x => x.Attachments).WithOne().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Income>().HasMany(x => x.BudgetJars).WithOne().HasForeignKey(x => x.IncomeId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Expense>().HasOne(x => x.BudgetJar).WithMany().HasForeignKey(x => x.BudgetJarId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<AppIdentityUser>().HasMany(x => x.Subscriptions).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<RecurrentExpense>().HasOne(x => x.BudgetJar).WithMany().HasForeignKey(x => x.BudgetJarId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<RecurrentExpense>().HasMany(x => x.Categories).WithOne().OnDelete(DeleteBehavior.SetNull);

            // Seed Default Data
            modelBuilder.ApplyConfiguration(new AppRoleConfig());
            modelBuilder.ApplyConfiguration(new BudgetJarDefaultConfig());
        }
    }
}
