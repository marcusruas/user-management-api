using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;

namespace UserManagement.Features.Users.Requests.CreateUser
{
    public class CreateUserHandler : FeatureHandler<CreateUserRequest, bool>
    {
        public CreateUserHandler(IMessaging messaging, ILogger<FeatureHandler<CreateUserRequest, bool>> logger) : base(messaging, logger) { }

        public override Task<bool> HandleRequest(CreateUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
