using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.Features.Grupos.Requests.CadastrarGrupo
{
    public class CadastrarGrupoRequest : IRequest<bool>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
