using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CategoriaIncidenciaDto
    {
        public int Id { get; set; } // Id de la categoria de la incidencia
        public string Titulo { get; set; } // Titulo de la categoria de la incidencia
        public string Descripcion { get; set; } // Descripción de la incidencia
    }
}
