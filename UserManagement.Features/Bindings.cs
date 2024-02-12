using UserManagement.Domain.Users.Entities;
using UserManagement.Features.Roles.ValueObjects;
using UserManagement.Features.Roles.Requests.CreateRole;

namespace UserManagement.Features
{
    public static class Bindings
    {
        public static void CreateBindings()
        {
            TinyMapper.Bind<CreateRoleRequest, Role>();
            TinyMapper.Bind<Role, RoleDto>();
        }
    }
}
