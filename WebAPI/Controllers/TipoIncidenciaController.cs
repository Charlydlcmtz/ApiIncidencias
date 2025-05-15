using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/tipo_incidencia")]
    [ApiController]
    public class TipoIncidenciaController : ControllerBase
    {
        private readonly ITipoIncidenciaService _tipoIncidenciaService;
        public TipoIncidenciaController(ITipoIncidenciaService tipoIncidenciaService)
        {
            _tipoIncidenciaService = tipoIncidenciaService;
        }

        [Authorize]
        [HttpGet(Name = "GetTipoIncidencias")]
        public async Task<ActionResult<IEnumerable<TipoIncidenciaDto>>> GetTipoIncidencias()
        {
            var tipoIncidencias = await _tipoIncidenciaService.GetTiposIncidenciaAsync();
            return Ok(new
            {
                success = true,
                data = tipoIncidencias
            });
        }

        [Authorize]
        [HttpGet("{tipoId}", Name = "GetTipoIncidencia")]
        public async Task<ActionResult<TipoIncidenciaDto>> GetTipoIncidenciaById(int tipoId)
        {
            var tipoIncidencia = await _tipoIncidenciaService.GetTipoIncidenciaByIdAsync(tipoId);
            if (tipoIncidencia == null) return NotFound();
            return Ok(new
            {
                success = true,
                data = tipoIncidencia
            });
        }

        [Authorize]
        [HttpPost(Name = "AddTipoIncidencia")]
        public async Task<ActionResult<TipoIncidenciaDto>> Agregar([FromBody] CrearTipoIncidenciaDto crearTipoDto)
        {
            if (crearTipoDto == null) return BadRequest("El objeto no puede ser nulo");
            var nuevoTipo = await _tipoIncidenciaService.AgregarAsync(crearTipoDto);
            return Ok(new
            {
                success = true,
                data = nuevoTipo
            });
        }

        [Authorize]
        [HttpPut("{tipoIncidenciaId:int}", Name = "UpdateTipoIncidencia")]
        public async Task<ActionResult<TipoIncidenciaDto>> Actualizar(int tipoIncidenciaId, [FromBody] ActualizarTipoIncidenciaDto actualizarTipoIncidenciaDto)
        {
            if (actualizarTipoIncidenciaDto == null) return BadRequest("El tipo de incidencia no se pudo actualizar");
            if (tipoIncidenciaId != actualizarTipoIncidenciaDto.Id) return BadRequest("El ID del tipo de incidencia no coincide");
            var existe = await _tipoIncidenciaService.GetTipoIncidenciaByIdAsync(tipoIncidenciaId);
            if (existe == null) return NotFound();
            await _tipoIncidenciaService.ActualizarAsync(actualizarTipoIncidenciaDto);
            return Ok(new
            {
                success = true,
                message = "El tipo de incidencia se actualizó correctamente"
            });
        }

        [Authorize]
        [HttpDelete("{tipoIncidenciaId:int}", Name = "DeleteTipoIncidencia")]
        public async Task<ActionResult<TipoIncidenciaDto>> Eliminar(int tipoIncidenciaId)
        {
            var existe = await _tipoIncidenciaService.GetTipoIncidenciaByIdAsync(tipoIncidenciaId);
            if (existe == null) return NotFound();
            await _tipoIncidenciaService.EliminarAsync(tipoIncidenciaId);
            return Ok(new
            {
                success = true,
                message = "El tipo de incidencia se eliminó correctamente"
            });
        }

        [Authorize]
        [HttpGet("nombre/{nombreTipoIncidencia}", Name = "SearchTipoIncidencia")]
        public async Task<ActionResult<TipoIncidenciaDto>> GetTipoIncidenciaByNombre(string nombreTipoIncidencia)
        {
            var tipoIncidencia = await _tipoIncidenciaService.GetTipoIncidenciaByNombreAsync(nombreTipoIncidencia);
            if (tipoIncidencia == null) return NotFound();
            return Ok(new
            {
                success = true,
                data = tipoIncidencia
            });
        }

        [Authorize]
        [HttpGet("categoria/{categoriaId:int}", Name = "GetTipoIncidenciaPorCategoria")]
        public async Task<ActionResult<IEnumerable<TipoIncidenciaDto>>> GetTipoIncidenciaPorCategoria(int categoriaId)
        {
            var tipoIncidencias = await _tipoIncidenciaService.GetTiposIncidenciaPorCategoriaAsync(categoriaId);
            if (tipoIncidencias == null) return NotFound();
            return Ok(new
            {
                success = true,
                data = tipoIncidencias
            });
        }

    }
}
