using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class IncidenciaDto
    {
        public int Id { get; set; } // ID de la incidencia
        public int CategoriaIncidenciaId { get; set; } // ID de la categoría de la incidencia
        public string CategoriaIncidenciaNombre { get; set; } // Nombre de la categoría de la incidencia
        public int TipoIncidenciaId { get; set; } // ID del tipo de incidencia
        public string TipoIncidenciaNombre { get; set; } // Nombre del tipo de incidencia
        public string Descripcion { get; set; } // Descripción de la incidencia
        //public int EstadoId { get; set; } // ID del estado de la incidencia
        public Estado Estado { get; set; } // 👈 Aquí ya usas el enum directamente
        public string EstadoNombre => Estado.ToString(); // Nombre del estado de la incidencia
        public DateTime FechaCreacion { get; set; } // Fecha de creación de la incidencia
        public int UsuarioId { get; set; } // ID del usuario que reportó la incidencia
    }
}
