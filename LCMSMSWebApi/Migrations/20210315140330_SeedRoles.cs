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

            string insertRole1 = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('f925a597-b0d8-4cef-8be4-4c7ce83f2e3b', 'NarrationApprover', 'NARRATIONAPPROVER')";
            migrationBuilder.Sql(insertRole1);

            string insertRole2 = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('aabba1b2-2b61-447c-b7a7-fa36423e35fe', 'OrphanReadWrite', 'ORPHANREADWRITE')";
            migrationBuilder.Sql(insertRole2);

            string insertRole3 = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('706f4f00-d1f1-4f34-a915-1279c55ee504', 'GuardianReadWrite', 'GUARDIANREADWRITE')";
            migrationBuilder.Sql(insertRole3);

            string insertRole4 = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('4ca11ca3-08ba-4fb9-b24c-45a507ed59f7', 'SponsorReadWrite', 'SPONSORREADWRITE')";
            migrationBuilder.Sql(insertRole4);

            string insertRole5 = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('49c3c311-c3a5-4590-a613-632bf4c46b5f', 'OrphanReadOnly', 'ORPHANREADONLY')";
            migrationBuilder.Sql(insertRole5);

            string insertRole6 = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('4f5e5daa-ed00-4f32-8137-893d22303750', 'GuardianReadOnly', 'GUARDIANREADONLY')";
            migrationBuilder.Sql(insertRole6);

            string insertRole7 = @"INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES('a1077a7e-8663-4af7-a525-06b687ad3164', 'SponsorReadOnly', 'SPONSORREADONLY')";
            migrationBuilder.Sql(insertRole7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
