using AutenticacaoMarcus.Retornos.Filtros;
using AutenticacaoMarcus.SharedKernel.Mensageria;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.SharedKernel
{
    public static class DependencyInjection
    {
        public static MvcOptions AdicionarFiltros(this MvcOptions configuracoes)
        {
            configuracoes.Filters.Add<ExceptionFilter>();
            configuracoes.Filters.Add<ModelValidationFilter>();

            return configuracoes;
        }

        public static IServiceCollection AdicionarMensageria(this IServiceCollection servicos)
        {
            servicos.AddScoped<IMensageria, Mensageria.Mensageria>();
            return servicos;
        }
    }
}
