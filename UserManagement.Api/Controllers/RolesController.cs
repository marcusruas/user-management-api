using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.SharedKernel.Returns;
using UserManagement.Features.Roles.ValueObjects;
using UserManagement.Features.Roles.Requests.GetRoleById;
using UserManagement.Features.Roles.Requests.UpdateRole;
using UserManagement.Features.Roles.Requests.GetAllRoles;
using UserManagement.Features.Roles.Requests.CreateRole;
using UserManagement.Features.Roles.Requests.DeleteRole;

namespace UserManagement.Api.Controllers
{
    [Route("api/roles")]
    public class RolesController : StandardController
    {
        public RolesController(IMediator mediador) : base(mediador) { }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<RoleDto>>> GetAllRoles()
            => await ProcessRequest(new GetAllRolesRequest());

        [HttpGet("{id}")]
        public async Task<ApiResult<RoleDto>> GetRoleById(Guid id)
            => await ProcessRequest(new GetRoleByIdRequest(id));

        [HttpPut("{id}")]
        public async Task<ApiResult<bool>> UpdateRole(Guid id, [FromBody] RoleUpdateBody request)
            => await ProcessRequest(new UpdateRoleRequest(id, request));

        [HttpPost]
        public async Task<ApiResult<bool>> CreateRole([FromBody] CreateRoleRequest request)
            => await ProcessRequest(request);

        [HttpDelete("{id}")]
        public async Task<ApiResult<bool>> DeleteRole(Guid id)
            => await ProcessRequest(new DeleteRoleRequest(id));
    }
}