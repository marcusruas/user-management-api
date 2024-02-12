using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Features.Roles.Requests.DeleteRole
{
    public class DeleteRoleRequest : IRequest<bool>
    {
        public DeleteRoleRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
