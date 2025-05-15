using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();

        Task<Usuario> GetUsuarioByIdAsync(int usuarioId);

        Task<Usuario> GetUsuarioByUsernameAsync(string username);

        Task AgregarAsync(Usuario usuario);

        Task ActualizarAsync(Usuario usuario);

        Task EliminarAsync(int usuarioId);
    }
}
