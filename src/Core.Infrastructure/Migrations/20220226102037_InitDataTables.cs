using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Infrastructure.Migrations
{
    public partial class InitDataTables : Migration
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
                    PeriodFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    Content = table.Column<string>(type: "text", nullable: true),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
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
                    Value = table.Column<string>(type: "text", nullable: true),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
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
                    Currency = table.Column<string>(type: "text", nullable: true),
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
                name: "BudgetJarTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Percentage = table.Column<int>(type: "integer", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IconId = table.Column<Guid>(type: "uuid", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetJarTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetJarTemplates_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseGroups",
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
                    table.PrimaryKey("PK_ExpenseGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseGroups_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetJars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IncomeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Percentage = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    IconId = table.Column<Guid>(type: "uuid", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetJars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetJars_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BudgetJars_Incomes_IncomeId",
                        column: x => x.IncomeId,
                        principalTable: "Incomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    Currency = table.Column<string>(type: "text", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ref = table.Column<string>(type: "text", nullable: true),
                    Payee = table.Column<string>(type: "text", nullable: true),
                    IsTaxable = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    BudgetJarId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseGroupId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseGroups_ExpenseGroupId",
                        column: x => x.ExpenseGroupId,
                        principalTable: "ExpenseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    ExpenseGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Repeat = table.Column<int>(type: "integer", nullable: false),
                    RepeatDay = table.Column<int>(type: "integer", nullable: false),
                    RepeatDaily = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrentExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurrentExpenses_BudgetJars_BudgetJarId",
                        column: x => x.BudgetJarId,
                        principalTable: "BudgetJars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RecurrentExpenses_ExpenseGroups_ExpenseGroupId",
                        column: x => x.ExpenseGroupId,
                        principalTable: "ExpenseGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: true),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Code", "Text", "UnicodeDecimal", "UnicodeHex" },
                values: new object[,]
                {
                    { "AFN", "Afghanistan Afghani", "1547", "60b" },
                    { "ALL", "Albania Lek", "76, 101, 107", "4c, 65, 6b" },
                    { "ANG", "Netherlands Antilles Guilder", "402", "192" },
                    { "ARS", "Argentina Peso", "36", "24" },
                    { "AUD", "Australia Dollar", "36", "24" },
                    { "AWG", "Aruba Guilder", "402", "192" },
                    { "AZN", "Azerbaijan New Manat", "1084, 1072, 1085", "43c, 430, 43d" },
                    { "BAM", "Bosnia and Herzegovina Convertible Marka", "75, 77", "4b, 4d" },
                    { "BBD", "Barbados Dollar", "36", "24" },
                    { "BGN", "Bulgaria Lev", "1083, 1074", "43b, 432" },
                    { "BMD", "Bermuda Dollar", "36", "24" },
                    { "BND", "Brunei Darussalam Dollar", "36", "24" },
                    { "BOB", "Bolivia Boliviano", "36, 98", "24, 62" },
                    { "BRL", "Brazil Real", "82, 36", "52, 24" },
                    { "BSD", "Bahamas Dollar", "36", "24" },
                    { "BWP", "Botswana Pula", "80", "50" },
                    { "BYR", "Belarus Ruble", "112, 46", "70, 2e" },
                    { "BZD", "Belize Dollar", "66, 90, 36", "42, 5a, 24" },
                    { "CAD", "Canada Dollar", "36", "24" },
                    { "CHF", "Switzerland Franc", "67, 72, 70", "43, 48, 46" },
                    { "CLP", "Chile Peso", "36", "24" },
                    { "CNY", "China Yuan Renminbi", "165", "a5" },
                    { "COP", "Colombia Peso", "36", "24" },
                    { "CRC", "Costa Rica Colon", "8353", "20a1" },
                    { "CUP", "Cuba Peso", "8369", "20b1" },
                    { "CZK", "Czech Republic Koruna", "75, 269", "4b, 10d" },
                    { "DKK", "Denmark Krone", "107, 114", "6b, 72" },
                    { "DOP", "Dominican Republic Peso", "82, 68, 36", "52, 44, 24" },
                    { "EEK", "Estonia Kroon", "107, 114", "6b, 72" },
                    { "EGP", "Egypt Pound", "163", "a3" },
                    { "EUR", "Euro Member Countries", "8364", "20ac" },
                    { "FJD", "Fiji Dollar", "36", "24" },
                    { "FKP", "Falkland Islands (Malvinas) Pound", "163", "a3" },
                    { "GBP", "United Kingdom Pound", "163", "a3" },
                    { "GGP", "Guernsey Pound", "163", "a3" },
                    { "GHC", "Ghana Cedis", "162", "a2" },
                    { "GIP", "Gibraltar Pound", "163", "a3" },
                    { "GTQ", "Guatemala Quetzal", "81", "51" },
                    { "GYD", "Guyana Dollar", "36", "24" },
                    { "HKD", "Hong Kong Dollar", "36", "24" },
                    { "HNL", "Honduras Lempira", "76", "4c" },
                    { "HRK", "Croatia Kuna", "107, 110", "6b, 6e" },
                    { "HUF", "Hungary Forint", "70, 116", "46, 74" },
                    { "IDR", "Indonesia Rupiah", "82, 112", "52, 70" },
                    { "ILS", "Israel Shekel", "8362", "20aa" },
                    { "IMP", "Isle of Man Pound", "163", "a3" },
                    { "INR", "India Rupee", "", "" },
                    { "IRR", "Iran Rial", "65020", "fdfc" },
                    { "ISK", "Iceland Krona", "107, 114", "6b, 72" },
                    { "JEP", "Jersey Pound", "163", "a3" },
                    { "JMD", "Jamaica Dollar", "74, 36", "4a, 24" },
                    { "JPY", "Japan Yen", "165", "a5" },
                    { "KGS", "Kyrgyzstan Som", "1083, 1074", "43b, 432" },
                    { "KHR", "Cambodia Riel", "6107", "17db" },
                    { "KPW", "Korea (North) Won", "8361", "20a9" },
                    { "KRW", "Korea (South) Won", "8361", "20a9" },
                    { "KYD", "Cayman Islands Dollar", "36", "24" },
                    { "KZT", "Kazakhstan Tenge", "1083, 1074", "43b, 432" },
                    { "LAK", "Laos Kip", "8365", "20ad" },
                    { "LBP", "Lebanon Pound", "163", "a3" },
                    { "LKR", "Sri Lanka Rupee", "8360", "20a8" },
                    { "LRD", "Liberia Dollar", "36", "24" },
                    { "LTL", "Lithuania Litas", "76, 116", "4c, 74" },
                    { "LVL", "Latvia Lat", "76, 115", "4c, 73" },
                    { "MKD", "Macedonia Denar", "1076, 1077, 1085", "434, 435, 43d" },
                    { "MNT", "Mongolia Tughrik", "8366", "20ae" },
                    { "MUR", "Mauritius Rupee", "8360", "20a8" },
                    { "MXN", "Mexico Peso", "36", "24" },
                    { "MYR", "Malaysia Ringgit", "82, 77", "52, 4d" },
                    { "MZN", "Mozambique Metical", "77, 84", "4d, 54" },
                    { "NAD", "Namibia Dollar", "36", "24" },
                    { "NGN", "Nigeria Naira", "8358", "20a6" },
                    { "NIO", "Nicaragua Cordoba", "67, 36", "43, 24" },
                    { "NOK", "Norway Krone", "107, 114", "6b, 72" },
                    { "NPR", "Nepal Rupee", "8360", "20a8" },
                    { "NZD", "New Zealand Dollar", "36", "24" },
                    { "OMR", "Oman Rial", "65020", "fdfc" },
                    { "PAB", "Panama Balboa", "66, 47, 46", "42, 2f, 2e" },
                    { "PEN", "Peru Nuevo Sol", "83, 47, 46", "53, 2f, 2e" },
                    { "PHP", "Philippines Peso", "8369", "20b1" },
                    { "PKR", "Pakistan Rupee", "8360", "20a8" },
                    { "PLN", "Poland Zloty", "122, 322", "7a, 142" },
                    { "PYG", "Paraguay Guarani", "71, 115", "47, 73" },
                    { "QAR", "Qatar Riyal", "65020", "fdfc" },
                    { "RON", "Romania New Leu", "108, 101, 105", "6c, 65, 69" },
                    { "RSD", "Serbia Dinar", "1044, 1080, 1085, 46", "414, 438, 43d, 2e" },
                    { "RUB", "Russia Ruble", "1088, 1091, 1073", "440, 443, 431" },
                    { "SAR", "Saudi Arabia Riyal", "65020", "fdfc" },
                    { "SBD", "Solomon Islands Dollar", "36", "24" },
                    { "SCR", "Seychelles Rupee", "8360", "20a8" },
                    { "SEK", "Sweden Krona", "107, 114", "6b, 72" },
                    { "SGD", "Singapore Dollar", "36", "24" },
                    { "SHP", "Saint Helena Pound", "163", "a3" },
                    { "SOS", "Somalia Shilling", "83", "53" },
                    { "SRD", "Suriname Dollar", "36", "24" },
                    { "SVC", "El Salvador Colon", "36", "24" },
                    { "SYP", "Syria Pound", "163", "a3" },
                    { "THB", "Thailand Baht", "3647", "e3f" },
                    { "TRL", "Turkey Lira", "8356", "20a4" },
                    { "TRY", "Turkey Lira", "", "" },
                    { "TTD", "Trinidad and Tobago Dollar", "84, 84, 36", "54, 54, 24" },
                    { "TVD", "Tuvalu Dollar", "36", "24" },
                    { "TWD", "Taiwan New Dollar", "78, 84, 36", "4e, 54, 24" },
                    { "UAH", "Ukraine Hryvna", "8372", "20b4" },
                    { "USD", "United States Dollar", "36", "24" },
                    { "UYU", "Uruguay Peso", "36, 85", "24, 55" },
                    { "UZS", "Uzbekistan Som", "1083, 1074", "43b, 432" },
                    { "VEF", "Venezuela Bolivar", "66, 115", "42, 73" },
                    { "VND", "Viet Nam Dong", "8363", "20ab" },
                    { "XCD", "East Caribbean Dollar", "36", "24" },
                    { "YER", "Yemen Rial", "65020", "fdfc" },
                    { "ZAR", "South Africa Rand", "82", "52" },
                    { "ZWD", "Zimbabwe Dollar", "90, 36", "5a, 24" }
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
                    { new Guid("6a9ae0f3-285d-450b-96e5-413362fae4a6"), "9e55630e-755c-47d5-936e-34693dc04a79", "user", "USER" },
                    { new Guid("9b78ce40-633a-48b5-99e3-d1cc5c753fbe"), "dc97d136-bab6-4e68-8111-9f25e7f0a016", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "BudgetJarTemplates",
                columns: new[] { "Id", "Archived", "IconId", "IsSystem", "Name", "Percentage", "UserId" },
                values: new object[,]
                {
                    { new Guid("2f32317b-7ce2-469b-91fc-a277d300f667"), false, new Guid("b0445780-db7c-4d1e-9d42-3b125422c1a2"), true, "Necessities", 55, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4adc7f4f-d3cd-4188-826c-410b729cfe8c"), false, new Guid("aa618108-0bad-42e9-b80a-b8e904478b99"), true, "Long Term Saving", 10, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4ecd52ce-ba4d-45df-bd3b-ce7a412e118d"), false, new Guid("e0822b72-a427-445f-acc0-5dc08c8c3929"), true, "Wants", 10, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("6b7c5ad3-82c5-4afc-ad66-a2a895a4bf7b"), false, new Guid("6b7c5ad3-82c5-4afc-ad66-a2a895a4bf7b"), true, "Others", 5, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7e7ad24e-cbf2-4a31-affe-cafa5c1a325c"), false, new Guid("2613db64-38d8-421c-9e73-c4fc2eb2c6df"), true, "Education", 10, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("eee63caf-e26a-4265-817c-259d47e14aba"), false, new Guid("0a55e9f4-ed2a-4ae5-8249-2aa9368efe88"), true, "Financial Freedom", 10, new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ExpenseGroups",
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
                name: "IX_Attachment_ExpenseId",
                table: "Attachment",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJars_IconId",
                table: "BudgetJars",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJars_IncomeId",
                table: "BudgetJars",
                column: "IncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetJarTemplates_IconId",
                table: "BudgetJarTemplates",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseGroups_IconId",
                table: "ExpenseGroups",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BudgetJarId",
                table: "Expenses",
                column: "BudgetJarId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseGroupId",
                table: "Expenses",
                column: "ExpenseGroupId");

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
                name: "IX_RecurrentExpenses_BudgetJarId",
                table: "RecurrentExpenses",
                column: "BudgetJarId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurrentExpenses_ExpenseGroupId",
                table: "RecurrentExpenses",
                column: "ExpenseGroupId");

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
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "BudgetJarTemplates");

            migrationBuilder.DropTable(
                name: "Currencies");

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
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BudgetJars");

            migrationBuilder.DropTable(
                name: "ExpenseGroups");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Icons");
        }
    }
}
