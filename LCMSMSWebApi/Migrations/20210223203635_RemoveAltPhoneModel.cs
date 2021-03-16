using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class RemoveAltPhoneModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltPhoneNumbers");

            migrationBuilder.AddColumn<string>(
                name: "AltPhone1",
                table: "Guardians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AltPhone2",
                table: "Guardians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AltPhone3",
                table: "Guardians",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltPhone1",
                table: "Guardians");

            migrationBuilder.DropColumn(
                name: "AltPhone2",
                table: "Guardians");

            migrationBuilder.DropColumn(
                name: "AltPhone3",
                table: "Guardians");

            migrationBuilder.CreateTable(
                name: "AltPhoneNumbers",
                columns: table => new
                {
                    AltPhoneNumberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuardianID = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltPhoneNumbers", x => x.AltPhoneNumberID);
                    table.ForeignKey(
                        name: "FK_AltPhoneNumbers_Guardians_GuardianID",
                        column: x => x.GuardianID,
                        principalTable: "Guardians",
                        principalColumn: "GuardianID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AltPhoneNumbers_GuardianID",
                table: "AltPhoneNumbers",
                column: "GuardianID");
        }
    }
}
