using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIncidenciaService
    {
        Task<IEnumerable<IncidenciaDto>> GetIncidenciasAsync();
        Task<IncidenciaDto> GetIncidenciaByIdAsync(int incidenciaId);
        Task<IncidenciaDto> GetIncidenciasByCategoriaIdAsync(int categoriaId);
        Task<IncidenciaDto> AgregarAsync(CrearIncidenciaDto crearIncidenciaDto);
        Task ActualizarAsync(ActualizarIncidenciaDto ActualizarIncidenciaDto);
        Task EliminarAsync(int incidenciaId);
    }
}
