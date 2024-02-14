using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.SharedKernel.Messaging;
using UserManagement.Domain.Users.Entities;
using UserManagement.Infrastructure.Repositories.Roles;
using UserManagement.Domain.Users.Specifications;

namespace UserManagement.Features.Roles.Requests.DeleteRole
{
    public class DeleteRoleHandler : FeatureHandler<DeleteRoleRequest, bool>
    {
        public DeleteRoleHandler(IRolesRepository repository, IMessaging messaging, ILogger<FeatureHandler<DeleteRoleRequest, bool>> logger) : base(messaging, logger)
        {
            _repository = repository;
        }

        private readonly IRolesRepository _repository;

        private Role? _requestedRole;

        public override async Task<bool> HandleRequest(DeleteRoleRequest request, CancellationToken cancellationToken)
        {
            await ValidateRole(request);

            await DeleteRole(request);

            return true;
        }

        private async Task ValidateRole(DeleteRoleRequest request)
        {
            _requestedRole = await _repository.FirstOrDefaultAsync(new SearchRoleByIdSpecification(request.Id));

            if (_requestedRole is null)
                Messaging.ReturnErrorMessage("The specified role does not exist.");
        }

        private async Task DeleteRole(DeleteRoleRequest request)
        {
            _requestedRole.Deleted = true;

            var affectedRows = await _repository.DeleteEntity(_requestedRole);

            if (affectedRows <= 0)
                Messaging.ReturnErrorMessage("Failed to delete the specified role. Please try again later.");
            Messaging.AddInformationalMessage("Role deleted successfully.");
        }
    }
}
