using AutenticacaoMarcus.Domain.Grupos.Entities;
using MediatR;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features.Grupos.Requests.CadastrarGrupo
{
    public class CadastrarGrupoRequest : IRequest<bool>
    {
        [Required(ErrorMessage = "Nome do grupo é obrigatório.")]
        [MaxLength(80, ErrorMessage = "Nome do grupo deve conter no máximo 80 caractéres")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Descrição do grupo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Descrição do grupo deve conter no máximo 200 caractéres")]
        public string? Descricao { get; set; }
    }
}
