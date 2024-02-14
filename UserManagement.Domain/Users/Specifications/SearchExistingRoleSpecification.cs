using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users.Entities;
using UserManagement.SharedKernel;

namespace UserManagement.Domain.Users.Specifications
{
    public class SearchExistingRoleSpecification : BaseSpecification<Role>
    {
        public SearchExistingRoleSpecification(Guid updatedRoleId, string name) : base(x => x.Id != updatedRoleId && x.Name.ToLower().Trim() == name.ToLower().Trim() && !x.Deleted)
        {
            
        }
    }
}
