using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IIncidenciaRepository
    {
        Task<IEnumerable<Incidencia>> GetIncidenciasAsync();
        Task<Incidencia> GetIncidenciaByIdAsync(int incidenciaId);
        Task<Incidencia> GetIncidenciasByCategoriaIdAsync(int categoriaId);
        Task AgregarAsync(Incidencia incidencia);
        Task ActualizarAsync(Incidencia incidencia);
        Task EliminarAsync(int incidenciaId);
    }
}
