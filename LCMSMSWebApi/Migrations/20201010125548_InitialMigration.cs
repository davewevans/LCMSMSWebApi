using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
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
                name: "OrphanProfilePics",
                columns: table => new
                {
                    OrphanProfilePicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrphanID = table.Column<int>(nullable: false),
                    PicUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanProfilePics", x => x.OrphanProfilePicID);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Academics_Orphans_OrphanID",
                        column: x => x.OrphanID,
                        principalTable: "Orphans",
                        principalColumn: "OrphanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    OrphanID = table.Column<int>(nullable: false),
                    SponsorID = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_Documents_Orphans_OrphanID",
                        column: x => x.OrphanID,
                        principalTable: "Orphans",
                        principalColumn: "OrphanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Sponsors_SponsorID",
                        column: x => x.SponsorID,
                        principalTable: "Sponsors",
                        principalColumn: "SponsorID",
                        onDelete: ReferentialAction.Cascade);
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
                    OrphanID = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
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
                name: "IX_Academics_OrphanID",
                table: "Academics",
                column: "OrphanID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OrphanID",
                table: "Documents",
                column: "OrphanID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_SponsorID",
                table: "Documents",
                column: "SponsorID");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DbUpdates");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Narrations");

            migrationBuilder.DropTable(
                name: "OrphanProfilePics");

            migrationBuilder.DropTable(
                name: "OrphanSponsors");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "Orphans");

            migrationBuilder.DropTable(
                name: "Guardians");
        }
    }
}
