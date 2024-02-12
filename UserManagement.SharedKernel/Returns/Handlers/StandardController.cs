using UserManagement.SharedKernel.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Returns;

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
        protected async Task<ApiResult<T>> ProcessRequest<T>(IRequest<T> request)
        {
            var result = await Mediator.Send(request);
            return StandardResult(result);
        }

        /// <summary>
        /// Converts the passed object to the standard return format for endpoints
        /// </summary>
        protected ApiResult<T> StandardResult<T>(T data)
        {
            var messaging = HttpContext.GetService<IMessaging>();
            Response.StatusCode = (int)GetStatusCodeResult(messaging);

            return new ApiResult<T>(data, messaging.Messages);
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
