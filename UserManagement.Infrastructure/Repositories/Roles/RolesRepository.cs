using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Context;
using UserManagement.SharedKernel.Messaging;
using UserManagement.SharedKernel.Persistence.SQL;

namespace UserManagement.Infrastructure.Repositories.Roles
{
    public class RolesRepository : StandardSqlRepository<UserManagerDbContext>, IRolesRepository
    {
        public RolesRepository(IMessaging messaging, IConfiguration configuration, ILogger<RolesRepository> logger, UserManagerDbContext context) : base(messaging, configuration, logger, context)
        {
        }
    }
}
