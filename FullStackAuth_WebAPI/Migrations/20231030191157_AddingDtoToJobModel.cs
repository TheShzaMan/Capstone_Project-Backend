using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingDtoToJobModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62e6f431-10ae-40e0-a0eb-99e20c53c602");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7688f7c2-01bd-4814-bf51-9a4241534d0c");

            migrationBuilder.AlterColumn<string>(
                name: "PostingUserId",
                table: "Jobs",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserForDisplayDto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    LastName = table.Column<string>(type: "longtext", nullable: true),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserForDisplayDto", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7ffb201f-5229-45bf-8e13-c606692a4439", null, "Admin", "ADMIN" },
                    { "a4637879-d935-4eea-abd6-ded1c28ace20", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PostingUserId",
                table: "Jobs",
                column: "PostingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_UserForDisplayDto_PostingUserId",
                table: "Jobs",
                column: "PostingUserId",
                principalTable: "UserForDisplayDto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_UserForDisplayDto_PostingUserId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "UserForDisplayDto");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_PostingUserId",
                table: "Jobs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ffb201f-5229-45bf-8e13-c606692a4439");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4637879-d935-4eea-abd6-ded1c28ace20");

            migrationBuilder.AlterColumn<string>(
                name: "PostingUserId",
                table: "Jobs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62e6f431-10ae-40e0-a0eb-99e20c53c602", null, "Admin", "ADMIN" },
                    { "7688f7c2-01bd-4814-bf51-9a4241534d0c", null, "User", "USER" }
                });
        }
    }
}
