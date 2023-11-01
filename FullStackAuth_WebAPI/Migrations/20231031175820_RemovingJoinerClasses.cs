using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovingJoinerClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobUsers");

            migrationBuilder.DropTable(
                name: "ReviewUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a22b252-a6a1-481d-9c35-f79f7300adf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65729c57-2cb7-4caa-a940-000befa57a3f");

            migrationBuilder.AddColumn<string>(
                name: "ReviewSubjectId",
                table: "Reviews",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobUser",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobUser", x => new { x.JobsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_JobUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobUser_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReviewUser",
                columns: table => new
                {
                    ReviewsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewUser", x => new { x.ReviewsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ReviewUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewUser_Reviews_ReviewsId",
                        column: x => x.ReviewsId,
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
                    { "209352d4-f4ca-499c-a6c2-db50e21b8b79", null, "Admin", "ADMIN" },
                    { "adb3011c-7a91-4076-9f74-eb46fd06f3a2", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobUser_UsersId",
                table: "JobUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewUser_UsersId",
                table: "ReviewUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobUser");

            migrationBuilder.DropTable(
                name: "ReviewUser");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "209352d4-f4ca-499c-a6c2-db50e21b8b79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adb3011c-7a91-4076-9f74-eb46fd06f3a2");

            migrationBuilder.DropColumn(
                name: "ReviewSubjectId",
                table: "Reviews");

            migrationBuilder.CreateTable(
                name: "JobUsers",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobUsers", x => new { x.JobId, x.UserId });
                    table.ForeignKey(
                        name: "FK_JobUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobUsers_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                    { "4a22b252-a6a1-481d-9c35-f79f7300adf3", null, "User", "USER" },
                    { "65729c57-2cb7-4caa-a940-000befa57a3f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobUsers_UserId",
                table: "JobUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewUsers_UserId",
                table: "ReviewUsers",
                column: "UserId");
        }
    }
}
