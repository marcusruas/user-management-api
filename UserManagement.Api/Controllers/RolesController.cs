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
    [Produces("application/json")]
    public class RolesController : StandardController
    {
        public RolesController(IMediator mediador) : base(mediador) { }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromQuery] GetAllRolesRequest request)
            => await ProcessRequest(request);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
            => await ProcessRequest(new GetRoleByIdRequest(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleUpdateBody request)
            => await ProcessRequest(new UpdateRoleRequest(id, request));

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
            => await ProcessRequest(request);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
            => await ProcessRequest(new DeleteRoleRequest(id));
    }
}