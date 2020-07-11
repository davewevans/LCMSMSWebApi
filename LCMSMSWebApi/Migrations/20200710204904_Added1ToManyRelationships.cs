using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class Added1ToManyRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuardianID",
                table: "Narrations");

            migrationBuilder.CreateIndex(
                name: "IX_Orphans_GuardianID",
                table: "Orphans",
                column: "GuardianID");

            migrationBuilder.CreateIndex(
                name: "IX_Narrations_OrphanID",
                table: "Narrations",
                column: "OrphanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Narrations_Orphans_OrphanID",
                table: "Narrations",
                column: "OrphanID",
                principalTable: "Orphans",
                principalColumn: "OrphanID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orphans_Guardians_GuardianID",
                table: "Orphans",
                column: "GuardianID",
                principalTable: "Guardians",
                principalColumn: "GuardianID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narrations_Orphans_OrphanID",
                table: "Narrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Orphans_Guardians_GuardianID",
                table: "Orphans");

            migrationBuilder.DropIndex(
                name: "IX_Orphans_GuardianID",
                table: "Orphans");

            migrationBuilder.DropIndex(
                name: "IX_Narrations_OrphanID",
                table: "Narrations");

            migrationBuilder.AddColumn<int>(
                name: "GuardianID",
                table: "Narrations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
