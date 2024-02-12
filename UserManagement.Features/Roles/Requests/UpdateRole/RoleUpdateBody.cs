using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Features.Roles.Requests.UpdateRole
{
    public class RoleUpdateBody
    {
        [MaxLength(80, ErrorMessage = "The role name must contain a maximum of 80 characters.")]
        public string? Name { get; set; }
        [MaxLength(200, ErrorMessage = "The role description must contain a maximum of 200 characters.")]
        public string? Description { get; set; }
    }
}
