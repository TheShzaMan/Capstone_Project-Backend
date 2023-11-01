using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingReviewedUserIdToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "209352d4-f4ca-499c-a6c2-db50e21b8b79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adb3011c-7a91-4076-9f74-eb46fd06f3a2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9e0f207e-defe-48d7-8dda-bb39e87b2db3", null, "Admin", "ADMIN" },
                    { "d0bc621e-2ebe-447a-91e7-ad562e356e0f", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "209352d4-f4ca-499c-a6c2-db50e21b8b79", null, "Admin", "ADMIN" },
                    { "adb3011c-7a91-4076-9f74-eb46fd06f3a2", null, "User", "USER" }
                });
        }
    }
}
