using LCMSMSWebApi.Data;
using LCMSMSWebApi.DTOs;
using LCMSMSWebApi.Helpers;
using LCMSMSWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            // Accounts to exclude from list in admin view
            // To prevent account from being edited.
            string[] exclude = { "paul.bolden@digipathways.org", "admin@lcm.com" };

            var queryable = context.Users.Where(x => !exclude.Contains(x.Email.ToLower())).AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, paginationDTO.RecordsPerPage);
            var users = await queryable.Paginate(paginationDTO)
                .Select(x => new UserDTO
                {
                    Email = x.Email,
                    UserID = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }).ToListAsync();

            // add role for each user
            foreach (var user in users)
            {                
                user.Roles = await FindUserRolesAsync(user.UserID);
            }

            return Ok(users);
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDTO>>> Get()
        {
            return await context.Roles
                .Select(x => new RoleDTO 
                { 
                    RoleName = x.Name,
                    DisplayName = RoleDTO.GetDisplayNameByRoleName(x.Name),
                    RoleDescription = RoleDTO.GetDecscriptionByRoleName(x.Name)
                    
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userDto = new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            userDto.Roles = await FindUserRolesAsync(id);

            return Ok(userDto);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UserDTO userDto)
        {
            var userFromDb = await userManager.FindByIdAsync(id);

            userFromDb.FirstName = string.IsNullOrWhiteSpace(userDto.FirstName)
                ? userFromDb.FirstName : userDto.FirstName;

            userFromDb.LastName = string.IsNullOrWhiteSpace(userDto.LastName)
                ? userFromDb.LastName : userDto.LastName;

            userFromDb.Email = string.IsNullOrWhiteSpace(userDto.Email)
                ? userFromDb.Email : userDto.Email;

            // Add user to role. If already in role, do nothing.           
            foreach (var role in userDto.Roles)
            {
                bool isAlreadyInRole = await userManager.IsInRoleAsync(userFromDb, role.RoleName);
                if (!isAlreadyInRole)
                {
                    await userManager.AddToRoleAsync(userFromDb, role.RoleName);
                }
            }

            // Remove user from existing role if role is not selected
            var existingRoles = await userManager.GetRolesAsync(userFromDb);
            foreach (var role in existingRoles)
            {
                bool keepInRole = userDto.Roles.Any(x => x.RoleName.Equals(role));
                if (!keepInRole)
                {
                    await userManager.RemoveFromRoleAsync(userFromDb, role);
                }
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var userToDelte = await userManager.FindByIdAsync(id);

            if (userToDelte == null)
            {
                return NotFound();
            }

            await userManager.DeleteAsync(userToDelte);
            return NoContent();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("assignRole")]
        public async Task<ActionResult> AssignRole(EditRoleDTO editRoleDTO)
        {
            var user = await userManager.FindByIdAsync(editRoleDTO.UserId);
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));
            return NoContent();
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("removeRole")]
        public async Task<ActionResult> RemoveRole(EditRoleDTO editRoleDTO)
        {
            var user = await userManager.FindByIdAsync(editRoleDTO.UserId);
            await userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDTO.RoleName));
            return NoContent();
        }

        private async Task<List<RoleDTO>> FindUserRolesAsync(string userId)
        {
            var userFromDb = context.Users.FirstOrDefault(x => x.Id == userId);
            var roles = await userManager.GetRolesAsync(userFromDb);
            var rolesList = new List<RoleDTO>();           
            foreach (var role in roles)
            {
                rolesList.Add(new RoleDTO
                {
                    RoleName = role,
                    DisplayName = RoleDTO.GetDisplayNameByRoleName(role),
                    RoleDescription = RoleDTO.GetDecscriptionByRoleName(role)
                });
            }
            return rolesList;
        }
    }
}
