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
    public class TipoIncidenciaService : ITipoIncidenciaService
    {

        private readonly ITipoIncidenciaRepository _repo;

        public TipoIncidenciaService(ITipoIncidenciaRepository repo)
        {
            _repo = repo;
        }

        public async Task ActualizarAsync(ActualizarTipoIncidenciaDto actualizarTipoIncidenciaDto)
        {
            var tipoIncidencia = await _repo.GetTipoIncidenciaByIdAsync(actualizarTipoIncidenciaDto.Id);
            tipoIncidencia.Titulo = actualizarTipoIncidenciaDto.Titulo;
            tipoIncidencia.Descripcion = actualizarTipoIncidenciaDto.Descripcion;
            await _repo.ActualizarAsync(tipoIncidencia);
        }

        public async Task<TipoIncidenciaDto> AgregarAsync(CrearTipoIncidenciaDto crearTipoIncidenciaDto)
        {
            // Validaciones de entrada
            if (string.IsNullOrWhiteSpace(crearTipoIncidenciaDto.Titulo))
                throw new Exception("El título es obligatorio.");

            if (crearTipoIncidenciaDto.CategoriaIncidenciaId <= 0)
                throw new Exception("Debe seleccionar una categoría válida.");

            var tipoIncidencia_nueva = new TipoIncidencia
            {
                Titulo = crearTipoIncidenciaDto.Titulo,
                Descripcion = crearTipoIncidenciaDto.Descripcion,
                CategoriaIncidenciaId = crearTipoIncidenciaDto.CategoriaIncidenciaId,
            };
            await _repo.AgregarAsync(tipoIncidencia_nueva);
            return new TipoIncidenciaDto
            {
                Id = tipoIncidencia_nueva.Id,
                Titulo = tipoIncidencia_nueva.Titulo,
                Descripcion = tipoIncidencia_nueva.Descripcion
            };
        }

        public async Task EliminarAsync(int tipoIncidenciaId)
        {
            await _repo.EliminarAsync(tipoIncidenciaId);
        }

        public async Task<TipoIncidenciaDto> GetTipoIncidenciaByIdAsync(int tipoIncidenciaId)
        {
            var tipoIncidencia = await _repo.GetTipoIncidenciaByIdAsync(tipoIncidenciaId);
            return new TipoIncidenciaDto
            {
                Id = tipoIncidencia.Id,
                Titulo = tipoIncidencia.Titulo,
                Descripcion = tipoIncidencia.Descripcion
            };
        }

        public async Task<TipoIncidenciaDto> GetTipoIncidenciaByNombreAsync(string nombreTipoIncidencia)
        {
            var tipoIncidencia = await _repo.GetTipoIncidenciaByNombreAsync(nombreTipoIncidencia);
            return new TipoIncidenciaDto
            {
                Id = tipoIncidencia.Id,
                Titulo = tipoIncidencia.Titulo,
                Descripcion = tipoIncidencia.Descripcion
            };
        }

        public async Task<IEnumerable<TipoIncidenciaDto>> GetTiposIncidenciaAsync()
        {
            var tiposIncidencias = await _repo.GetTiposIncidenciaAsync();
            return tiposIncidencias.Select(t => new TipoIncidenciaDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                CategoriaIncidenciaId = t.CategoriaIncidenciaId
            });
        }

        public async Task<IEnumerable<TipoIncidenciaDto>> GetTiposIncidenciaPorCategoriaAsync(int categoriaId)
        {
            var tiposIncidencias = await _repo.GetTiposPorCategoriaAsync(categoriaId);
            return tiposIncidencias.Select(t => new TipoIncidenciaDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion
            });
        }
    }
}
