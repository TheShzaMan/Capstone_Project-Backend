using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingReviewUsersTableRedo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "606966c6-ef07-4b9b-be3b-1200aa0a66a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7f213cc-f7aa-4dd2-b343-e14725623d5b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4a22b252-a6a1-481d-9c35-f79f7300adf3", null, "User", "USER" },
                    { "65729c57-2cb7-4caa-a940-000befa57a3f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a22b252-a6a1-481d-9c35-f79f7300adf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65729c57-2cb7-4caa-a940-000befa57a3f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "606966c6-ef07-4b9b-be3b-1200aa0a66a3", null, "Admin", "ADMIN" },
                    { "a7f213cc-f7aa-4dd2-b343-e14725623d5b", null, "User", "USER" }
                });
        }
    }
}
