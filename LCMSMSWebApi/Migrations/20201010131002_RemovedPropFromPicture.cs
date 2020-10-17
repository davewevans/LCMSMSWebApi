using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class RemovedPropFromPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PictureFileName",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Pictures");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "Pictures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PictureFileName",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
