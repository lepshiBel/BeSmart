using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeSmart.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Addcourselearningtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: true, defaultValue: "Не пройдено"),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusThemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: true, defaultValue: "Не пройдено"),
                    MembersipID = table.Column<int>(name: "Membersip_ID", type: "integer", nullable: false),
                    ThemeID = table.Column<int>(name: "Theme_ID", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusThemes_Memberships_Membersip_ID",
                        column: x => x.MembersipID,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusThemes_Themes_Theme_ID",
                        column: x => x.ThemeID,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: true, defaultValue: "Не пройдено"),
                    StatusofthemeID = table.Column<int>(name: "Status_of_theme_ID", type: "integer", nullable: false),
                    LessonID = table.Column<int>(name: "Lesson_ID", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusLessons_Lessons_Lesson_ID",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusLessons_StatusThemes_Status_of_theme_ID",
                        column: x => x.StatusofthemeID,
                        principalTable: "StatusThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: true, defaultValue: "Не пройдено"),
                    Mark = table.Column<int>(type: "integer", nullable: true),
                    StatusofthemeID = table.Column<int>(name: "Status_of_theme_ID", type: "integer", nullable: false),
                    TestID = table.Column<int>(name: "Test_ID", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusTests_StatusThemes_Status_of_theme_ID",
                        column: x => x.StatusofthemeID,
                        principalTable: "StatusThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusTests_Tests_Test_ID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_CourseId",
                table: "Memberships",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserId",
                table: "Memberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusLessons_Lesson_ID",
                table: "StatusLessons",
                column: "Lesson_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusLessons_Status_of_theme_ID",
                table: "StatusLessons",
                column: "Status_of_theme_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTests_Status_of_theme_ID",
                table: "StatusTests",
                column: "Status_of_theme_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTests_Test_ID",
                table: "StatusTests",
                column: "Test_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusThemes_Membersip_ID",
                table: "StatusThemes",
                column: "Membersip_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StatusThemes_Theme_ID",
                table: "StatusThemes",
                column: "Theme_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusLessons");

            migrationBuilder.DropTable(
                name: "StatusTests");

            migrationBuilder.DropTable(
                name: "StatusThemes");

            migrationBuilder.DropTable(
                name: "Memberships");
        }
    }
}
