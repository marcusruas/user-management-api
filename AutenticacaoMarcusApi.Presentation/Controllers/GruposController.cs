using AutenticacaoMarcusApi.SharedKernel.Retornos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoMarcusApi.Presentation.Controllers
{
    [Route("api/grupos")]
    public class GruposController : StandardController
    {
        public GruposController(IMediator mediador) : base(mediador) { }

        [HttpGet]
        public string Index()
        {
            return "teste";
        }
    }
}