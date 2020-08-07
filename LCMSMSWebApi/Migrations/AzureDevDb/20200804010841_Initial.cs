using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations.AzureDevDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academics",
                columns: table => new
                {
                    AcademicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<string>(maxLength: 30, nullable: true),
                    KCPE = table.Column<string>(maxLength: 30, nullable: true),
                    KCSE = table.Column<string>(maxLength: 30, nullable: true),
                    School = table.Column<string>(maxLength: 255, nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OrphanID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academics", x => x.AcademicID);
                });

            migrationBuilder.CreateTable(
                name: "DbUpdates",
                columns: table => new
                {
                    DbUpdateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrphansUpdateTimeStamp = table.Column<DateTime>(nullable: false),
                    AcademicsUpdateTimeStamp = table.Column<DateTime>(nullable: false),
                    GuardiansUpdateTimeStamp = table.Column<DateTime>(nullable: false),
                    NarrationsUpdateTimeStamp = table.Column<DateTime>(nullable: false),
                    SponsorsUpdateTimeStamp = table.Column<DateTime>(nullable: false),
                    PicturesUpdateTimeStamp = table.Column<DateTime>(nullable: false),
                    DateTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbUpdates", x => x.DbUpdateId);
                });

            migrationBuilder.CreateTable(
                name: "Guardians",
                columns: table => new
                {
                    GuardianID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guardians", x => x.GuardianID);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    SponsorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 30, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    MainPhone = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.SponsorID);
                });

            migrationBuilder.CreateTable(
                name: "Orphans",
                columns: table => new
                {
                    OrphanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Gender = table.Column<string>(maxLength: 15, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    LCMStatus = table.Column<string>(maxLength: 30, nullable: true),
                    ProfileNumber = table.Column<string>(maxLength: 30, nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    GuardianID = table.Column<int>(nullable: true),
                    ProfilePictureID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orphans", x => x.OrphanID);
                    table.ForeignKey(
                        name: "FK_Orphans_Guardians_GuardianID",
                        column: x => x.GuardianID,
                        principalTable: "Guardians",
                        principalColumn: "GuardianID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Narrations",
                columns: table => new
                {
                    NarrationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(maxLength: 255, nullable: false),
                    Note = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OrphanID = table.Column<int>(nullable: true),
                    GuardianID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narrations", x => x.NarrationID);
                    table.ForeignKey(
                        name: "FK_Narrations_Orphans_OrphanID",
                        column: x => x.OrphanID,
                        principalTable: "Orphans",
                        principalColumn: "OrphanID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrphanSponsors",
                columns: table => new
                {
                    OrphanID = table.Column<int>(nullable: false),
                    SponsorID = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanSponsors", x => new { x.OrphanID, x.SponsorID });
                    table.ForeignKey(
                        name: "FK_OrphanSponsors_Orphans_OrphanID",
                        column: x => x.OrphanID,
                        principalTable: "Orphans",
                        principalColumn: "OrphanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrphanSponsors_Sponsors_SponsorID",
                        column: x => x.SponsorID,
                        principalTable: "Sponsors",
                        principalColumn: "SponsorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureFileName = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OrphanID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureID);
                    table.ForeignKey(
                        name: "FK_Pictures_Orphans_OrphanID",
                        column: x => x.OrphanID,
                        principalTable: "Orphans",
                        principalColumn: "OrphanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Narrations_OrphanID",
                table: "Narrations",
                column: "OrphanID");

            migrationBuilder.CreateIndex(
                name: "IX_Orphans_GuardianID",
                table: "Orphans",
                column: "GuardianID");

            migrationBuilder.CreateIndex(
                name: "IX_OrphanSponsors_SponsorID",
                table: "OrphanSponsors",
                column: "SponsorID");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_OrphanID",
                table: "Pictures",
                column: "OrphanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academics");

            migrationBuilder.DropTable(
                name: "DbUpdates");

            migrationBuilder.DropTable(
                name: "Narrations");

            migrationBuilder.DropTable(
                name: "OrphanSponsors");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "Orphans");

            migrationBuilder.DropTable(
                name: "Guardians");
        }
    }
}
