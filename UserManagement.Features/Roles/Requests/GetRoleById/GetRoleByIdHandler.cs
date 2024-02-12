using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Features.Roles.ValueObjects;
using UserManagement.SharedKernel.Messaging;
using UserManagement.Domain.Users.Entities;

namespace UserManagement.Features.Roles.Requests.GetRoleById
{
    public class GetRoleByIdHandler : FeatureHandler<GetRoleByIdRequest, RoleDto>
    {
        public GetRoleByIdHandler(UserManagerDbContext context, IMessaging messaging, ILogger<FeatureHandler<GetRoleByIdRequest, RoleDto>> logger) : base(messaging, logger)
        {
            _context = context;
        }

        private readonly UserManagerDbContext _context;

        private Role _requestedRole;

        public override async Task<RoleDto> HandleRequest(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            await GetExistingRole(request);

            return TinyMapper.Map<RoleDto>(_requestedRole);
        }

        private async Task GetExistingRole(GetRoleByIdRequest request)
        {
            _requestedRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);

            if (_requestedRole is null)
                Messaging.ReturnValidationFailureMessage("The specified role does not exist.");
        }
    }
}
