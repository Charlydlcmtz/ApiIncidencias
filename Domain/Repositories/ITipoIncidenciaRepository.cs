using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITipoIncidenciaRepository
    {
        Task<IEnumerable<TipoIncidencia>> GetTiposIncidenciaAsync();
        Task<IEnumerable<TipoIncidencia>> GetTiposPorCategoriaAsync(int categoriaIncidenciaId);
        Task<TipoIncidencia> GetTipoIncidenciaByIdAsync(int tipoIncidenciaId);
        Task<TipoIncidencia> GetTipoIncidenciaByNombreAsync(string nombreTipoIncidencia);
        Task AgregarAsync(TipoIncidencia tipoIncidencia);
        Task ActualizarAsync(TipoIncidencia tipoIncidencia);
        Task EliminarAsync(int tipoIncidenciaId);
    }
}
