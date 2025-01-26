using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientInfoMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmokingHabits",
                table: "Lifestyles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmokingHabits",
                table: "Lifestyles",
                type: "TEXT",
                nullable: true);
        }
    }
}
