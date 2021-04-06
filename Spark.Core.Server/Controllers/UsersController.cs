using Calculo.Server.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spark.Core.Server.Extensions;
using Spark.Core.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Spark.Core.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SparkDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public UsersController(SparkDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Users.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.ItemsPerPage);
            return await queryable.Paginate(pagination)
                .Select(x => new UserDTO() { UserID = x.Id, Email = x.Email }).ToListAsync();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDTO>>> Get()
        {
            return await context.Roles.Select(x => new RoleDTO() { RoleName = x.Name }).ToListAsync();
        }

        [HttpPost("assignrole")]
        public async Task<ActionResult> AssignRole(EditRoleDTO editRoleDto)
        {
            var user = await userManager.FindByIdAsync(editRoleDto.UserID);
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDto.RoleName));
            return NoContent();
        }

        [HttpPost("removerole")]
        public async Task<ActionResult> RemoveRole(EditRoleDTO editRoleDto)
        {
            var user = await userManager.FindByIdAsync(editRoleDto.UserID);
            await userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDto.RoleName));
            return NoContent();
        }

        [HttpDelete("removeuser/{id}")]
        public async Task<ActionResult> RemoveUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);

            return NoContent();
        }
    }
}
