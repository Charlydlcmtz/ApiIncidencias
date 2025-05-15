using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Relación: TipoIncidencia pertenece a una CategoriaIncidencia
            builder.Entity<TipoIncidencia>()
                .HasOne(t => t.CategoriaIncidencia)
                .WithMany(c => c.Tipos)
                .HasForeignKey(t => t.CategoriaIncidenciaId);

            // Datos semilla para categorías
            builder.Entity<CategoriaIncidencia>().HasData(
                new CategoriaIncidencia { Id = 1, Titulo = "Camiones" },
                new CategoriaIncidencia { Id = 2, Titulo = "Operadores" },
                new CategoriaIncidencia { Id = 3, Titulo = "Mantenimiento" },
                new CategoriaIncidencia { Id = 4, Titulo = "Diesel" },
                new CategoriaIncidencia { Id = 5, Titulo = "Carga/descarga" },
                new CategoriaIncidencia { Id = 6, Titulo = "Tiempos de entrega" },
                new CategoriaIncidencia { Id = 7, Titulo = "Ruta / Accidente" },
                new CategoriaIncidencia { Id = 8, Titulo = "Sistemas" }
            );

            // Datos semilla para tipos
            builder.Entity<TipoIncidencia>().HasData(
                new TipoIncidencia { Id = 1, Titulo = "Fallo mecánico", CategoriaIncidenciaId = 1 },
                new TipoIncidencia { Id = 2, Titulo = "Retraso en carga", CategoriaIncidenciaId = 5 },
                new TipoIncidencia { Id = 3, Titulo = "Retraso en descarga", CategoriaIncidenciaId = 5 },
                new TipoIncidencia { Id = 4, Titulo = "Problema con GPS/Tablet", CategoriaIncidenciaId = 8 },
                new TipoIncidencia { Id = 5, Titulo = "Incidente en ruta (choque, etc.)", CategoriaIncidenciaId = 7 },
                new TipoIncidencia { Id = 6, Titulo = "Falta de diesel", CategoriaIncidenciaId = 4 },
                new TipoIncidencia { Id = 7, Titulo = "Daño en unidad", CategoriaIncidenciaId = 1 },
                new TipoIncidencia { Id = 8, Titulo = "Error en documentos", CategoriaIncidenciaId = 3 },
                new TipoIncidencia { Id = 9, Titulo = "Operador no disponible", CategoriaIncidenciaId = 2 },
                new TipoIncidencia { Id = 10, Titulo = "Otro", CategoriaIncidenciaId = 3 }
            );

            // Relación: Incidencia → Usuario
            builder.Entity<Incidencia>()
                .HasOne(i => i.Usuario)
                .WithMany()
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación: Incidencia → TipoIncidencia
            builder.Entity<Incidencia>()
                .HasOne(i => i.TipoIncidencia)
                .WithMany()
                .HasForeignKey(i => i.TipoIncidenciaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación: Incidencia → CategoriaIncidencia
            builder.Entity<Incidencia>()
                .HasOne(i => i.CategoriaIncidencia)
                .WithMany()
                .HasForeignKey(i => i.CategoriaIncidenciaId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<CategoriaIncidencia> CategoriasIncidencias { get; set; }
        public DbSet<TipoIncidencia> TiposIncidencias { get; set; }  
    }
}
