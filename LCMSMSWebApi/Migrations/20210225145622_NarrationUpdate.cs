using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class NarrationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Narrations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Narrations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByID",
                table: "Narrations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "Narrations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedByID",
                table: "Narrations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Narrations");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Narrations");

            migrationBuilder.DropColumn(
                name: "ApprovedByID",
                table: "Narrations");

            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "Narrations");

            migrationBuilder.DropColumn(
                name: "SubmittedByID",
                table: "Narrations");
        }
    }
}
