using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.SharedKernel.Retornos.Models
{
    public abstract class Tabela
    {
        public Tabela()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
            Excluido = false;
        }

        public Guid Id { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Excluido { get; set; }
    }
}
