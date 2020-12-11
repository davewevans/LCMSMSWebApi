using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddPopForLastDonation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastDonationDate",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Narrations_GuardianID",
                table: "Narrations",
                column: "GuardianID");

            migrationBuilder.AddForeignKey(
                name: "FK_Narrations_Guardians_GuardianID",
                table: "Narrations",
                column: "GuardianID",
                principalTable: "Guardians",
                principalColumn: "GuardianID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narrations_Guardians_GuardianID",
                table: "Narrations");

            migrationBuilder.DropIndex(
                name: "IX_Narrations_GuardianID",
                table: "Narrations");

            migrationBuilder.DropColumn(
                name: "LastDonationDate",
                table: "Sponsors");
        }
    }
}
