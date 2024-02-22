using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;
using UserManagement.SharedKernel.Returns;

namespace UserManagement.SharedKernel.Retornos
{
    public abstract class FeatureHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public FeatureHandler(IMessaging messaging, ILogger<FeatureHandler<TRequest, TResponse>> logger)
        {
            Messaging = messaging;
            Logger = logger;
        }

        protected readonly IMessaging Messaging;
        protected readonly ILogger<FeatureHandler<TRequest, TResponse>> Logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleRequest(request, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex is MessagingException)
                    throw;

                var requestJson = JsonConvert.SerializeObject(request);
                Logger.LogError(ex, "The request to the handler {handler} failed. Request: {request}", GetType().Name, requestJson);
                throw;
            }
        }

        public abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);
    }
}
