using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddedOrphanHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrphanHistory",
                columns: table => new
                {
                    OrphanHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrphanID = table.Column<int>(nullable: false),
                    GuardianID = table.Column<int>(nullable: true),
                    SponsorID = table.Column<int>(nullable: true),
                    UnassignedAt = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanHistory", x => x.OrphanHistoryID);
                    table.ForeignKey(
                        name: "FK_OrphanHistory_Orphans_OrphanID",
                        column: x => x.OrphanID,
                        principalTable: "Orphans",
                        principalColumn: "OrphanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrphanHistory_OrphanID",
                table: "OrphanHistory",
                column: "OrphanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrphanHistory");
        }
    }
}
