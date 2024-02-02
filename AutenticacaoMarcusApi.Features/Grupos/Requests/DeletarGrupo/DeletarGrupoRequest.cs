using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.Features.Grupos.Requests.DeletarGrupo
{
    public class DeletarGrupoRequest : IRequest<bool>
    {
        public DeletarGrupoRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
