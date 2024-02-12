using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Features.Roles.ValueObjects;

namespace UserManagement.Features.Roles.Requests.GetAllRoles
{
    public class GetAllRolesRequest : IRequest<IEnumerable<RoleDto>>
    {
    }
}
