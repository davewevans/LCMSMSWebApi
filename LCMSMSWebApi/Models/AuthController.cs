using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LCMSMSWebApi.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //[HttpPost, Route("login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login([FromBody] LoginDTO user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid request.");
        //    }

        //    var userFromDb = dbContext.Users.Include("UsersRoles").SingleOrDefault(u => u.Email == user.Email);
        //    if (userFromDb == null) return StatusCode(StatusCodes.Status404NotFound);
        //    var passwordHash = userFromDb.PasswordHash;
        //    var salt = userFromDb.Salt;

        //    var passwordHashObj = auth.HashPasswordWithSalt(user.Password, userFromDb.Salt);

        //    if (!passwordHash.Equals(passwordHashObj.Hash)) return Unauthorized();

        //    List<RoleModel> roles = (from ur in userFromDb.UsersRoles
        //                             select dbContext.Roles.SingleOrDefault(x => x.RoleID == ur.RoleID)).ToList();
        //    var claims = auth.GetClaimsWithRoles(userFromDb.Email, roles);
        //    string tokenString = auth.GetJwtToken(claims);
        //    var refreshToken = auth.GetRefreshToken(userFromDb.UserID);

        //    await dbContext.RefreshTokens.AddAsync(refreshToken);
        //    await dbContext.SaveChangesAsync();

        //    var userDto = mapper.Map<UserDTO>(userFromDb);
        //    var authResponse = new AuthenticateResponseDTO(userDto, tokenString, refreshToken.Token);
        //    return Ok(authResponse);
        //}
    }
}
