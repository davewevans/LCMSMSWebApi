using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddProfilePicNameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Orphans_OrphanID",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "OrphanProfilePics");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_OrphanID",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "OrphanID",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProfilePictureID",
                table: "Orphans");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicFileName",
                table: "Orphans",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrphanPictures",
                columns: table => new
                {
                    OrphanID = table.Column<int>(nullable: false),
                    PictureID = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanPictures", x => new { x.OrphanID, x.PictureID });
                    table.ForeignKey(
                        name: "FK_OrphanPictures_Orphans_OrphanID",
                        column: x => x.OrphanID,
                        principalTable: "Orphans",
                        principalColumn: "OrphanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrphanPictures_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrphanPictures_PictureID",
                table: "OrphanPictures",
                column: "PictureID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrphanPictures");

            migrationBuilder.DropColumn(
                name: "ProfilePicFileName",
                table: "Orphans");

            migrationBuilder.AddColumn<int>(
                name: "OrphanID",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureID",
                table: "Orphans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrphanProfilePics",
                columns: table => new
                {
                    OrphanProfilePicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrphanID = table.Column<int>(type: "int", nullable: false),
                    PicUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanProfilePics", x => x.OrphanProfilePicID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_OrphanID",
                table: "Pictures",
                column: "OrphanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Orphans_OrphanID",
                table: "Pictures",
                column: "OrphanID",
                principalTable: "Orphans",
                principalColumn: "OrphanID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
