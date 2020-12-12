using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class MakeSponsorIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Sponsors_SponsorID",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "SponsorID",
                table: "Documents",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Sponsors_SponsorID",
                table: "Documents",
                column: "SponsorID",
                principalTable: "Sponsors",
                principalColumn: "SponsorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Sponsors_SponsorID",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "SponsorID",
                table: "Documents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Sponsors_SponsorID",
                table: "Documents",
                column: "SponsorID",
                principalTable: "Sponsors",
                principalColumn: "SponsorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
