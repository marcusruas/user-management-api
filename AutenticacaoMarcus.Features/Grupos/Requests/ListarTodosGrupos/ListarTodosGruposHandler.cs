﻿using AutenticacaoMarcus.Features.Grupos.Requests.CadastrarGrupo;
using AutenticacaoMarcus.Features.Grupos.ValueObjects;
using AutenticacaoMarcus.Infrastructure.Context;
using AutenticacaoMarcus.SharedKernel.Mensageria;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcus.Features.Grupos.Requests.ListarTodosGrupos
{
    public class ListarTodosGruposHandler : FeatureHandler<ListarTodosGruposRequest, IEnumerable<GrupoDto>>
    {
        public ListarTodosGruposHandler(AutenticacaoDbContext context, IMensageria mensageria, ILogger<FeatureHandler<ListarTodosGruposRequest, IEnumerable<GrupoDto>>> logger) : base(mensageria, logger)
        {
            _context = context;
        }

        private readonly AutenticacaoDbContext _context;

        public override async Task<IEnumerable<GrupoDto>> HandleRequest(ListarTodosGruposRequest request, CancellationToken cancellationToken)
        {
            var gruposCadastrados = await _context.Grupos.Where(x => !x.Excluido).ToListAsync();

            if (gruposCadastrados is null)
                return new List<GrupoDto>();

            return gruposCadastrados.Select(x => TinyMapper.Map<GrupoDto>(x));
        }
    }
}