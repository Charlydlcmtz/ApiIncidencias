using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ActualizarTipoIncidenciaDto
    {
        public int Id { get; set; } // ID del tipo de incidencia
        public string Titulo { get; set; } // Titulo del tipo de incidencia
        public string Descripcion { get; set; } // Descripción del tipo de incidencia
    }
}
