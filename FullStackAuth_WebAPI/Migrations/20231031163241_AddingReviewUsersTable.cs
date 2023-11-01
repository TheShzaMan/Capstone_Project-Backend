using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingReviewUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Reviews_ReviewId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_UserForDisplayDto_PostingUserId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "UserForDisplayDto");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_PostingUserId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReviewId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ffb201f-5229-45bf-8e13-c606692a4439");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4637879-d935-4eea-abd6-ded1c28ace20");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PostingUserId",
                table: "Jobs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewUsers",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    IsCreator = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewUsers", x => new { x.ReviewId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ReviewUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewUsers_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "606966c6-ef07-4b9b-be3b-1200aa0a66a3", null, "Admin", "ADMIN" },
                    { "a7f213cc-f7aa-4dd2-b343-e14725623d5b", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewUsers_UserId",
                table: "ReviewUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "606966c6-ef07-4b9b-be3b-1200aa0a66a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7f213cc-f7aa-4dd2-b343-e14725623d5b");

            migrationBuilder.AlterColumn<string>(
                name: "PostingUserId",
                table: "Jobs",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserForDisplayDto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReviewId",
                table: "AspNetUsers",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Reviews_ReviewId",
                table: "AspNetUsers",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_UserForDisplayDto_PostingUserId",
                table: "Jobs",
                column: "PostingUserId",
                principalTable: "UserForDisplayDto",
                principalColumn: "Id");
        }
    }
}
