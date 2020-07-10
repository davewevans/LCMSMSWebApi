using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class AddAcademicEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academics",
                columns: table => new
                {
                    AcademicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<string>(nullable: true),
                    KCPE = table.Column<string>(nullable: true),
                    KCSE = table.Column<string>(nullable: true),
                    School = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OrphanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academics", x => x.AcademicId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Academics");
        }
    }
}
