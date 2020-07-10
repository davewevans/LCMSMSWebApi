using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class RenameIdToID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuardianId",
                table: "Orphans",
                newName: "GuardianID");

            migrationBuilder.RenameColumn(
                name: "OrphanId",
                table: "Orphans",
                newName: "OrphanID");

            migrationBuilder.RenameColumn(
                name: "OrphanId",
                table: "Academics",
                newName: "OrphanID");

            migrationBuilder.RenameColumn(
                name: "AcademicId",
                table: "Academics",
                newName: "AcademicID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuardianID",
                table: "Orphans",
                newName: "GuardianId");

            migrationBuilder.RenameColumn(
                name: "OrphanID",
                table: "Orphans",
                newName: "OrphanId");

            migrationBuilder.RenameColumn(
                name: "OrphanID",
                table: "Academics",
                newName: "OrphanId");

            migrationBuilder.RenameColumn(
                name: "AcademicID",
                table: "Academics",
                newName: "AcademicId");
        }
    }
}
