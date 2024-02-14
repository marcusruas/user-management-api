using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users.Entities;
using UserManagement.Features.Roles.Requests.CreateRole;
using UserManagement.Features.Roles.ValueObjects;

namespace UserManagement.Features.Roles
{
    public class RolesBindings
    {
        public static void CreateBindings()
        {
            TinyMapper.Bind<CreateRoleRequest, Role>();
            TinyMapper.Bind<Role, RoleDto>();
        }
    }
}
