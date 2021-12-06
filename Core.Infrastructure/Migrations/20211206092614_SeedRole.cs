using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Infrastructure.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6a9ae0f3-285d-450b-96e5-413362fae4a6"), "e5f1b7cb-7171-47dd-992c-992484893e54", "User", "USER" },
                    { new Guid("9b78ce40-633a-48b5-99e3-d1cc5c753fbe"), "d1ca48c6-d4d7-42f5-a4ab-1f12db2c0cce", "Developer", "DEVELOPER" },
                    { new Guid("9f50e6a8-e115-489b-8b4b-dbc70b2fbbfc"), "5df128f6-38d1-4ba7-b35c-3a94b21c1893", "Support", "SUPPORT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6a9ae0f3-285d-450b-96e5-413362fae4a6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b78ce40-633a-48b5-99e3-d1cc5c753fbe"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9f50e6a8-e115-489b-8b4b-dbc70b2fbbfc"));
        }
    }
}
