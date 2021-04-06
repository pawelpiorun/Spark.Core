using Spark.Core.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public interface IUsersRepository
    {
        Task AssignRole(EditRoleDTO editRoleDto);
        Task<List<RoleDTO>> GetRoles();
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO pagination);
        Task RemoveRole(EditRoleDTO editRoleDto);
        Task RemoveUser(string userId);
    }
}
