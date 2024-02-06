using AutenticacaoMarcus.Features.Grupos.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features.Grupos.Requests.ObterGrupoPorId
{
    public class ObterGrupoPorIdRequest : IRequest<GrupoDto>
    {
        public ObterGrupoPorIdRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
