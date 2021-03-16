using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class GuardianAltPhoneNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhone",
                table: "Guardians",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AltPhoneNumbers",
                columns: table => new
                {
                    AltPhoneNumberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(nullable: true),
                    GuardianID = table.Column<int>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltPhoneNumbers");

            migrationBuilder.DropColumn(
                name: "MainPhone",
                table: "Guardians");
        }
    }
}
