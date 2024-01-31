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
        public CadastrarGrupoHandler(IMensageria mensageria, ILogger<FeatureHandler<CadastrarGrupoRequest, bool>> logger) : base(mensageria, logger) { }

        public override Task<bool> HandleRequest(CadastrarGrupoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
