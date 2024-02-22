using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Features.Users.Requests.CreateUser;
using UserManagement.SharedKernel.Returns;

namespace UserManagement.Api.Controllers
{
    [Route("api/users")]
    [Produces("application/json")]
    public class UsersController : StandardController
    {
        public UsersController(IMediator mediador) : base(mediador) { }

        [HttpPost]
        public async Task<ApiResult<bool>> CreateUser([FromBody] CreateUserRequest request)
            => await ProcessRequest(request);
    }
}
