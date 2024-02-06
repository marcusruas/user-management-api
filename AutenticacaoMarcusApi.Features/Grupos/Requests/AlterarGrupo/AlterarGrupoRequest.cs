using AutenticacaoMarcusApi.Features.Grupos.Requests.CadastrarGrupo;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.Features.Grupos.Requests.AlterarGrupo
{
    public class AlterarGrupoRequest : IRequest<bool>
    {
        public AlterarGrupoRequest(Guid id, AlteracaoGrupoBody request)
        {
            Id = id;
            Nome = request.Nome;
            Descricao = request.Descricao;
        }

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome do grupo é obrigatório.")]
        [MaxLength(80, ErrorMessage = "Nome do grupo deve conter no máximo 80 caractéres")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Descrição do grupo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Descrição do grupo deve conter no máximo 200 caractéres")]
        public string? Descricao { get; set; }
    }
}
