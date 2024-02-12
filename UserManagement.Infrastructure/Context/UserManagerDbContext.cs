using UserManagement.SharedKernel.Retornos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users.Entities;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagerDbContext : DbContext
    {
        public UserManagerDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(80);
                builder.Property(x => x.Description).HasMaxLength(200);
            });
        }
    }
}
