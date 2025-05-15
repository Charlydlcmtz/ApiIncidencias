using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtProvider _jwtProvider;

        public UsuarioController(IUsuarioService usuarioService, IJwtProvider jwtProvider)
        {
            _usuarioService = usuarioService;
            _jwtProvider = jwtProvider;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet(Name = "Listar Usuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int usuarioId)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(usuarioId);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioRegistroDto>> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            if (usuarioRegistroDto == null) return BadRequest("El objeto no puede ser nulo");

            var resultado = await _usuarioService.AgregarAsync(usuarioRegistroDto);

            if (resultado.Usuario == null)
            {
                return BadRequest(new { mensaje = "Error al registrar al usuario" });
            }
            return Ok(resultado);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioLoginRespuestaDto>> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            if (usuarioLoginDto == null) return BadRequest("El objeto no puede ser nulo");
            var resultado = await _usuarioService.LoginAsync(usuarioLoginDto);
            if (resultado == null)
            {
                return BadRequest(new { mensaje = "Usuario o contraseña incorrectos" });
            }
            return Ok(resultado);
        }

        [AllowAnonymous]
        [HttpPost("check-token")]
        public IActionResult CheckRenewToken([FromHeader(Name = "Authorization")] string authHeader)
        {
            try
            {
                var token = _jwtProvider.ExtractTokenFromHeader(authHeader);
                var newToken = _jwtProvider.RenewToken(token);

                return Ok(new { token = newToken });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al renovar el token: {ex.Message}" });
            }
        }
    }
}
