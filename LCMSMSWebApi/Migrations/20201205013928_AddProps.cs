using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelationshipToGuardian",
                table: "Orphans",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllSponsors",
                table: "Documents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelationshipToGuardian",
                table: "Orphans");

            migrationBuilder.DropColumn(
                name: "AllSponsors",
                table: "Documents");
        }
    }
}
