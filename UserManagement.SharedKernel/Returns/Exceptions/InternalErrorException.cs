using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.SharedKernel.Returns
{
    public class InternalErrorException : MessagingException
    {
        public InternalErrorException()
        {
            DefaultMessage = "Ocorreu uma falha ao enviar as informações. Reinicie o navegador ou tente novamente mais tarde";
        }

        public override int StatusCodeResult => 500;
        public override string DefaultMessage { get; }
    }
}
