using AutenticacaoMarcus.Domain.Grupos.Entities;
using AutenticacaoMarcus.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcus.Features.Grupos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features
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
