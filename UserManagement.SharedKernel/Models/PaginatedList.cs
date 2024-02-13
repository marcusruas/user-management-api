using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.SharedKernel
{
    public class PaginatedList<T>
    {
        public PaginatedList(IEnumerable<T> items, int currentPage, int requestedAmountOfRecords, int totalNumberOfRecords)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalNumberOfRecords = totalNumberOfRecords;
            TotalNumberOfPages = (int)Math.Ceiling(totalNumberOfRecords / (double)requestedAmountOfRecords);
        }

        public IEnumerable<T> Items { get; }
        public int CurrentPage { get; }
        public int TotalNumberOfRecords { get; }
        public int TotalNumberOfPages { get; }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query, int pagina, int quantidadeRegistros)
        {
            var itens = await query.Skip((pagina - 1) * quantidadeRegistros).Take(quantidadeRegistros).ToListAsync();
            var quantidadeTotalRegistros = await query.CountAsync();

            return new PaginatedList<T>(itens, pagina, quantidadeRegistros, quantidadeTotalRegistros);
        }

        public static PaginatedList<T> Create(IEnumerable<T> itens, int paginaAtual, int quantidadeRegistrosSolicitada, int quantidadeTotalRegistros)
            => new(itens, paginaAtual, quantidadeRegistrosSolicitada, quantidadeTotalRegistros);
    }
}
