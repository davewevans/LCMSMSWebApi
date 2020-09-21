using LCMSMSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Services
{
    public interface IAuthService
    {
        PasswordHashModel HashPasswordWithSalt(string password, byte[] salt = null);

        string GetJwtToken(IEnumerable<Claim> claims);

        List<Claim> GetClaims(string email);

    }
}
