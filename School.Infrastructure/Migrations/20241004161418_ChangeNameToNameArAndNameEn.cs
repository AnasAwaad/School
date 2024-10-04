using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameToNameArAndNameEn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectName",
                table: "Subjects",
                newName: "SubjectNameEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "DName",
                table: "Departments",
                newName: "DNameEn");

            migrationBuilder.AddColumn<string>(
                name: "SubjectNameAr",
                table: "Subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNameAr",
                table: "Departments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectNameAr",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DNameAr",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "SubjectNameEn",
                table: "Subjects",
                newName: "SubjectName");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DNameEn",
                table: "Departments",
                newName: "DName");
        }
    }
}
