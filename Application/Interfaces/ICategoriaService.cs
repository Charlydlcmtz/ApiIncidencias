using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaIncidenciaDto>> GetCategoriasAsync();
        Task<CategoriaIncidenciaDto> GetCategoriaByIdAsync(int categoriaId);
        Task<CategoriaIncidenciaDto> GetCategoriaByNombreAsync(string nombreCategoria);
        Task<CategoriaIncidenciaDto> AgregarAsync(CrearCategoriaIncidenciaDto crearCategoriaDto);
        Task ActualizarAsync(ActualizarCategoriaIncidenciaDto actualizarCategoriaDto);
        Task EliminarAsync(int categoriaId);
    }
}
