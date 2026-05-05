using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTutor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyNotesToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudyNotes",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyNotes",
                table: "Sessions");
        }
    }
}
