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

namespace UserManagement.Features.Roles.Requests.UpdateRole
{
    public class UpdateRoleHandler : FeatureHandler<UpdateRoleRequest, bool>
    {
        public UpdateRoleHandler(UserManagerDbContext context, IMessaging messaging, ILogger<FeatureHandler<UpdateRoleRequest, bool>> logger) : base(messaging, logger)
        {
            _context = context;
        }

        private readonly UserManagerDbContext _context;

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
            _requestedRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);

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
            _context.Roles.Update(_requestedRole);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                Messaging.ReturnErrorMessage("Failed to modify the specified role. Please try again later.");
            Messaging.AddInformationalMessage("Role updated successfully.");
        }
    }
}
