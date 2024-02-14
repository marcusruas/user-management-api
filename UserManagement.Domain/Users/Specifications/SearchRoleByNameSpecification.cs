using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users.Entities;
using UserManagement.SharedKernel;

namespace UserManagement.Domain.Users.Specifications
{
    public class SearchRoleByNameSpecification : BaseSpecification<Role>
    {
        public SearchRoleByNameSpecification(string name) : base (x => x.Name.ToLower().Trim() == name.ToLower().Trim() && !x.Deleted) { }
    }
}
