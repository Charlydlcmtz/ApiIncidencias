using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoIncidencia
    {
        public int Id { get; set; }
        public string Titulo { get; set; } // Titulo de la incidencia

        public string? Descripcion { get; set; } // Descripción de la incidencia

        public int CategoriaIncidenciaId { get; set; } // ID de la categoría de la incidencia
        public CategoriaIncidencia CategoriaIncidencia { get; set; } // Relación con la categoría de la incidencia

        public ICollection<Incidencia>? Incidencias { get; set; } // Relación con las incidencias
    }
}
