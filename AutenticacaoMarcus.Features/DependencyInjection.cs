using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFeatures(this IServiceCollection servicos)
        {
            servicos.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return servicos;
        }
    }
}
