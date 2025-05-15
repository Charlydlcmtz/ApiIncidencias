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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(CategoriaIncidencia categoria)
        {
            _context.CategoriasIncidencias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarAsync(CategoriaIncidencia categoria)
        {
            await _context.CategoriasIncidencias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int categoriaId)
        {
            var categoria = await GetCategoriaByIdAsync(categoriaId);
             _context.CategoriasIncidencias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoriaIncidencia> GetCategoriaByIdAsync(int categoriaId)
        {
            return await _context.CategoriasIncidencias.FirstOrDefaultAsync(c => c.Id == categoriaId)
                ?? throw new Exception("Categoria no Encontrada");
        }

        public async Task<CategoriaIncidencia> GetCategoriaByNombreAsync(string nombreCategoria)
        {
            return await _context.CategoriasIncidencias.FirstOrDefaultAsync(c => c.Titulo == nombreCategoria)
                ?? throw new Exception("Categoria no Encontrada");
        }

        public async Task<IEnumerable<CategoriaIncidencia>> GetCategoriasAsync()
        {
            return await _context.CategoriasIncidencias.ToListAsync();
        }
    }
}
