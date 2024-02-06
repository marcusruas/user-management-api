using AutenticacaoMarcus.Domain.Grupos.Entities;
using AutenticacaoMarcus.Features.Grupos.Requests.DeletarGrupo;
using AutenticacaoMarcus.Features.Grupos.ValueObjects;
using AutenticacaoMarcus.Infrastructure.Context;
using AutenticacaoMarcus.SharedKernel.Mensageria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features.Grupos.Requests.ObterGrupoPorId
{
    public class ObterGrupoPorIdHandler : FeatureHandler<ObterGrupoPorIdRequest, GrupoDto>
    {
        public ObterGrupoPorIdHandler(AutenticacaoDbContext context, IMensageria mensageria, ILogger<FeatureHandler<ObterGrupoPorIdRequest, GrupoDto>> logger) : base(mensageria, logger)
        {
            _context = context;
        }

        private readonly AutenticacaoDbContext _context;

        private Grupo _grupoSolicitado;

        public override async Task<GrupoDto> HandleRequest(ObterGrupoPorIdRequest request, CancellationToken cancellationToken)
        {
            await ObterGrupoExistente(request);

            return TinyMapper.Map<GrupoDto>(_grupoSolicitado);
        }

        private async Task ObterGrupoExistente(ObterGrupoPorIdRequest request)
        {
            _grupoSolicitado = await _context.Grupos.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Excluido);

            if (_grupoSolicitado is null)
                Mensageria.RetornarMensagemFalhaValidacao("Grupo informado não existe.");
        }
    }
}
