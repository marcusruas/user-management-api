using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Context;
using UserManagement.SharedKernel.Persistence.SQL;

namespace UserManagement.Infrastructure.Repositories.Roles
{
    public interface IRolesRepository : IStandardSqlRepository<UserManagerDbContext>
    {
    }
}
