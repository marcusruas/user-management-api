using AutenticacaoMarcus.SharedKernel.Mensageria;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutenticacaoMarcus.Retornos.Filtros
{
    internal class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var _mensageria = context.HttpContext.ObterServico<IMensageria>();
                var errosModelo = context.ModelState.Values.SelectMany(x => x.Errors);

                foreach (var erro in errosModelo)
                    _mensageria.AdicionarMensagemFalhaValidacao(erro.ErrorMessage);

                if (_mensageria.PossuiFalhasValidacao())
                    throw new FalhaValidacaoException();
            }

            base.OnActionExecuting(context);
        }


    }
}
