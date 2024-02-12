using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Features.Roles.Requests.UpdateRole
{
    public class UpdateRoleRequest : IRequest<bool>
    {
        public UpdateRoleRequest(Guid id, RoleUpdateBody request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
