using AutenticacaoMarcusApi.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcusApi.SharedKernel.Retornos;
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
    }
}