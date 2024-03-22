using UserManagement.SharedKernel.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Returns;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserManagement.SharedKernel.Retornos
{
    public abstract class StandardController : ControllerBase
    {
        public StandardController(IMediator mediador)
        {
            Mediator = mediador;
        }

        protected readonly IMediator Mediator;

        /// <summary>
        /// Passes the object of type <see cref="IRequest" /> to the mediator of type <see cref="IMediator"/> and returns the formatted response ready for the endpoint
        /// </summary>
        protected async Task<IActionResult> ProcessRequest<T>(IRequest<T> request)
        {
            var result = await Mediator.Send(request);

            var messaging = HttpContext.GetService<IMessaging>();
            var statusCode = (int)GetStatusCodeResult(messaging);

            var defaultResult = new ApiResult<T>(result, messaging.Messages);
            return StatusCode(statusCode, defaultResult);
        }

        private HttpStatusCode GetStatusCodeResult(IMessaging messaging)
        {
            if (messaging.HasErrors())
                return HttpStatusCode.InternalServerError;

            if (messaging.HasValidationFailures())
                return HttpStatusCode.BadRequest;

            return HttpStatusCode.OK;
        }
    }
}
