using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingReviewAndJobModelsUpdatingUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "239f493b-e5aa-4694-a296-e380b93b2369");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f5fe06a-5d66-4aa1-b694-9f917afefbb8");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessDescription",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailNow",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkillLevel",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WagePerHour",
                table: "AspNetUsers",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Location = table.Column<string>(type: "longtext", nullable: false),
                    JobName = table.Column<string>(type: "longtext", nullable: false),
                    SkillLevel = table.Column<string>(type: "longtext", nullable: false),
                    JobDescription = table.Column<string>(type: "longtext", nullable: false),
                    PayPerHour = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AdherenceToAgreement = table.Column<int>(type: "int", nullable: false),
                    WorkQuality = table.Column<int>(type: "int", nullable: true),
                    Timeliness = table.Column<int>(type: "int", nullable: false),
                    LikelinessOfRepeat = table.Column<int>(type: "int", nullable: false),
                    Communication = table.Column<int>(type: "int", nullable: false),
                    AdaptabilityRate = table.Column<int>(type: "int", nullable: true),
                    IsWorker = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                    { "86aa0e5b-6f0d-4185-b58f-dc90f0a33ade", null, "User", "USER" },
                    { "cc4aaea5-371c-4f18-9997-81b67c02b5f6", null, "Admin", "ADMIN" }
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

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86aa0e5b-6f0d-4185-b58f-dc90f0a33ade");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc4aaea5-371c-4f18-9997-81b67c02b5f6");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BusinessDescription",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAvailNow",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SkillLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WagePerHour",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "239f493b-e5aa-4694-a296-e380b93b2369", null, "Admin", "ADMIN" },
                    { "3f5fe06a-5d66-4aa1-b694-9f917afefbb8", null, "User", "USER" }
                });
        }
    }
}
