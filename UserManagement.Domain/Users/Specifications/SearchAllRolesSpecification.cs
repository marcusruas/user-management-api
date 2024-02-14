using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users.Entities;
using UserManagement.SharedKernel;

namespace UserManagement.Domain.Users.Specifications
{
    public class SearchAllRolesSpecification : PaginatedBaseSpecification<Role>
    {
        public SearchAllRolesSpecification(int page, int recordsPerPage) : base(page, recordsPerPage) { }
    }
}
