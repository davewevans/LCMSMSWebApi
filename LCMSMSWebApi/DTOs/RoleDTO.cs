using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCMSMSWebApi.DTOs
{
    public class RoleDTO
    {
        public string RoleName { get; set; }

        public string DisplayName { get; set; }

        public string RoleDescription { get; set; }


        public static string GetDisplayNameByRoleName(string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return "Administrator";
                case "Staff":
                    return "Staff";
                case "Guest":
                    return "Guest";
                case "NarrationApprover":
                    return "Narration Approver";
                case "OrphanReadWrite":
                    return "Orphan Portal";
                case "GuardianReadWrite":
                    return "Guardian Portal";
                case "SponsorReadWrite":
                    return "Sponsor Portal";
                case "OrphanReadOnly":
                    return "Orphan Portal - Read Only";
                case "GuardianReadOnly":
                    return "Guardian Portal - Read Only";
                case "SponsorReadOnly":
                    return "Sponsor Portal - Read Only";
            }

            return roleName;
        }

        public static string GetDecscriptionByRoleName(string roleName)
        {
            switch (roleName)
            {
                case "Admin":
                    return "Administrator has full access. Admins can create and edit users, assign roles to users, approved narration, and have full access to the entire app.";
                case "Staff":
                    return "Staff has full access to all of the portals (Orphan, Guardian, Sponsor). Staff can make changes to any of the records. Staff cannot add or edit users.";
                case "Guest":
                    return "Guest are allowed to read the data in all of the portals. Guest cannot make any changes whatesoever.";
                case "NarrationApprover":
                    return "Narration Approver has permission to approve any narration submitted by any user.";
                case "OrphanReadWrite":
                    return "This role gives users full read/write access to the Orphan portal.";
                case "GuardianReadWrite":
                    return "This role gives users full read/write access to the Guardian portal.";
                case "SponsorReadWrite":
                    return "This role gives users full read/write access to the Sponsor portal.";
                case "OrphanReadOnly":
                    return "This role allows users to read the data in the Orphan protal, but does not grant permission to make changes.";
                case "GuardianReadOnly":
                    return "This role allows users to read the data in the Guardian protal, but does not grant permission to make changes.";
                case "SponsorReadOnly":
                    return "This role allows users to read the data in the Sponsor protal, but does not grant permission to make changes.";
            }

            return roleName;
        }
    }
}
