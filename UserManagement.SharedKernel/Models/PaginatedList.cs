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
        public PaginatedList() { }

        public PaginatedList(IEnumerable<T> items, int currentPage, int requestedAmountOfRecords, int totalNumberOfRecords)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalNumberOfRecords = totalNumberOfRecords;
            TotalNumberOfPages = (int)Math.Ceiling(totalNumberOfRecords / (double)requestedAmountOfRecords);
        }

        public IEnumerable<T> Items { get; private set; }    
        public int CurrentPage { get; private set; }
        public int TotalNumberOfRecords { get; private set; }
        public int TotalNumberOfPages { get; private set; }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query, int pagina, int quantidadeRegistros)
        {
            var itens = await query.Skip((pagina - 1) * quantidadeRegistros).Take(quantidadeRegistros).ToListAsync();
            var quantidadeTotalRegistros = await query.CountAsync();

            return new PaginatedList<T>(itens, pagina, quantidadeRegistros, quantidadeTotalRegistros);
        }

        public static PaginatedList<T> Create(IEnumerable<T> itens, int paginaAtual, int quantidadeRegistrosSolicitada, int quantidadeTotalRegistros)
            => new(itens, paginaAtual, quantidadeRegistrosSolicitada, quantidadeTotalRegistros);

        public static PaginatedList<TDestination> CreateFromPaginatedList<TDestination, TSource>(IEnumerable<TDestination> items, PaginatedList<TSource> paginatedList)
        {
            var result = new PaginatedList<TDestination>();
            result.Items = items;
            result.CurrentPage = paginatedList.CurrentPage;
            result.TotalNumberOfRecords = paginatedList.TotalNumberOfRecords;
            result.TotalNumberOfPages = paginatedList.TotalNumberOfPages;

            return result;
        }
    }
}
