using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; } // Nombre de usuario
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Correo { get; set; }
        public string ContrasenaHash { get; set; } // Almacenado como hash
        public string Rol { get; set; } // Ejemplo: Admin, Operador, Soporte
        public bool Activo { get; set; } = true;
    }
}
