using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Models;
using LCMSMSWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IAuthService auth;
        private readonly IMapper mapper;

        public AuthController(ApplicationDbContext dbContext, IAuthService auth, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.auth = auth;
            this.mapper = mapper;
        }

        [HttpGet, Route("adduser")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {

            var newUser = new UserModel
            {
                FirstName = "Guest",
                LastName = "Guest",
                Email = "guest@gmail.com",
                CreatedAt = DateTime.Now
            };

            string password = "1L0veKenya";

            var passwordHashObj = auth.HashPasswordWithSalt(password);        

            newUser.PasswordHash = passwordHashObj.Hash;
            newUser.Salt = passwordHashObj.Salt;

            await dbContext.Users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPost, Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request.");
            }

            var userFromDb = dbContext.Users.SingleOrDefault(u => u.Email == user.Email);
            if (userFromDb == null) return StatusCode(StatusCodes.Status404NotFound);
            var passwordHash = userFromDb.PasswordHash;
            var salt = userFromDb.Salt;

            var passwordHashObj = auth.HashPasswordWithSalt(user.Password, userFromDb.Salt);

            if (!passwordHash.Equals(passwordHashObj.Hash)) return Unauthorized();

            //List<RoleModel> roles = (from ur in userFromDb.UsersRoles
            //                         select dbContext.Roles.SingleOrDefault(x => x.RoleID == ur.RoleID)).ToList();
            var claims = auth.GetClaims(userFromDb.Email);
            string tokenString = auth.GetJwtToken(claims);          

        
            await dbContext.SaveChangesAsync();

            var userDto = mapper.Map<UserDTO>(userFromDb);
            var authResponse = new AuthenticateResponseDTO(userDto, tokenString);
            return Ok(authResponse);
        }
    }
}
