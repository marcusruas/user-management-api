using UserManagement.SharedKernel.Returns;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.SharedKernel.Extensions
{
    public static class AspNetCoreExtensions
    {
        /// <summary>
        /// Obtains a registered service from the <see cref="IServiceProvider"/>. If the same service has not been registered, an exception
        /// of type <see cref="InvalidServiceException"/> will be thrown
        /// </summary>
        /// <typeparam name="TService">Type of the registered service</typeparam>
        public static TService GetService<TService>(this IServiceProvider services)
        {
            var service = (TService)services.GetService(typeof(TService));

            if (service is null)
                throw new InvalidServiceException(typeof(TService).ToString());

            return service;
        }

        /// <summary>
        /// Obtains a registered service from the <see cref="HttpContext"/>. If the same service has not been registered, an exception
        /// of type <see cref="InvalidServiceException"/> will be thrown
        /// </summary>
        /// <typeparam name="TService">Type of the registered service</typeparam>
        public static TService GetService<TService>(this HttpContext context)
        {
            var service = (TService)context.RequestServices.GetService(typeof(TService));

            if (service is null)
                throw new InvalidServiceException(typeof(TService).ToString());

            return service;
        }
    }
}
