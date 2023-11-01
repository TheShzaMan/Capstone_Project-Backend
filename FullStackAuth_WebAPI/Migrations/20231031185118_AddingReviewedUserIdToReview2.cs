using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingReviewedUserIdToReview2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e0f207e-defe-48d7-8dda-bb39e87b2db3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0bc621e-2ebe-447a-91e7-ad562e356e0f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04ad4f5c-94e6-4fd6-bba1-806e88e03bd9", null, "Admin", "ADMIN" },
                    { "91b6d208-21da-416c-9e5e-8a9a1c102b02", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "9e0f207e-defe-48d7-8dda-bb39e87b2db3", null, "Admin", "ADMIN" },
                    { "d0bc621e-2ebe-447a-91e7-ad562e356e0f", null, "User", "USER" }
                });
        }
    }
}
