using LCMSMSWebApi.Data;
using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration config;
        private readonly ApplicationDbContext dbContext;

        public AuthService(IConfiguration config, ApplicationDbContext dbContext)
        {
            this.config = config;
            this.dbContext = dbContext;
        }

        public PasswordHashModel HashPasswordWithSalt(string password, byte[] salt = null)
        {
            salt = salt == null ? GenerateRandomSalt() : salt;

            return new PasswordHashModel
            {
                Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8)),
                Salt = salt
            };
        }

        private byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public string GetJwtToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
             issuer: config["Tokens:Issuer"],
             audience: config["Tokens:Issuer"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public List<Claim> GetClaims(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Name, email)
            };

            //foreach (var r in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, r.Name));
            //}

            return claims;
        }

    }
}
