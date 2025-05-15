using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<CategoriaIncidencia>> GetCategoriasAsync();
        Task<CategoriaIncidencia> GetCategoriaByIdAsync(int categoriaId);
        Task<CategoriaIncidencia> GetCategoriaByNombreAsync(string nombreCategoria);
        Task AgregarAsync(CategoriaIncidencia categoria);
        Task ActualizarAsync(CategoriaIncidencia categoria);
        Task EliminarAsync(int categoriaId);
    }
}
