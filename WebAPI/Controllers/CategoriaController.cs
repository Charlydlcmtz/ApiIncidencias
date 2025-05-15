using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [Authorize]
        [HttpGet(Name = "GetCategorias")]
        public async Task<ActionResult<IEnumerable<CategoriaIncidenciaDto>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetCategoriasAsync();
            return Ok(new
            {
                success = true,
                data = categorias
            });
        }

        [Authorize]
        [HttpGet("{categoriaId}", Name = "GetCategoria")]
        public async Task<ActionResult<CategoriaIncidenciaDto>> GetCategoriaById(int categoriaId)
        {
            var categoria = await _categoriaService.GetCategoriaByIdAsync(categoriaId);
            if (categoria == null) return NotFound();
            return Ok(new
            {
                success = true,
                data = categoria
            });
        }

        [Authorize]
        [HttpPost(Name = "AddCategoria")]
        public async Task<ActionResult<CategoriaIncidenciaDto>> Agregar([FromBody] CrearCategoriaIncidenciaDto crearCategoriaDto)
        {
            if (crearCategoriaDto == null) return BadRequest("El objeto no puede ser nulo");
            var nuevaCategoria = await _categoriaService.AgregarAsync(crearCategoriaDto);

            return Ok(new
            {
                success = true,
                data = nuevaCategoria
            });
        }

        [Authorize]
        [HttpPut("{categoriaId:int}", Name = "UpdateCategoria")]
        public async Task<ActionResult<CategoriaIncidenciaDto>> Actualizar(int categoriaId, [FromBody] ActualizarCategoriaIncidenciaDto actualizarCategoriaDto)
        {
            if (actualizarCategoriaDto == null) return BadRequest("La categoria no se pudo actualizar");
            if (categoriaId != actualizarCategoriaDto.Id) return BadRequest("El ID de la categoria no coincide");
            var existe = await _categoriaService.GetCategoriaByIdAsync(categoriaId);
            if (existe == null) return NotFound();
            await _categoriaService.ActualizarAsync(actualizarCategoriaDto);
            return Ok(new
            {
                success = true,
                mensaje = "Categoria actualizada correctamente."
            });
        }

        [Authorize]
        [HttpDelete("{categoriaId:int}", Name = "DeleteCategoria")]
        public async Task<ActionResult<CategoriaIncidenciaDto>> Eliminar(int categoriaId)
        {
            var existe = await _categoriaService.GetCategoriaByIdAsync(categoriaId);
            if (existe == null) return NotFound();
            await _categoriaService.EliminarAsync(categoriaId);
            return Ok(new
            {
                success = true,
                mensaje = "Categoria eliminada correctamente."
            });
        }

        [Authorize]
        [HttpGet("nombre/{nombreCategoria}", Name = "SearchCategoria")]
        public async Task<ActionResult<CategoriaIncidenciaDto>> GetCategoriaByNombre(string nombreCategoria)
        {
            var categoria = await _categoriaService.GetCategoriaByNombreAsync(nombreCategoria);
            if (categoria == null) return NotFound();
            return Ok(new
            {
                success = true,
                data = categoria
            });
        }
    }
}
