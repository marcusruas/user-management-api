using AutenticacaoMarcusApi.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcusApi.Features.Grupos.Requests.DeletarGrupo;
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

        [HttpPost]
        public async Task<RetornoApi<bool>> CadastrarGrupo([FromBody] CadastrarGrupoRequest request)
            => await ProcessarSolicitacao(request);

        [HttpDelete("id")]
        public async Task<RetornoApi<bool>> ExcluirGrupo(Guid id)
            => await ProcessarSolicitacao(new DeletarGrupoRequest(id));
    }
}