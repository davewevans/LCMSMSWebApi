using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class ChangePropNamePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Pictures");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "Pictures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PictureFileName",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
