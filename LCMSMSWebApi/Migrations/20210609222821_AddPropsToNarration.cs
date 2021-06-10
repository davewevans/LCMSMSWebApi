using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddPropsToNarration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedAt",
                table: "Narrations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedByID",
                table: "Narrations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedAt",
                table: "Narrations");

            migrationBuilder.DropColumn(
                name: "RejectedByID",
                table: "Narrations");
        }
    }
}
