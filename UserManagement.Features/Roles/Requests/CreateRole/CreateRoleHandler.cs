using UserManagement.Infrastructure.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;
using UserManagement.Domain.Users.Entities;
using UserManagement.Infrastructure.Repositories.Roles;
using UserManagement.Domain.Users.Specifications;

namespace UserManagement.Features.Roles.Requests.CreateRole
{
    public class CreateRoleHandler : FeatureHandler<CreateRoleRequest, bool>
    {
        public CreateRoleHandler(IRolesRepository repository, IMessaging messaging, ILogger<FeatureHandler<CreateRoleRequest, bool>> logger) : base(messaging, logger)
        {
            _repository = repository;
        }

        private readonly IRolesRepository _repository;

        public override async Task<bool> HandleRequest(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            await ValidateRole(request);
            await CreateRole(request);

            return true;
        }

        private async Task ValidateRole(CreateRoleRequest request)
        {
            var roleExists = await _repository.AnyAsync(new SearchRoleByNameSpecification(request.Name));

            if (roleExists)
                Messaging.ReturnValidationFailureMessage("The selected role already exists.");
        }

        private async Task CreateRole(CreateRoleRequest request)
        {
            var newRole = TinyMapper.Map<Role>(request);
            var affectedRows = await _repository.AddEntity(newRole);

            if (affectedRows <= 0)
                Messaging.ReturnErrorMessage("Failed to create the specified role. Please try again later.");
            Messaging.AddInformationalMessage("Role created successfully.");
        }
    }
}
