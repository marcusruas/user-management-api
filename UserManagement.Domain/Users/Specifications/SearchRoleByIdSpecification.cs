using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users.Entities;
using UserManagement.SharedKernel;

namespace UserManagement.Domain.Users.Specifications
{
    public class SearchRoleByIdSpecification : BaseSpecification<Role>
    {
        public SearchRoleByIdSpecification(Guid id) : base(x => x.Id == id && !x.Deleted) { }
    }
}
