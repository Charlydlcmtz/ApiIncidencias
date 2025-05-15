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
    public class IncidenciaService : IIncidenciaService
    {
        private readonly IIncidenciaRepository _repo;
        public IncidenciaService(IIncidenciaRepository repo)
        {
            _repo = repo;
        }
        public async Task ActualizarAsync(ActualizarIncidenciaDto actualizarIncidenciaDto)
        {
            var incidencia = await _repo.GetIncidenciaByIdAsync(actualizarIncidenciaDto.Id);

            incidencia.CategoriaIncidenciaId = actualizarIncidenciaDto.CategoriaIncidenciaId;
            incidencia.TipoIncidenciaId = actualizarIncidenciaDto.TipoIncidenciaId;
            incidencia.Descripcion = actualizarIncidenciaDto.Descripcion;
            incidencia.Estado = actualizarIncidenciaDto.Estado;
            incidencia.UsuarioId = actualizarIncidenciaDto.UsuarioId;

            await _repo.ActualizarAsync(incidencia);
        }

        public async Task<IncidenciaDto> AgregarAsync(CrearIncidenciaDto crearIncidenciaDto)
        {
            var incidencia_nueva = new Incidencia
            {
                CategoriaIncidenciaId = crearIncidenciaDto.CategoriaIncidenciaId,
                TipoIncidenciaId = crearIncidenciaDto.TipoIncidenciaId,
                Descripcion = crearIncidenciaDto.Descripcion,
                FechaCreacion = DateTime.Now,
                Estado = crearIncidenciaDto.Estado,
                UsuarioId = crearIncidenciaDto.UsuarioId
            };

            await _repo.AgregarAsync(incidencia_nueva);

            return new IncidenciaDto
            {
                Id = incidencia_nueva.Id,
                CategoriaIncidenciaId = incidencia_nueva.CategoriaIncidenciaId,
                TipoIncidenciaId = incidencia_nueva.TipoIncidenciaId,
                Descripcion = incidencia_nueva.Descripcion,
                FechaCreacion = incidencia_nueva.FechaCreacion,
                Estado = incidencia_nueva.Estado,
                UsuarioId = incidencia_nueva.UsuarioId
            };
        }

        public async Task EliminarAsync(int incidenciaId)
        {
            await _repo.EliminarAsync(incidenciaId);
        }

        public async Task<IncidenciaDto> GetIncidenciaByIdAsync(int incidenciaId)
        {
            var incidencia = await _repo.GetIncidenciaByIdAsync(incidenciaId);

            return new IncidenciaDto
            {
                Id = incidencia.Id,
                CategoriaIncidenciaId = incidencia.CategoriaIncidenciaId,
                TipoIncidenciaId = incidencia.TipoIncidenciaId,
                Descripcion = incidencia.Descripcion,
                FechaCreacion = incidencia.FechaCreacion,
                Estado = incidencia.Estado,
                UsuarioId = incidencia.UsuarioId
            };
        }

        public async Task<IncidenciaDto> GetIncidenciasByCategoriaIdAsync(int categoriaId)
        {
            var incidencia = await _repo.GetIncidenciasByCategoriaIdAsync(categoriaId);

            return new IncidenciaDto
            {
                Id = incidencia.Id,
                CategoriaIncidenciaId = incidencia.CategoriaIncidenciaId,
                TipoIncidenciaId = incidencia.TipoIncidenciaId,
                Descripcion = incidencia.Descripcion,
                FechaCreacion = incidencia.FechaCreacion,
                Estado = incidencia.Estado,
                UsuarioId = incidencia.UsuarioId
            };
        }

        public async Task<IEnumerable<IncidenciaDto>> GetIncidenciasAsync()
        {
            var incidencias = await _repo.GetIncidenciasAsync();

            return incidencias.Select(i => new IncidenciaDto
            {
                Id = i.Id,
                CategoriaIncidenciaId = i.CategoriaIncidenciaId,
                CategoriaIncidenciaNombre = i.CategoriaIncidencia.Titulo,

                TipoIncidenciaId = i.TipoIncidenciaId,
                TipoIncidenciaNombre = i.TipoIncidencia.Titulo,

                Descripcion = i.Descripcion,
                FechaCreacion = i.FechaCreacion,
                Estado = i.Estado,
                UsuarioId = i.UsuarioId
            });
        }
    }
}
