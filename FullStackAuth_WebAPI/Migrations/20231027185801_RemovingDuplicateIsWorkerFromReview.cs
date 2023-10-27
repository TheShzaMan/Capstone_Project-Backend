using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovingDuplicateIsWorkerFromReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86aa0e5b-6f0d-4185-b58f-dc90f0a33ade");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc4aaea5-371c-4f18-9997-81b67c02b5f6");

            migrationBuilder.DropColumn(
                name: "IsWorker",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04c2fe21-777a-4a18-ac31-434854ed5f3e", null, "User", "USER" },
                    { "67b6d7de-e4da-41be-9ec9-047b95d2f6a6", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04c2fe21-777a-4a18-ac31-434854ed5f3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67b6d7de-e4da-41be-9ec9-047b95d2f6a6");

            migrationBuilder.AddColumn<bool>(
                name: "IsWorker",
                table: "Reviews",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "86aa0e5b-6f0d-4185-b58f-dc90f0a33ade", null, "User", "USER" },
                    { "cc4aaea5-371c-4f18-9997-81b67c02b5f6", null, "Admin", "ADMIN" }
                });
        }
    }
}
