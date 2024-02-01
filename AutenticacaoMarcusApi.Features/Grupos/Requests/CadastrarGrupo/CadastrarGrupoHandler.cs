using AutenticacaoMarcusApi.Domain.Grupos.Entities;
using AutenticacaoMarcusApi.Infrastructure.Context;
using AutenticacaoMarcusApi.SharedKernel.Mensageria;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.Features.Grupos.Requests.CadastrarGrupo
{
    public class CadastrarGrupoHandler : FeatureHandler<CadastrarGrupoRequest, bool>
    {
        public CadastrarGrupoHandler(AutenticacaoDbContext context, IMensageria mensageria, ILogger<FeatureHandler<CadastrarGrupoRequest, bool>> logger) : base(mensageria, logger)
        {
            _context = context;
        }

        private readonly AutenticacaoDbContext _context;

        public override async Task<bool> HandleRequest(CadastrarGrupoRequest request, CancellationToken cancellationToken)
        {
            ValidarGrupoExistente(request);

            await CadastrarGrupo(request);

            return true;
        }

        private void ValidarGrupoExistente(CadastrarGrupoRequest request)
        {
            var grupoExiste = _context.Grupos.Any(x => x.Nome.Trim().ToUpper() == request.Nome.Trim().ToUpper() && !x.Excluido);

            if (grupoExiste)
                Mensageria.RetornarMensagemFalhaValidacao("Grupo informado já existe.");
        }

        private async Task CadastrarGrupo(CadastrarGrupoRequest request)
        {
            var grupoNovo = TinyMapper.Map<Grupo>(request);

            await _context.Grupos.AddAsync(grupoNovo);
            var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas <= 0)
                Mensageria.RetornarMensagemErro("Falha ao cadastrar o grupo indicado. Tente novamente mais tarde.");
            Mensageria.AdicionarMensagemInformativa("Grupo cadastrado com sucesso.");
        }
    }
}
