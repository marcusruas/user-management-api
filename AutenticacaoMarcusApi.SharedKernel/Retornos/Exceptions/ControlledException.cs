﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AutenticacaoMarcusApi.SharedKernel.Retornos
{
    public abstract class ControlledException : Exception
    {
        public abstract int CodigoRetorno { get; }
        public abstract string MensagemPadrao { get; }
    }
}
