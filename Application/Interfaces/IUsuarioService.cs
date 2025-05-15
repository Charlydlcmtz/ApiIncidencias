using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetUsuariosAsync();
        Task<UsuarioDto> GetUsuarioByIdAsync(int usuarioId);
        Task<UsuarioDto> GetUsuarioByUsernameAsync(string username);
        Task<UsuarioLoginRespuestaDto> LoginAsync(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioRegistroRespuestaDto> AgregarAsync(UsuarioRegistroDto usuarioRegistroDto);
        Task ActualizarAsync(UsuarioDto usuarioDto);
        Task EliminarAsync(int usuarioId);
    }
}
