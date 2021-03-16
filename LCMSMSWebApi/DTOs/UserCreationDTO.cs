using System.Collections.Generic;

namespace LCMSMSWebApi.DTOs
{
    public class UserCreationDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<RoleDTO> Roles { get; set; }
    }
}
