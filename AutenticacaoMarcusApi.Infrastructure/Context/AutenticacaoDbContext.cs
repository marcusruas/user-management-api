using AutenticacaoMarcusApi.Domain.Grupos.Entities;
using AutenticacaoMarcusApi.SharedKernel.Retornos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoMarcusApi.Infrastructure.Context
{
    public class AutenticacaoDbContext : DbContext
    {
        public AutenticacaoDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Grupo> Grupos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupo>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Nome).HasMaxLength(80);
                builder.Property(x => x.Descricao).HasMaxLength(200);
            });
        }
    }
}
