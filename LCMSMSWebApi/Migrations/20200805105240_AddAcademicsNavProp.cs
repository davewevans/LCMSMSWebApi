using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddAcademicsNavProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Academics_OrphanID",
                table: "Academics",
                column: "OrphanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Academics_Orphans_OrphanID",
                table: "Academics",
                column: "OrphanID",
                principalTable: "Orphans",
                principalColumn: "OrphanID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Academics_Orphans_OrphanID",
                table: "Academics");

            migrationBuilder.DropIndex(
                name: "IX_Academics_OrphanID",
                table: "Academics");
        }
    }
}
