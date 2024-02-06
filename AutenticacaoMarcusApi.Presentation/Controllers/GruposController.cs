using AutenticacaoMarcusApi.Features.Grupos.Requests.AlterarGrupo;
using AutenticacaoMarcusApi.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcusApi.Features.Grupos.Requests.DeletarGrupo;
using AutenticacaoMarcusApi.Features.Grupos.Requests.ListarTodosGrupos;
using AutenticacaoMarcusApi.Features.Grupos.Requests.ObterGrupoPorId;
using AutenticacaoMarcusApi.Features.Grupos.ValueObjects;
using AutenticacaoMarcusApi.SharedKernel.Retornos;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoMarcusApi.Presentation.Controllers
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