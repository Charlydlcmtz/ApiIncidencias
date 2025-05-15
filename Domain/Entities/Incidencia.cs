using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Incidencia
    {
        public int Id { get; set; }

        public int CategoriaIncidenciaId { get; set; } // ID de la categoría de la incidencia
        public CategoriaIncidencia CategoriaIncidencia { get; set; } // Relación con la categoría de la incidencia
        public int TipoIncidenciaId { get; set; } // ID del tipo de incidencia
        public TipoIncidencia TipoIncidencia { get; set; } // Relación con la categoría de la incidencia

        public string? Descripcion { get; set; } // Descripción de la incidencia

        //Relación con los estados
        //public int EstadoId { get; set; }
        //public Estado Estado { get; set; } // Relación con el estado de la incidencia
        public Estado Estado { get; set; } = Estado.Abierta;
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // Fecha de creación de la incidencia

        public int UsuarioId { get; set; } // ID del usuario que reportó la incidencia
        public Usuario Usuario { get; set; } // Relación con el usuario que reportó la incidencia
    }
}
