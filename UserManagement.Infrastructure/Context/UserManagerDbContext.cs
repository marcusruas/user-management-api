using UserManagement.SharedKernel.Retornos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users.Entities;
using UserManagement.SharedKernel.Returns.Handlers;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagerDbContext : StandardContext<UserManagerDbContext>
    {
        public UserManagerDbContext(DbContextOptions<UserManagerDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(80).IsRequired();
                builder.Property(x => x.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(120).IsRequired();
                builder.Property(x => x.UserName).HasMaxLength(20).IsRequired();
                builder.Property(x => x.CPF).HasMaxLength(11).IsRequired();
                builder.Property(x => x.Password).HasMaxLength(60).IsRequired();
            });
        }
    }
}
