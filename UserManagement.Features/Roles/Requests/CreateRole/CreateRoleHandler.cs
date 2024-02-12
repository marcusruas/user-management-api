using UserManagement.Infrastructure.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;
using UserManagement.Domain.Users.Entities;

namespace UserManagement.Features.Roles.Requests.CreateRole
{
    public class CreateRoleHandler : FeatureHandler<CreateRoleRequest, bool>
    {
        public CreateRoleHandler(UserManagerDbContext context, IMessaging messaging, ILogger<FeatureHandler<CreateRoleRequest, bool>> logger) : base(messaging, logger)
        {
            _context = context;
        }

        private readonly UserManagerDbContext _context;

        public override async Task<bool> HandleRequest(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            ValidateRole(request);

            await CreateRole(request);

            return true;
        }

        private void ValidateRole(CreateRoleRequest request)
        {
            var roleExists = _context.Roles.Any(x => x.Name.Trim().ToUpper() == request.Name.Trim().ToUpper() && !x.Deleted);

            if (roleExists)
                Messaging.ReturnValidationFailureMessage("The selected role already exists.");
        }

        private async Task CreateRole(CreateRoleRequest request)
        {
            var newRole = TinyMapper.Map<Role>(request);

            await _context.Roles.AddAsync(newRole);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                Messaging.ReturnErrorMessage("Failed to create the specified role. Please try again later.");
            Messaging.AddInformationalMessage("Role created successfully.");
        }
    }
}
