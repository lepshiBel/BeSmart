using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeSmart.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedstatusTeststatusThememembershiptables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount_of_completed_lessons",
                table: "StatusThemes",
                type: "integer",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount_of_faithfull_answers",
                table: "StatusTests",
                type: "integer",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount_of_incorrect_answers",
                table: "StatusTests",
                type: "integer",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount_of_completed_themes",
                table: "Memberships",
                type: "integer",
                nullable: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount_of_completed_lessons",
                table: "StatusThemes");

            migrationBuilder.DropColumn(
                name: "Amount_of_faithfull_answers",
                table: "StatusTests");

            migrationBuilder.DropColumn(
                name: "Amount_of_incorrect_answers",
                table: "StatusTests");

            migrationBuilder.DropColumn(
                name: "Amount_of_completed_themes",
                table: "Memberships");
        }
    }
}
