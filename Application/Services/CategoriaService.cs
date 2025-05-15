using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaService(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public async Task ActualizarAsync(ActualizarCategoriaIncidenciaDto actualizarCategoriaDto)
        {
            var categoria = await _repo.GetCategoriaByIdAsync(actualizarCategoriaDto.Id);
            categoria.Titulo = actualizarCategoriaDto.Titulo;
            categoria.Descripcion = actualizarCategoriaDto.Descripcion;
            await _repo.ActualizarAsync(categoria);
        }

        public async Task<CategoriaIncidenciaDto> AgregarAsync(CrearCategoriaIncidenciaDto crearCategoriaDto)
        {
            var categoria_nueva = new CategoriaIncidencia
            {
                Titulo = crearCategoriaDto.Titulo,
                Descripcion = crearCategoriaDto.Descripcion
            };
            await _repo.AgregarAsync(categoria_nueva);

            return new CategoriaIncidenciaDto
            {
                Id = categoria_nueva.Id,
                Titulo = categoria_nueva.Titulo,
                Descripcion = categoria_nueva.Descripcion
            };
        }

        public async Task EliminarAsync(int categoriaId)
        {
            await _repo.EliminarAsync(categoriaId);
        }

        public async Task<CategoriaIncidenciaDto> GetCategoriaByIdAsync(int categoriaId)
        {
            var categoria = await _repo.GetCategoriaByIdAsync(categoriaId);
            return new CategoriaIncidenciaDto
            {
                Id = categoria.Id,
                Titulo = categoria.Titulo,
                Descripcion = categoria.Descripcion
            };
        }

        public async Task<CategoriaIncidenciaDto> GetCategoriaByNombreAsync(string nombreCategoria)
        {
            var categoria = await _repo.GetCategoriaByNombreAsync(nombreCategoria);
            return new CategoriaIncidenciaDto
            {
                Id = categoria.Id,
                Titulo = categoria.Titulo,
                Descripcion = categoria.Descripcion
            };
        }

        public async Task<IEnumerable<CategoriaIncidenciaDto>> GetCategoriasAsync()
        {
            var categorias = await _repo.GetCategoriasAsync();
            return categorias.Select(c => new CategoriaIncidenciaDto
            {
                Id = c.Id,
                Titulo = c.Titulo,
                Descripcion = c.Descripcion
            });
        }
    }
}
