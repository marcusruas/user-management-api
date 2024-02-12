using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Features.Roles.ValueObjects;

namespace UserManagement.Features.Roles.Requests.GetRoleById
{
    public class GetRoleByIdRequest : IRequest<RoleDto>
    {
        public GetRoleByIdRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
