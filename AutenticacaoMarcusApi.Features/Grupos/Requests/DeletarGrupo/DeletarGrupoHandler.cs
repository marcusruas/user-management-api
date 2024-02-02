using AutenticacaoMarcusApi.Domain.Grupos.Entities;
using AutenticacaoMarcusApi.Infrastructure.Context;
using AutenticacaoMarcusApi.SharedKernel.Mensageria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.Features.Grupos.Requests.DeletarGrupo
{
    public class DeletarGrupoHandler : FeatureHandler<DeletarGrupoRequest, bool>
    {
        public DeletarGrupoHandler(AutenticacaoDbContext context, IMensageria mensageria, ILogger<FeatureHandler<DeletarGrupoRequest, bool>> logger) : base(mensageria, logger)
        {
            _context = context;
        }

        private readonly AutenticacaoDbContext _context;

        private Grupo? _grupoSolicitado;

        public override async Task<bool> HandleRequest(DeletarGrupoRequest request, CancellationToken cancellationToken)
        {
            await ValidarGrupoExistente(request);

            await ExcluirGrupo(request);

            return true;
        }

        private async Task ValidarGrupoExistente(DeletarGrupoRequest request)
        {
            _grupoSolicitado = await _context.Grupos.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Excluido);

            if (_grupoSolicitado is null)
                Mensageria.RetornarMensagemFalhaValidacao("Grupo informado não existe.");
        }

        private async Task ExcluirGrupo(DeletarGrupoRequest request)
        {
            _grupoSolicitado.Excluido = true;

            _context.Grupos.Update(_grupoSolicitado);
            var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas <= 0)
                Mensageria.RetornarMensagemErro("Falha ao excluir o grupo indicado. Tente novamente mais tarde.");
            Mensageria.AdicionarMensagemInformativa("Grupo excluído com sucesso.");
        }
    }
}
