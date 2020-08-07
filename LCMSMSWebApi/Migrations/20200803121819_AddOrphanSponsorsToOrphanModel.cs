using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddOrphanSponsorsToOrphanModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcademicsUpdateTimeStamp",
                table: "DbUpdates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GuardiansUpdateTimeStamp",
                table: "DbUpdates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NarrationsUpdateTimeStamp",
                table: "DbUpdates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrphansUpdateTimeStamp",
                table: "DbUpdates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PicturesUpdateTimeStamp",
                table: "DbUpdates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SponsorsUpdateTimeStamp",
                table: "DbUpdates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicsUpdateTimeStamp",
                table: "DbUpdates");

            migrationBuilder.DropColumn(
                name: "GuardiansUpdateTimeStamp",
                table: "DbUpdates");

            migrationBuilder.DropColumn(
                name: "NarrationsUpdateTimeStamp",
                table: "DbUpdates");

            migrationBuilder.DropColumn(
                name: "OrphansUpdateTimeStamp",
                table: "DbUpdates");

            migrationBuilder.DropColumn(
                name: "PicturesUpdateTimeStamp",
                table: "DbUpdates");

            migrationBuilder.DropColumn(
                name: "SponsorsUpdateTimeStamp",
                table: "DbUpdates");
        }
    }
}
