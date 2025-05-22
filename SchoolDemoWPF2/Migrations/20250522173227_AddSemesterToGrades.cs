using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDemoWPF2.Migrations
{
    /// <inheritdoc />
    public partial class AddSemesterToGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Grades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Grades");
        }
    }
}
