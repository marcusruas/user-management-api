using AutenticacaoMarcus.SharedKernel.Mensageria;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.SharedKernel.Retornos
{
    public abstract class FeatureHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public FeatureHandler(IMensageria mensageria, ILogger<FeatureHandler<TRequest, TResponse>> logger)
        {
            Mensageria = mensageria;
            Logger = logger;
        }

        protected readonly IMensageria Mensageria;
        protected readonly ILogger<FeatureHandler<TRequest, TResponse>> Logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleRequest(request, cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex is MensageriaException)
                    throw;

                var requestJson = JsonConvert.SerializeObject(request);
                Logger.LogError(ex, "A requisição para o handler {handler} falhou. Request: {request}", GetType().Name, requestJson);
                throw;
            }
        }

        public abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);
    }
}
