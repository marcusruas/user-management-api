﻿using AutenticacaoMarcus.SharedKernel.Retornos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Domain.Grupos.Entities
{
    public class Grupo : Tabela
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}