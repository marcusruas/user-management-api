using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;
using UserManagement.Features.Roles.ValueObjects;

namespace UserManagement.Features.Roles.Requests.GetAllRoles
{
    public class GetAllRolesHandler : FeatureHandler<GetAllRolesRequest, IEnumerable<RoleDto>>
    {
        public GetAllRolesHandler(UserManagerDbContext context, IMessaging messaging, ILogger<FeatureHandler<GetAllRolesRequest, IEnumerable<RoleDto>>> logger) : base(messaging, logger)
        {
            _context = context;
        }

        private readonly UserManagerDbContext _context;

        public override async Task<IEnumerable<RoleDto>> HandleRequest(GetAllRolesRequest request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.Where(x => !x.Deleted).ToListAsync();

            if (roles is null || !roles.Any())
                return new List<RoleDto>();

            return roles.Select(x => TinyMapper.Map<RoleDto>(x));
        }
    }
}
