using AutenticacaoMarcus.Features.Grupos.Requests.AlterarGrupo;
using AutenticacaoMarcus.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcus.Features.Grupos.Requests.DeletarGrupo;
using AutenticacaoMarcus.Features.Grupos.Requests.ListarTodosGrupos;
using AutenticacaoMarcus.Features.Grupos.Requests.ObterGrupoPorId;
using AutenticacaoMarcus.Features.Grupos.ValueObjects;
using AutenticacaoMarcus.SharedKernel.Retornos;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoMarcus.Api.Controllers
{
    [Route("api/grupos")]
    public class GruposController : StandardController
    {
        public GruposController(IMediator mediador) : base(mediador) { }

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<GrupoDto>>> ObterGrupoPorId()
            => await ProcessarSolicitacao(new ListarTodosGruposRequest());

        [HttpGet("{id}")]
        public async Task<RetornoApi<GrupoDto>> ObterGrupoPorId(Guid id)
            => await ProcessarSolicitacao(new ObterGrupoPorIdRequest(id));

        [HttpPut("{id}")]
        public async Task<RetornoApi<bool>> AlterarGrupo(Guid id, [FromBody] AlteracaoGrupoBody request)
            => await ProcessarSolicitacao(new AlterarGrupoRequest(id, request));

        [HttpPost]
        public async Task<RetornoApi<bool>> CadastrarGrupo([FromBody] CadastrarGrupoRequest request)
            => await ProcessarSolicitacao(request);

        [HttpDelete("{id}")]
        public async Task<RetornoApi<bool>> ExcluirGrupo(Guid id)
            => await ProcessarSolicitacao(new DeletarGrupoRequest(id));
    }
}