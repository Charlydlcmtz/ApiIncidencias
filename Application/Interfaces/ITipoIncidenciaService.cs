using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITipoIncidenciaService
    {
        Task<IEnumerable<TipoIncidenciaDto>> GetTiposIncidenciaAsync();
        Task<IEnumerable<TipoIncidenciaDto>> GetTiposIncidenciaPorCategoriaAsync(int categoriaId);
        Task<TipoIncidenciaDto> GetTipoIncidenciaByIdAsync(int tipoIncidenciaId);
        Task<TipoIncidenciaDto> GetTipoIncidenciaByNombreAsync(string nombreTipoIncidencia);
        Task<TipoIncidenciaDto> AgregarAsync(CrearTipoIncidenciaDto crearTipoIncidenciaDto);
        Task ActualizarAsync(ActualizarTipoIncidenciaDto actualizarTipoIncidenciaDto);
        Task EliminarAsync(int tipoIncidenciaId);
    }
}
