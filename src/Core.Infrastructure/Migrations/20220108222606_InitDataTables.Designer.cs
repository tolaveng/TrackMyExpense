﻿// <auto-generated />
using System;
using Core.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220108222606_InitDataTables")]
    partial class InitDataTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryExpense", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("integer");

                    b.Property<long>("ExpensesExpenseId")
                        .HasColumnType("bigint");

                    b.HasKey("CategoriesCategoryId", "ExpensesExpenseId");

                    b.HasIndex("ExpensesExpenseId");

                    b.ToTable("CategoryExpense");
                });

            modelBuilder.Entity("Core.Domain.Entities.Attachment", b =>
                {
                    b.Property<string>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<long?>("ExpenseId")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("AttachmentId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Core.Domain.Entities.BudgetJar", b =>
                {
                    b.Property<int>("BudgetJarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BudgetJarId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<bool>("Archived")
                        .HasColumnType("boolean");

                    b.Property<string>("IconName")
                        .HasColumnType("text");

                    b.Property<long?>("IncomeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Percentage")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("BudgetJarId");

                    b.HasIndex("IncomeId");

                    b.ToTable("BudgetJars");

                    b.HasData(
                        new
                        {
                            BudgetJarId = 1,
                            Amount = 0m,
                            Archived = false,
                            IsSystem = true,
                            Name = "Necessities",
                            Percentage = 55,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            BudgetJarId = 2,
                            Amount = 0m,
                            Archived = false,
                            IsSystem = true,
                            Name = "Long Term Saving",
                            Percentage = 10,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            BudgetJarId = 3,
                            Amount = 0m,
                            Archived = false,
                            IsSystem = true,
                            Name = "Wants",
                            Percentage = 10,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            BudgetJarId = 4,
                            Amount = 0m,
                            Archived = false,
                            IsSystem = true,
                            Name = "Education",
                            Percentage = 10,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            BudgetJarId = 5,
                            Amount = 0m,
                            Archived = false,
                            IsSystem = true,
                            Name = "Financial Freedom",
                            Percentage = 10,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            BudgetJarId = 6,
                            Amount = 0m,
                            Archived = false,
                            IsSystem = true,
                            Name = "Others",
                            Percentage = 5,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("Core.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<bool>("Archived")
                        .HasColumnType("boolean");

                    b.Property<string>("CategoryName")
                        .HasColumnType("text");

                    b.Property<string>("IconName")
                        .HasColumnType("text");

                    b.Property<int?>("RecurrentExpenseId")
                        .HasColumnType("integer");

                    b.HasKey("CategoryId");

                    b.HasIndex("RecurrentExpenseId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Domain.Entities.Expense", b =>
                {
                    b.Property<long>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ExpenseId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<bool>("Archived")
                        .HasColumnType("boolean");

                    b.Property<int>("BudgetJarId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsTaxable")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PaidDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Payee")
                        .HasColumnType("text");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<int?>("RecurrentExpenseId")
                        .HasColumnType("integer");

                    b.Property<string>("Ref")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ExpenseId");

                    b.HasIndex("BudgetJarId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Core.Domain.Entities.Income", b =>
                {
                    b.Property<long>("IncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IncomeId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("PeriodFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("PeriodTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("IncomeId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("Core.Domain.Entities.RecurrentExpense", b =>
                {
                    b.Property<int>("RecurrentExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RecurrentExpenseId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<bool>("Archived")
                        .HasColumnType("boolean");

                    b.Property<int>("BudgetJarId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsTaxable")
                        .HasColumnType("boolean");

                    b.Property<string>("Payee")
                        .HasColumnType("text");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<string>("Ref")
                        .HasColumnType("text");

                    b.Property<int>("Repeat")
                        .HasColumnType("integer");

                    b.Property<string>("RepeatDaily")
                        .HasColumnType("text");

                    b.Property<int>("RepeatDay")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RecurrentExpenseId");

                    b.HasIndex("BudgetJarId");

                    b.ToTable("RecurrentExpenses");
                });

            modelBuilder.Entity("Core.Domain.Entities.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubscriptionId"));

                    b.Property<bool>("Archived")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<int>("SubscriptionType")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ValidAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Core.Infrastructure.Database.Identity.AppIdentityRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("9b78ce40-633a-48b5-99e3-d1cc5c753fbe"),
                            ConcurrencyStamp = "c6a82930-9c6c-4aee-bff9-32041286315a",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("6a9ae0f3-285d-450b-96e5-413362fae4a6"),
                            ConcurrencyStamp = "0fa7f4f2-ee03-4bc8-86f1-9c5d54a02071",
                            Name = "user",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Core.Infrastructure.Database.Identity.AppIdentityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("CategoryExpense", b =>
                {
                    b.HasOne("Core.Domain.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.Expense", null)
                        .WithMany()
                        .HasForeignKey("ExpensesExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Entities.Attachment", b =>
                {
                    b.HasOne("Core.Domain.Entities.Expense", null)
                        .WithMany("Attachments")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Core.Domain.Entities.BudgetJar", b =>
                {
                    b.HasOne("Core.Domain.Entities.Income", null)
                        .WithMany("BudgetJars")
                        .HasForeignKey("IncomeId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Core.Domain.Entities.Category", b =>
                {
                    b.HasOne("Core.Domain.Entities.RecurrentExpense", null)
                        .WithMany("Categories")
                        .HasForeignKey("RecurrentExpenseId");
                });

            modelBuilder.Entity("Core.Domain.Entities.Expense", b =>
                {
                    b.HasOne("Core.Domain.Entities.BudgetJar", "BudgetJar")
                        .WithMany()
                        .HasForeignKey("BudgetJarId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("BudgetJar");
                });

            modelBuilder.Entity("Core.Domain.Entities.RecurrentExpense", b =>
                {
                    b.HasOne("Core.Domain.Entities.BudgetJar", "BudgetJar")
                        .WithMany()
                        .HasForeignKey("BudgetJarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BudgetJar");
                });

            modelBuilder.Entity("Core.Domain.Entities.Subscription", b =>
                {
                    b.HasOne("Core.Infrastructure.Database.Identity.AppIdentityUser", null)
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Core.Infrastructure.Database.Identity.AppIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Core.Infrastructure.Database.Identity.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Core.Infrastructure.Database.Identity.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Core.Infrastructure.Database.Identity.AppIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Infrastructure.Database.Identity.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Core.Infrastructure.Database.Identity.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Entities.Expense", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Core.Domain.Entities.Income", b =>
                {
                    b.Navigation("BudgetJars");
                });

            modelBuilder.Entity("Core.Domain.Entities.RecurrentExpense", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Core.Infrastructure.Database.Identity.AppIdentityUser", b =>
                {
                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}