using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/incidencias")]
    [ApiController]
    public class IncidenciaController : ControllerBase
    {
        private readonly IIncidenciaService _incidenciaService;
        public IncidenciaController(IIncidenciaService incidenciaService)
        {
            _incidenciaService = incidenciaService;
        }

        [Authorize]
        [HttpGet(Name = "GetIncidencias")]
        public async Task<ActionResult<IEnumerable<IncidenciaDto>>> GetIncidencias()
        {
            var incidencias = await _incidenciaService.GetIncidenciasAsync();
            return Ok(new
            {
                success = true,
                data = incidencias
            });
        }

        [Authorize]
        [HttpGet("{incidenciaId}", Name = "GetIncidencia")]
        public async Task<ActionResult<IncidenciaDto>> GetIncidenciaById(int incidenciaId)
        {
            var incidencia = await _incidenciaService.GetIncidenciaByIdAsync(incidenciaId);
            if (incidencia == null) return NotFound();
            return Ok(new
            {
                success = true,
                data = incidencia
            });
        }

        [Authorize]
        [HttpPost(Name = "AddIncidencia")]
        public async Task<ActionResult<IncidenciaDto>> Agregar([FromBody] CrearIncidenciaDto crearIncidenciaDto)
        {
            if (crearIncidenciaDto == null) return BadRequest("El objeto no puede ser nulo");
            var nuevaIncidencia =  await _incidenciaService.AgregarAsync(crearIncidenciaDto);
            return Ok(new
            {
                success = true,
                data = nuevaIncidencia
            });

        }

        [Authorize]
        [HttpPut("{incidenciaId:int}", Name = "UpdateIncidencia")]
        public async Task<ActionResult<IncidenciaDto>> Actualizar(int incidenciaId, [FromBody] ActualizarIncidenciaDto actualizarIncidenciaDto)
        {
            if (actualizarIncidenciaDto == null) return BadRequest("La incidencia no se pudo actualizar");
            if (incidenciaId != actualizarIncidenciaDto.Id) return BadRequest("El ID de la incidencia no coincide");
            var existe = await _incidenciaService.GetIncidenciaByIdAsync(incidenciaId);
            if (existe == null) return NotFound();
            return Ok(new
            {
                success = true,
                mensaje = "Incidencia actualizada correctamente."
            });

        }

        [Authorize]
        [HttpDelete("{incidenciaId:int}", Name = "DeleteIncidencia")]
        public async Task<ActionResult<IncidenciaDto>> Eliminar(int incidenciaId)
        {
            var incidencia = await _incidenciaService.GetIncidenciaByIdAsync(incidenciaId);
            if (incidencia == null) return NotFound();
            await _incidenciaService.EliminarAsync(incidenciaId);
            return Ok(new
            {
                success = true,
                mensaje = "Incidencia eliminada correctamente."
            });
        }

        [Authorize]
        [HttpGet("por-categoria/{categoriaId:int}", Name = "GetIncidenciasPorCategoria")]
        public async Task<ActionResult<IEnumerable<IncidenciaDto>>> GetIncidenciasByCategoriaId(int categoriaId)
        {
            var incidencias = await _incidenciaService.GetIncidenciasByCategoriaIdAsync(categoriaId);
            return Ok(new
            {
                success = true,
                data = incidencias
            });
        }
    }
}
