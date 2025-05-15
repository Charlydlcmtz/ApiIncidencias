using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoriaIncidencia
    {
        public int Id { get; set; }
        public string Titulo { get; set; } // Titulo de la categoria de la incidencia

        public string? Descripcion { get; set; } // Descripción de la incidencia

        public ICollection<TipoIncidencia>? Tipos { get; set; } // Relación con los tipos de incidencias
    }
}
