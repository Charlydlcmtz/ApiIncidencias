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
    public class TipoIncidenciaRepository : ITipoIncidenciaRepository
    {

        private readonly ApplicationDbContext _context;

        public TipoIncidenciaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(TipoIncidencia tipoIncidencia)
        {
            _context.TiposIncidencias.Update(tipoIncidencia);
            await _context.SaveChangesAsync();
        }

        public Task AgregarAsync(TipoIncidencia tipoIncidencia)
        {
            _context.TiposIncidencias.AddAsync(tipoIncidencia);
            return _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int tipoIncidenciaId)
        {
            var tipoIncidencia = await GetTipoIncidenciaByIdAsync(tipoIncidenciaId);
            _context.TiposIncidencias.Remove(tipoIncidencia);
            await _context.SaveChangesAsync();
        }

        public async Task<TipoIncidencia> GetTipoIncidenciaByIdAsync(int tipoIncidenciaId)
        {
            return await _context.TiposIncidencias.FirstOrDefaultAsync(t => t.Id == tipoIncidenciaId)
                ?? throw new Exception("Tipo de Incidencia no Encontrada");
        }

        public  async Task<TipoIncidencia> GetTipoIncidenciaByNombreAsync(string nombreTipoIncidencia)
        {
            var tipoIncidencia = await _context.TiposIncidencias.FirstOrDefaultAsync(t => t.Titulo == nombreTipoIncidencia);
            return tipoIncidencia ?? throw new Exception("Tipo de Incidencia no Encontrada");
        }

        public async Task<IEnumerable<TipoIncidencia>> GetTiposIncidenciaAsync()
        {
            return await _context.TiposIncidencias.ToListAsync();
        }

        public async Task<IEnumerable<TipoIncidencia>> GetTiposPorCategoriaAsync(int categoriaIncidenciaId)
        {
            return await _context.TiposIncidencias.Where(t => t.CategoriaIncidenciaId == categoriaIncidenciaId)
                .ToListAsync();
        }
    }
}
