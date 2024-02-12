using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManagement.SharedKernel.Messaging;
using UserManagement.SharedKernel.Returns;

namespace UserManagement.Retornos.Filters
{
    internal class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var messaging = context.HttpContext.GetService<IMessaging>();
                var modelErrors = context.ModelState.Values.SelectMany(x => x.Errors);

                foreach (var errors in modelErrors)
                    messaging.AddValidationFailureMessage(errors.ErrorMessage);

                if (messaging.HasValidationFailures())
                    throw new ValidationFailureException();
            }

            base.OnActionExecuting(context);
        }


    }
}
