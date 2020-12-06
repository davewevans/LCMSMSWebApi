using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddedPropsOrpansSponsors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Orphans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "YearOfAdmission",
                table: "Orphans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Orphans");

            migrationBuilder.DropColumn(
                name: "YearOfAdmission",
                table: "Orphans");
        }
    }
}
