using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeSmart.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class addcashing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Password",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "User_Role");

            migrationBuilder.AlterColumn<string>(
                name: "User_Role",
                table: "Users",
                type: "text",
                nullable: true,
                defaultValue: "user",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "User_PasswordHash",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "User_PasswordSalt",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "User_PasswordSalt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "User_Role",
                table: "Users",
                newName: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "user");

            migrationBuilder.AddColumn<string>(
                name: "User_Password",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
