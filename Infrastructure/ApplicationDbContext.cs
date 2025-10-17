using Domain.Entities.Eventos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<CategoriaEntrada> CategoriasEntrada { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.CategoriasEntrada)
                .WithOne(c => c.Evento)
                .HasForeignKey(c => c.EventoId);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Activo)
                .HasDefaultValue(true);
        }
    }
}
