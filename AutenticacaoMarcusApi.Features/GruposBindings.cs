using AutenticacaoMarcusApi.Domain.Grupos.Entities;
using AutenticacaoMarcusApi.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcusApi.Features.Grupos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.Features
{
    public static class Bindings
    {
        public static void CriarBindingsParaFeatures()
        {
            TinyMapper.Bind<CadastrarGrupoRequest, Grupo>();
            TinyMapper.Bind<Grupo, GrupoDto>();
        }
    }
}
