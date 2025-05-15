using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } // Nombre de usuario
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Correo { get; set; }
        public string ContrasenaHash { get; set; }
        public string Rol { get; set; } = "Operador"; // Ejemplo: Admin, Operador, Soporte
        public bool Activo { get; set; } = true; // Indica si el usuario está activo o no
    }
}
