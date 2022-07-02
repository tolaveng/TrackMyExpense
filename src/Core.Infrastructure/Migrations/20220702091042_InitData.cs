using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Infrastructure.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    UnicodeDecimal = table.Column<string>(type: "text", nullable: true),
                    UnicodeHex = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    IconType = table.Column<int>(type: "integer", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageHtmls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageHtmls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    TimeZone = table.Column<string>(type: "text", nullable: true),
                    CultureInfo = table.Column<string>(type: "text", nullable: true),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetJars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Percentage = table.Column<float>(type: "real", nullable: false),
                    IconId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false),
                    TotalBalance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetJars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetJars_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IconId = table.Column<Guid>(type: "uuid", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionType = table.Column<int>(type: "integer", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaidGateway = table.Column<string>(type: "text", nullable: true),
                    PaidRef = table.Column<string>(type: "text", nullable: true),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    PaymentCycle = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Begin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCanceled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeBudgetJars",
                columns: table => new
                {
                    IncomeId = table.Column<Guid>(type: "uuid", nullable: false),
                    BudgetJarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Percentage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeBudgetJars", x => new { x.IncomeId, x.BudgetJarId });
                    table.ForeignKey(
                        name: "FK_IncomeBudgetJars_BudgetJars_BudgetJarId",
                        column: x => x.BudgetJarId,
                        principalTable: "BudgetJars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeBudgetJars_Incomes_IncomeId",
                        column: x => x.IncomeId,
                        principalTable: "Incomes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsTaxable = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    BudgetJarId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecurrentExpenseId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_BudgetJars_BudgetJarId",
                        column: x => x.BudgetJarId,
                        principalTable: "BudgetJars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecurrentExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Ref = table.Column<string>(type: "text", nullable: true),
                    Payee = table.Column<string>(type: "text", nullable: true),
                    IsTaxable = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    BudgetJarId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Repeat = table.Column<int>(type: "integer", nullable: false),
                    RepeatDay = table.Column<int>(type: "integer", nullable: false),
                    RepeatDaily = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrentExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurrentExpenses_BudgetJars_BudgetJarId",
                        column: x => x.BudgetJarId,
                        principalTable: "BudgetJars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecurrentExpenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachements_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "Id", "Archived", "IconType", "IsHidden", "Name", "Path" },
                values: new object[,]
                {
                    { new Guid("05269476-13c6-451d-9259-5bcf2480ef1e"), false, 0, false, "Baby", "/assets/icons/baby.png" },
                    { new Guid("0a55e9f4-ed2a-4ae5-8249-2aa9368efe88"), false, 0, false, "Financial Freedom", "/assets/icons/financial-freedom.png" },
                    { new Guid("1137ee8a-9a4e-4625-be8e-0612b6a20bc4"), false, 0, false, "Donate", "/assets/icons/donate.png" },
                    { new Guid("2613db64-38d8-421c-9e73-c4fc2eb2c6df"), false, 0, false, "Education", "/assets/icons/education.png" },
                    { new Guid("2f67a4c3-7186-47da-9ead-407be93a675e"), false, 0, false, "Gas", "/assets/icons/gas.png" },
                    { new Guid("4006be63-25d4-4425-9702-2ba10dc721bb"), false, 0, false, "Travel", "/assets/icons/travel.png" },
                    { new Guid("44a67dfd-6a7d-4dbe-b8cf-82d25db8dbbc"), false, 0, false, "Gardens", "/assets/icons/garden.png" },
                    { new Guid("476501c2-92df-4e9f-a863-dfd93dd937c0"), false, 0, false, "Marry", "/assets/icons/marry.png" },
                    { new Guid("4bb8d462-187a-4370-bb1e-e0fb99d233e0"), false, 0, false, "Bakery", "/assets/icons/bakery.png" },
                    { new Guid("52afbf57-54d3-4fe6-a680-40375679b5ff"), false, 0, false, "Holiday", "/assets/icons/holiday.png" },
                    { new Guid("56f4421e-10b6-4940-b594-01de569fde12"), false, 0, false, "Saving", "/assets/icons/Saving.png" },
                    { new Guid("5b311b51-d25d-459e-9d1c-b4e1b199edab"), false, 0, false, "Utilities", "/assets/icons/utilities.png" },
                    { new Guid("6370331d-d544-41b8-ad67-a0cfc0756975"), false, 0, false, "Eat Out", "/assets/icons/eat-out.png" },
                    { new Guid("6550b905-6763-4e97-9038-50ec50d68853"), false, 0, false, "Medicines", "/assets/icons/medicine.png" },
                    { new Guid("68dc6416-6d0b-4e63-b9ff-42c68d3b96f4"), false, 0, false, "Petro", "/assets/icons/petro.png" },
                    { new Guid("6b7c5ad3-82c5-4afc-ad66-a2a895a4bf7b"), false, 0, false, "Others", "/assets/icons/others.png" },
                    { new Guid("70d3e625-cf3b-4eca-a773-f1fb5e340c64"), false, 0, false, "Sports", "/assets/icons/sports.png" },
                    { new Guid("79cf43bd-c6d4-4834-b677-52c9b359473e"), false, 0, false, "Honey Moon", "/assets/icons/honeymoon.png" },
                    { new Guid("84478ca2-0873-4dac-a279-6cc2bd20b22c"), false, 0, false, "Investment", "/assets/icons/investment.png" },
                    { new Guid("8470c1bc-d85c-4f94-9133-b1ba2945f7f2"), false, 0, false, "Jeans", "/assets/icons/jeans.png" },
                    { new Guid("8d5d29e8-dd5a-4971-b1b0-a50a4bf4c73c"), false, 0, false, "Grocery", "/assets/icons/grocery.png" },
                    { new Guid("8ec17bda-749c-4089-8511-bce5cea403aa"), false, 0, false, "Furniture", "/assets/icons/furniture.png" },
                    { new Guid("9392f6a9-f9aa-4d9e-9ab2-8d89ba455ea9"), false, 0, false, "Vaccation", "/assets/icons/vaccation.png" },
                    { new Guid("aa618108-0bad-42e9-b80a-b8e904478b99"), false, 0, false, "Long Term Saving", "/assets/icons/long-term-saving.png" },
                    { new Guid("b028a67a-b2e3-44a7-953f-de1c0959262b"), false, 0, false, "Electricity", "/assets/icons/electricity.png" },
                    { new Guid("b0445780-db7c-4d1e-9d42-3b125422c1a2"), false, 0, false, "Necessities", "/assets/icons/necessities.png" },
                    { new Guid("bc0a7db7-2eed-415f-9076-08dab2e93933"), false, 0, false, "Toys", "/assets/icons/toy.png" },
                    { new Guid("d6552f54-0c69-431e-9907-34147dd2c029"), false, 0, false, "Clothes", "/assets/icons/clothes.png" },
                    { new Guid("e0822b72-a427-445f-acc0-5dc08c8c3929"), false, 0, false, "Wants", "/assets/icons/wants.png" },
                    { new Guid("e1fc64f8-3154-4880-9732-37b5615592bd"), false, 0, false, "Shirt", "/assets/icons/shirt.png" },
                    { new Guid("e54521ec-1d1c-41f0-8353-bc3b62485f25"), false, 0, false, "Car", "/assets/icons/car.png" },
                    { new Guid("ea2978ef-f900-4b01-b0f0-90afe13e0a55"), false, 0, false, "Households", "/assets/icons/household-items.png" },
                    { new Guid("ee676e1c-6a69-41ae-8b3b-b2dac73b9751"), false, 0, false, "Transport", "/assets/icons/transport.png" },
                    { new Guid("eff825bc-673f-43e3-a422-1df323bf274d"), false, 0, false, "Tools", "/assets/icons/tools.png" },
                    { new Guid("f0ffaf46-dde9-47e0-a9f1-d64785d4dfe7"), false, 0, false, "Gift", "/assets/icons/gift.png" },
                    { new Guid("f348aa99-c779-4b7c-a8bd-d96502ee2692"), false, 0, false, "Insurance", "/assets/icons/insurance.png" },
                    { new Guid("f58fb384-e35e-4b15-bbfd-428642178fbc"), false, 0, false, "Family", "/assets/icons/family.png" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6a9ae0f3-285d-450b-96e5-413362fae4a6"), "77df938f-9c25-437b-a4a8-52d745140423", "user", "USER" },
                    { new Guid("9b78ce40-633a-48b5-99e3-d1cc5c753fbe"), "171638f1-534b-4e70-bddd-e975af32de8d", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "BudgetJars",
                columns: new[] { "Id", "Archived", "IconId", "IsDefault", "Name", "Percentage", "TotalBalance", "UserId" },
                values: new object[,]
                {
                    { new Guid("2f32317b-7ce2-469b-91fc-a277d300f667"), false, new Guid("b0445780-db7c-4d1e-9d42-3b125422c1a2"), true, "Necessities", 55f, 0m, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4adc7f4f-d3cd-4188-826c-410b729cfe8c"), false, new Guid("aa618108-0bad-42e9-b80a-b8e904478b99"), true, "Long Term Saving", 10f, 0m, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4ecd52ce-ba4d-45df-bd3b-ce7a412e118d"), false, new Guid("e0822b72-a427-445f-acc0-5dc08c8c3929"), true, "Wants", 10f, 0m, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6b7c5ad3-82c5-4afc-ad66-a2a895a4bf7b"), false, new Guid("6b7c5ad3-82c5-4afc-ad66-a2a895a4bf7b"), true, "Others", 5f, 0m, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7e7ad24e-cbf2-4a31-affe-cafa5c1a325c"), false, new Guid("2613db64-38d8-421c-9e73-c4fc2eb2c6df"), true, "Education", 10f, 0m, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("eee63caf-e26a-4265-817c-259d47e14aba"), false, new Guid("0a55e9f4-ed2a-4ae5-8249-2aa9368efe88"), true, "Financial Freedom", 10f, 0m, new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Archived", "IconId", "IsSystem", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("1137ee8a-9a4e-4625-be8e-0612b6a20bc4"), false, new Guid("1137ee8a-9a4e-4625-be8e-0612b6a20bc4"), true, "Donate", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("2613db64-38d8-421c-9e73-c4fc2eb2c6df"), false, new Guid("2613db64-38d8-421c-9e73-c4fc2eb2c6df"), true, "Education", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("44a67dfd-6a7d-4dbe-b8cf-82d25db8dbbc"), false, new Guid("44a67dfd-6a7d-4dbe-b8cf-82d25db8dbbc"), true, "Gardens", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5b311b51-d25d-459e-9d1c-b4e1b199edab"), false, new Guid("5b311b51-d25d-459e-9d1c-b4e1b199edab"), true, "Utilities", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6370331d-d544-41b8-ad67-a0cfc0756975"), false, new Guid("6370331d-d544-41b8-ad67-a0cfc0756975"), true, "Eat Out", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6550b905-6763-4e97-9038-50ec50d68853"), false, new Guid("6550b905-6763-4e97-9038-50ec50d68853"), true, "Medicines", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("68dc6416-6d0b-4e63-b9ff-42c68d3b96f4"), false, new Guid("68dc6416-6d0b-4e63-b9ff-42c68d3b96f4"), true, "Petro", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6b7c5ad3-82c5-4afc-ad66-a2a895a4bf7b"), false, new Guid("6b7c5ad3-82c5-4afc-ad66-a2a895a4bf7b"), true, "Others", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("70d3e625-cf3b-4eca-a773-f1fb5e340c64"), false, new Guid("70d3e625-cf3b-4eca-a773-f1fb5e340c64"), true, "Sports", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("84478ca2-0873-4dac-a279-6cc2bd20b22c"), false, new Guid("84478ca2-0873-4dac-a279-6cc2bd20b22c"), true, "Investment", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8d5d29e8-dd5a-4971-b1b0-a50a4bf4c73c"), false, new Guid("8d5d29e8-dd5a-4971-b1b0-a50a4bf4c73c"), true, "Grocery", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8ec17bda-749c-4089-8511-bce5cea403aa"), false, new Guid("8ec17bda-749c-4089-8511-bce5cea403aa"), true, "Furniture", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("bc0a7db7-2eed-415f-9076-08dab2e93933"), false, new Guid("bc0a7db7-2eed-415f-9076-08dab2e93933"), true, "Toys", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d6552f54-0c69-431e-9907-34147dd2c029"), false, new Guid("d6552f54-0c69-431e-9907-34147dd2c029"), true, "Clothes", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e54521ec-1d1c-41f0-8353-bc3b62485f25"), false, new Guid("e54521ec-1d1c-41f0-8353-bc3b62485f25"), true, "Car", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ea2978ef-f900-4b01-b0f0-90afe13e0a55"), false, new Guid("ea2978ef-f900-4b01-b0f0-90afe13e0a55"), true, "Households", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("ee676e1c-6a69-41ae-8b3b-b2dac73b9751"), false, new Guid("ee676e1c-6a69-41ae-8b3b-b2dac73b9751"), true, "Transport", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f348aa99-c779-4b7c-a8bd-d96502ee2692"), false, new Guid("f348aa99-c779-4b7c-a8bd-d96502ee2692"), true, "Insurance", new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f58fb384-e35e-4b15-bbfd-428642178fbc"), false, new Guid("f58fb384-e35e-4b15-bbfd-428642178fbc"), true, "Family", new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachements_ExpenseId",
                table: "Attachements",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJars_IconId",
                table: "BudgetJars",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IconId",
                table: "Categories",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BudgetJarId",
                table: "Expenses",
                column: "BudgetJarId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaidDate",
                table: "Expenses",
                column: "PaidDate");

            migrationBuilder.CreateIndex(
                name: "IX_Icons_Name",
                table: "Icons",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeBudgetJars_BudgetJarId",
                table: "IncomeBudgetJars",
                column: "BudgetJarId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurrentExpenses_BudgetJarId",
                table: "RecurrentExpenses",
                column: "BudgetJarId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurrentExpenses_CategoryId",
                table: "RecurrentExpenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachements");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "IncomeBudgetJars");

            migrationBuilder.DropTable(
                name: "PageHtmls");

            migrationBuilder.DropTable(
                name: "RecurrentExpenses");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SysAttributes");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BudgetJars");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Icons");
        }
    }
}
