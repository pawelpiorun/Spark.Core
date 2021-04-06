using Spark.Core.Client.Services;
using Spark.Core.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/users";

        public UsersRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<PaginatedResponse<List<UserDTO>>> GetUsers(
            PaginationDTO pagination)
        {
            string queryUrl = url;
            string query = $"page={pagination.Page}&itemsPerPage={pagination.ItemsPerPage}";

            queryUrl += url.Contains("?") ? "&" : "?";
            queryUrl += query;

            var responseWrapper = await httpService.Get<List<UserDTO>>(queryUrl);
            if (!responseWrapper.Success)
            {
                throw new ApplicationException(await responseWrapper.GetBody());
            }

            var totalPagesHeader = responseWrapper.ResponseMessage.Headers.GetValues("totalPages").FirstOrDefault();
            var totalAmountOfPages = int.Parse(totalPagesHeader);

            var paginatedResponse = new PaginatedResponse<List<UserDTO>>()
            {
                Response = responseWrapper.Response.ToList(),
                TotalPages = totalAmountOfPages
            };
            return paginatedResponse;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var responseWrapper = await httpService.Get<List<RoleDTO>>($"{url}/roles");
            if (!responseWrapper.Success)
            {
                throw new ApplicationException(await responseWrapper.GetBody());
            }
            return responseWrapper.Response;
        }

        public async Task AssignRole(EditRoleDTO editRoleDto)
        {
            var response = await httpService.Post($"{url}/assignrole", editRoleDto);
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task RemoveRole(EditRoleDTO editRoleDto)
        {
            var response = await httpService.Post($"{url}/removerole", editRoleDto);
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task RemoveUser(string userId)
        {
            var response = await httpService.Delete($"{url}/removeuser/{userId}");
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }
    }
}
