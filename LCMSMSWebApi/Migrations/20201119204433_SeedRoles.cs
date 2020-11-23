using Microsoft.EntityFrameworkCore.Migrations;

namespace LCMSMSWebApi.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string insertAdminRoleSql = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('79249653-33ba-4ff4-9d7e-6b58123b1584', 'Admin', 'ADMIN')";
            migrationBuilder.Sql(insertAdminRoleSql);

            string insertStaffRoleSql = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('55a8bc2a-70d7-4d5c-ba55-7e9bda0355ea', 'Staff', 'STAFF')";
            migrationBuilder.Sql(insertStaffRoleSql);

            string insertGuestRoleSql = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('43584d32-f65b-4fd9-95ce-b490c112edef', 'Guest', 'GUEST')";
            migrationBuilder.Sql(insertGuestRoleSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
