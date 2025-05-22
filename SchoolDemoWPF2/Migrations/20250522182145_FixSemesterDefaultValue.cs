using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDemoWPF2.Migrations
{
    /// <inheritdoc />
    public partial class FixSemesterDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "Semester",
            table: "Grades",
            nullable: false,
            defaultValue: 1); // 1 соответствует Semester.First
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
