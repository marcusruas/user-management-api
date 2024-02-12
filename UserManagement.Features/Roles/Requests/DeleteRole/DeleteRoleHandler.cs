using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.SharedKernel.Messaging;
using UserManagement.Domain.Users.Entities;

namespace UserManagement.Features.Roles.Requests.DeleteRole
{
    public class DeleteRoleHandler : FeatureHandler<DeleteRoleRequest, bool>
    {
        public DeleteRoleHandler(UserManagerDbContext context, IMessaging messaging, ILogger<FeatureHandler<DeleteRoleRequest, bool>> logger) : base(messaging, logger)
        {
            _context = context;
        }

        private readonly UserManagerDbContext _context;

        private Role? _requestedRole;

        public override async Task<bool> HandleRequest(DeleteRoleRequest request, CancellationToken cancellationToken)
        {
            await ValidateRole(request);

            await DeleteRole(request);

            return true;
        }

        private async Task ValidateRole(DeleteRoleRequest request)
        {
            _requestedRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);

            if (_requestedRole is null)
                Messaging.ReturnErrorMessage("The specified role does not exist.");
        }

        private async Task DeleteRole(DeleteRoleRequest request)
        {
            _requestedRole.Deleted = true;

            _context.Roles.Update(_requestedRole);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                Messaging.ReturnErrorMessage("Failed to delete the specified role. Please try again later.");
            Messaging.AddInformationalMessage("Role deleted successfully.");
        }
    }
}
