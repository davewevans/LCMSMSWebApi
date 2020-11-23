using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [Authorize(Roles ="Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost, Route("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserCreation newUser)
        {
            var user = new ApplicationUser
            {
                UserName = newUser.Email,
                Email = newUser.Email,
                FirstName = newUser.FirstName ?? "",
                LastName = newUser.LastName ?? ""
            };
            var result = await userManager.CreateAsync(user, newUser.Password);

            // assign role
            await userManager.AddToRoleAsync(user, newUser.Role);

            // for the token
            var userInfo = new UserInfo
            {
                FirstName = newUser.FirstName,
                Email = newUser.Email,
            };

            if (result.Succeeded)
            {
                return await BuildToken(userInfo);
            }
            else
            {
                return BadRequest("Username or password is invalid. Or no first name was given.");
            }
        }  
          

        [HttpPost, Route("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await signInManager.PasswordSignInAsync(
                userInfo.Email,
                userInfo.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(userInfo);
            }
            else
            {
                return BadRequest("Username or password is incorrect.");
            }
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("resetPassword/{userId}")]
        public async Task<ActionResult> ResetPassword(string userId, [FromBody] PasswordResetDTO resetPasswordDTO)
        {
            var user = await userManager.FindByIdAsync(userId);
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, code, resetPasswordDTO.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Password has been reset.");
            }
            return BadRequest("Invalid password.");
            
        }

        [HttpGet, Route("RenewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> Renew()
        {
            var userInfo = new UserInfo()
            {
                Email = HttpContext.User.Identity.Name
            };

            return await BuildToken(userInfo);
        }

        private async Task<UserToken> BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>
            {               
                new Claim(ClaimTypes.Email, userInfo.Email),
            };

            var identityUser = await userManager.FindByEmailAsync(userInfo.Email);

            // Add firstname as claim
            if (!string.IsNullOrWhiteSpace(identityUser.FirstName))
                claims.Add(new Claim(ClaimTypes.Name, identityUser.FirstName));

            // Add claims (doesn't include roles)
            var claimsDB = await userManager.GetClaimsAsync(identityUser);
            claims.AddRange(claimsDB);

            // Add roles
            var roles = await userManager.GetRolesAsync(identityUser);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Expiration time span for the JWT token
            var expiration = DateTime.UtcNow.AddDays(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["Tokens:Issuer"],
                audience: configuration["Tokens:Issuer"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
