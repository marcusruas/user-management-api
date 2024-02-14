using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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

namespace UserManagement.Features.Roles.Requests.UpdateRole
{
    public class UpdateRoleHandler : FeatureHandler<UpdateRoleRequest, bool>
    {
        public UpdateRoleHandler(IRolesRepository repository, IMessaging messaging, ILogger<FeatureHandler<UpdateRoleRequest, bool>> logger) : base(messaging, logger)
        {
            _repository = repository;
        }

        private readonly IRolesRepository _repository;

        private Role _requestedRole;

        public override async Task<bool> HandleRequest(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            await ValidateRole(request);

            ApplyRoleUpdates(request);

            await SaveUpdate();

            return true;
        }

        private async Task ValidateRole(UpdateRoleRequest request)
        {
            _requestedRole = await _repository.FirstOrDefaultAsync(new SearchExistingRoleSpecification(request.Id, request.Name));

            if (_requestedRole is null)
                Messaging.ReturnErrorMessage("The specified role does not exist.");
        }

        private void ApplyRoleUpdates(UpdateRoleRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
                _requestedRole.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Description))
                _requestedRole.Description = request.Description;
        }

        private async Task SaveUpdate()
        {
            var affectedRows = await _repository.UpdateEntity(_requestedRole);

            if (affectedRows <= 0)
                Messaging.ReturnErrorMessage("Failed to modify the specified role. Please try again later.");
            Messaging.AddInformationalMessage("Role updated successfully.");
        }
    }
}
