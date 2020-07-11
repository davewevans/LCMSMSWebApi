using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class MakeForeignIDsNullableInNarrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narrations_Orphans_OrphanID",
                table: "Narrations");

            migrationBuilder.AlterColumn<int>(
                name: "OrphanID",
                table: "Narrations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GuardianID",
                table: "Narrations",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Narrations_Orphans_OrphanID",
                table: "Narrations",
                column: "OrphanID",
                principalTable: "Orphans",
                principalColumn: "OrphanID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narrations_Orphans_OrphanID",
                table: "Narrations");

            migrationBuilder.DropColumn(
                name: "GuardianID",
                table: "Narrations");

            migrationBuilder.AlterColumn<int>(
                name: "OrphanID",
                table: "Narrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Narrations_Orphans_OrphanID",
                table: "Narrations",
                column: "OrphanID",
                principalTable: "Orphans",
                principalColumn: "OrphanID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
