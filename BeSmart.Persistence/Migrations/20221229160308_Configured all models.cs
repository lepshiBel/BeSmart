using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeSmart.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Configuredallmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Themes",
                newName: "Theme_Name");

            migrationBuilder.RenameColumn(
                name: "CountTest",
                table: "Themes",
                newName: "Theme_CountTest");

            migrationBuilder.RenameColumn(
                name: "CountLesson",
                table: "Themes",
                newName: "Theme_CountLesson");

            migrationBuilder.RenameColumn(
                name: "QuestionsCount",
                table: "Tests",
                newName: "Test_QuestionsCount");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tests",
                newName: "Test_Name");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Lessons",
                newName: "Lesson_text");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lessons",
                newName: "Lesson_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Course_Name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Category_name");

            migrationBuilder.RenameColumn(
                name: "Word",
                table: "Cards",
                newName: "Card_word");

            migrationBuilder.RenameColumn(
                name: "Transctipt",
                table: "Cards",
                newName: "Card_transcript");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Cards",
                newName: "Card_text");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Cards",
                newName: "Card_imageUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AccountType",
                newName: "AccountType_Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AccountType",
                newName: "AccoutType_Description");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Account",
                newName: "Account_userName");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Account",
                newName: "Account_userEmail");

            migrationBuilder.AlterColumn<string>(
                name: "Theme_Name",
                table: "Themes",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Test_Name",
                table: "Tests",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Lesson_text",
                table: "Lessons",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Lesson_name",
                table: "Lessons",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Course_Name",
                table: "Courses",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Course_CountOfThemes",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Category_name",
                table: "Categories",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Card_word",
                table: "Cards",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Card_transcript",
                table: "Cards",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Card_text",
                table: "Cards",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Card_imageUrl",
                table: "Cards",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AccountType_Name",
                table: "AccountType",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AccoutType_Description",
                table: "AccountType",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Account_userName",
                table: "Account",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Account_userEmail",
                table: "Account",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Account_userPassword",
                table: "Account",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Categories_CategoryId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Course_CountOfThemes",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Account_userPassword",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "Theme_Name",
                table: "Themes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Theme_CountTest",
                table: "Themes",
                newName: "CountTest");

            migrationBuilder.RenameColumn(
                name: "Theme_CountLesson",
                table: "Themes",
                newName: "CountLesson");

            migrationBuilder.RenameColumn(
                name: "Test_QuestionsCount",
                table: "Tests",
                newName: "QuestionsCount");

            migrationBuilder.RenameColumn(
                name: "Test_Name",
                table: "Tests",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Lesson_text",
                table: "Lessons",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Lesson_name",
                table: "Lessons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Course_Name",
                table: "Courses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Category_name",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Card_word",
                table: "Cards",
                newName: "Word");

            migrationBuilder.RenameColumn(
                name: "Card_transcript",
                table: "Cards",
                newName: "Transctipt");

            migrationBuilder.RenameColumn(
                name: "Card_text",
                table: "Cards",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Card_imageUrl",
                table: "Cards",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "AccoutType_Description",
                table: "AccountType",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "AccountType_Name",
                table: "AccountType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Account_userName",
                table: "Account",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Account_userEmail",
                table: "Account",
                newName: "UserEmail");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Themes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tests",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Lessons",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lessons",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Transctipt",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AccountType",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccountType",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Account",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Account",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Account",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
