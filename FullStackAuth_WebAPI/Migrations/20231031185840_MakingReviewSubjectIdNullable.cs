using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class MakingReviewSubjectIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04ad4f5c-94e6-4fd6-bba1-806e88e03bd9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91b6d208-21da-416c-9e5e-8a9a1c102b02");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7affe000-248c-4389-82e8-89b6fc82c419", null, "Admin", "ADMIN" },
                    { "d962d4d1-8932-4ea8-855d-8687c9a4d99d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7affe000-248c-4389-82e8-89b6fc82c419");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d962d4d1-8932-4ea8-855d-8687c9a4d99d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04ad4f5c-94e6-4fd6-bba1-806e88e03bd9", null, "Admin", "ADMIN" },
                    { "91b6d208-21da-416c-9e5e-8a9a1c102b02", null, "User", "USER" }
                });
        }
    }
}
