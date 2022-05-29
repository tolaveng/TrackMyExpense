using Core.Domain.Entities;
using Core.Infrastructure.Database.Identity;
using Core.Infrastructure.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Infrastructure.Database
{
    // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0

    public class AppDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<SysAttribute> SysAttributes { get; set; }
        public DbSet<PageHtml> PageHtmls { get; set; }
        public DbSet<BudgetJar> BudgetJars { get; set; }
        public DbSet<IncomeBudgetJar> IncomeBudgetJars { get; set; }
        public DbSet<ExpenseGroup> ExpenseGroups { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<RecurrentExpense> RecurrentExpenses { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Attachment> Attachements { get; set; }

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
            modelBuilder.Entity<Expense>().HasMany(x => x.Attachments).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Expense>().HasOne(x => x.ExpenseGroup).WithMany().HasForeignKey(x => x.ExpenseGroupId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Expense>().HasOne(x => x.BudgetJar).WithMany().HasForeignKey(x => x.BudgetJarId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Expense>().HasIndex(x => x.PaidDate);
            modelBuilder.Entity<IncomeBudgetJar>().HasKey(x => new { x.IncomeId, x.BudgetJarId });
            modelBuilder.Entity<IncomeBudgetJar>().HasOne(x => x.Income).WithMany(x => x.IncomeBudgetJars).HasForeignKey(x => x.IncomeId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<IncomeBudgetJar>().HasOne(x => x.BudgetJar).WithMany().HasForeignKey(x => x.BudgetJarId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AppIdentityUser>().HasMany(x => x.Subscriptions).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RecurrentExpense>().HasOne(x => x.BudgetJar).WithMany().HasForeignKey(x => x.BudgetJarId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RecurrentExpense>().HasOne(x => x.ExpenseGroup).WithMany().HasForeignKey(x => x.ExpenseGroupId).OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<ExpenseGroup>().HasOne(x => x.Icon).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BudgetJar>().HasOne(x => x.Icon).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Icon>().HasIndex(x => x.Name).IsUnique();
            

            // Seed Default Data
            modelBuilder.ApplyConfiguration(new AppRoleConfig());
            modelBuilder.ApplyConfiguration(new IconConfig());
            modelBuilder.ApplyConfiguration(new BudgetJarConfig());
            modelBuilder.ApplyConfiguration(new ExpenseGroupConfig());
            // Deprecated
            //modelBuilder.ApplyConfiguration(new CurrencyConfig());
        }
    }
}
