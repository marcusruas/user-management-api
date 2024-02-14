using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Features.Roles.ValueObjects;

namespace UserManagement.Features.Roles.Requests.GetAllRoles
{
    public class GetAllRolesRequest : IRequest<PaginatedList<RoleDto>>
    {
        [Required(ErrorMessage = "Number of the page is required")]
        public int? Page { get; set; }
        [Required(ErrorMessage = "Number of the records per page is required")]
        public int? RecordsPerPage { get; set; }
    }
}
