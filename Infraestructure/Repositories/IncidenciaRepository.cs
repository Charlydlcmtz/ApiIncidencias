using Domain.Entities;
using Domain.Repositories;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IncidenciaRepository : IIncidenciaRepository
    {
        private readonly ApplicationDbContext _context;

        public IncidenciaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Incidencia incidencia)
        {
            _context.Incidencias.Update(incidencia);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(Incidencia incidencia)
        {
             await _context.Incidencias.AddAsync(incidencia);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int incidenciaId)
        {
            var incidencia = await GetIncidenciaByIdAsync(incidenciaId);
            _context.Incidencias.Remove(incidencia);
            await _context.SaveChangesAsync();
        }

        public async Task<Incidencia> GetIncidenciaByIdAsync(int incidenciaId)
        {
            return await _context.Incidencias.FirstOrDefaultAsync(i => i.Id == incidenciaId)
                ?? throw new Exception("Incidencia no Encontrada");
        }

        public async Task<Incidencia> GetIncidenciasByCategoriaIdAsync(int categoriaId)
        {
            return await _context.Incidencias.Include(i => i.CategoriaIncidencia).FirstOrDefaultAsync(i => i.CategoriaIncidencia.Id == categoriaId)
                ?? throw new Exception("Incidencia no Encontrada");
        }

        public async Task<IEnumerable<Incidencia>> GetIncidenciasAsync()
        {
            return await _context.Incidencias
                .Include(i => i.CategoriaIncidencia)
                .Include(i => i.TipoIncidencia)
                .ToListAsync();
        }
    }
}
