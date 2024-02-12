using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.SharedKernel.Returns
{
    public class InvalidServiceException : ControlledException
    {
        public InvalidServiceException(string servicoInvalido)
        {
            DefaultMessage = $"Não foi possível localizar um serviço registrado do tipo {servicoInvalido}";
        }

        public override int StatusCodeResult => 500;
        public override string DefaultMessage { get; }
    }
}
