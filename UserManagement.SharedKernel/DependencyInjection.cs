using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Retornos.Filters;
using UserManagement.SharedKernel.Messaging;

namespace UserManagement.SharedKernel
{
    public static class DependencyInjection
    {
        public static MvcOptions AddFilters(this MvcOptions configuration)
        {
            configuration.Filters.Add<ExceptionFilter>();
            configuration.Filters.Add<ModelValidationFilter>();

            return configuration;
        }

        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddScoped<IMessaging, Messaging.Messaging>();
            return services;
        }
    }
}
