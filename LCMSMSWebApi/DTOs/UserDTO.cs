using System.Collections.Generic;

namespace LCMSMSWebApi.DTOs
{
    public class UserDTO
    {
        public string UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<RoleDTO> Roles { get; set; }
    }
}
