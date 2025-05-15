using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CrearTipoIncidenciaDto
    {
        public string Titulo { get; set; } // Titulo de la incidencia
        public string Descripcion { get; set; } // Descripción de la incidencia
        public int CategoriaIncidenciaId { get; set; } // ID de la categoría de la incidencia
    }
}
