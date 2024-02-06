using AutenticacaoMarcus.Domain.Grupos.Entities;
using AutenticacaoMarcus.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcus.Features.Grupos.Requests.DeletarGrupo;
using AutenticacaoMarcus.Infrastructure.Context;
using AutenticacaoMarcus.SharedKernel.Mensageria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features.Grupos.Requests.AlterarGrupo
{
    public class AlterarGrupoHandler : FeatureHandler<AlterarGrupoRequest, bool>
    {
        public AlterarGrupoHandler(AutenticacaoDbContext context, IMensageria mensageria, ILogger<FeatureHandler<AlterarGrupoRequest, bool>> logger) : base(mensageria, logger)
        {
            _context = context;
        }

        private readonly AutenticacaoDbContext _context;

        private Grupo _grupoSolicitado;

        public override async Task<bool> HandleRequest(AlterarGrupoRequest request, CancellationToken cancellationToken)
        {
            await ValidarGrupoExistente(request);

            AplicarAlteracoesGrupo(request);

            await SalvarAlteracoes();

            return true;
        }

        private async Task ValidarGrupoExistente(AlterarGrupoRequest request)
        {
            _grupoSolicitado = await _context.Grupos.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Excluido);

            if (_grupoSolicitado is null)
                Mensageria.RetornarMensagemFalhaValidacao("Grupo informado não existe.");
        }

        private void AplicarAlteracoesGrupo(AlterarGrupoRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Nome))
                _grupoSolicitado.Nome = request.Nome;
            if (!string.IsNullOrWhiteSpace(request.Descricao))
                _grupoSolicitado.Descricao = request.Descricao;
        }

        private async Task SalvarAlteracoes()
        {
            _context.Grupos.Update(_grupoSolicitado);
            var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas <= 0)
                Mensageria.RetornarMensagemErro("Falha ao alterar o grupo indicado. Tente novamente mais tarde.");
            Mensageria.AdicionarMensagemInformativa("Grupo alterado com sucesso.");
        }
    }
}
