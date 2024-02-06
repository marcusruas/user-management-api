using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features.Grupos.Requests.AlterarGrupo
{
    public class AlteracaoGrupoBody
    {
        [MaxLength(80, ErrorMessage = "Nome do grupo deve conter no máximo 80 caractéres")]
        public string? Nome { get; set; }
        [MaxLength(200, ErrorMessage = "Descrição do grupo deve conter no máximo 200 caractéres")]
        public string? Descricao { get; set; }
    }
}
