using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Tarea> Tareas {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Tarea>()
                .Property(t => t.Titulo).HasMaxLength(120).IsRequired();
            modelBuilder.Entity<Tarea>()
                .Property(t => t.Descripcion).HasMaxLength(250).IsRequired();
        }
    }
}
