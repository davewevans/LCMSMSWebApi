using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class ChangeSateToState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sate",
                table: "Sponsors");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Sponsors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Sponsors");

            migrationBuilder.AddColumn<string>(
                name: "Sate",
                table: "Sponsors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
