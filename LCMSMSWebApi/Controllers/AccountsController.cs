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
            IConfiguration configuration )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost, Route("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo newUser)
        {
            var user = new ApplicationUser { UserName = newUser.Email, Email = newUser.Email, FirstName = newUser.FirstName ?? "" };
            var result = await userManager.CreateAsync(user, newUser.Password);

            if (result.Succeeded)
            {
                return await BuildToken(newUser);
            }
            else
            {
                return BadRequest("Username or password is invalid");
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
                return BadRequest("Invalid login attempt");
            }
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
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim("myvalue", "whatever")
            };

            var identityUser = await userManager.FindByEmailAsync(userInfo.Email);
            var claimsDB = await userManager.GetClaimsAsync(identityUser);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

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
