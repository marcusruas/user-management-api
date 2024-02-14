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
using UserManagement.Infrastructure.Repositories.Roles;
using UserManagement.Domain.Users.Specifications;

namespace UserManagement.Features.Roles.Requests.GetRoleById
{
    public class GetRoleByIdHandler : FeatureHandler<GetRoleByIdRequest, RoleDto>
    {
        public GetRoleByIdHandler(IRolesRepository repository, IMessaging messaging, ILogger<FeatureHandler<GetRoleByIdRequest, RoleDto>> logger) : base(messaging, logger)
        {
            _repository = repository;
        }

        private readonly IRolesRepository _repository;

        private Role _requestedRole;

        public override async Task<RoleDto> HandleRequest(GetRoleByIdRequest request, CancellationToken cancellationToken)
        {
            await GetExistingRole(request);

            return TinyMapper.Map<RoleDto>(_requestedRole);
        }

        private async Task GetExistingRole(GetRoleByIdRequest request)
        {
            _requestedRole = await _repository.FirstOrDefaultAsync(new SearchRoleByIdSpecification(request.Id));

            if (_requestedRole is null)
                Messaging.ReturnValidationFailureMessage("The specified role does not exist.");
        }
    }
}
