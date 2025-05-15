using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CrearUsuarioDtoValidator : AbstractValidator<UsuarioRegistroDto>
    {
        public CrearUsuarioDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MinimumLength(5).WithMessage("El nombre de usuario debe tener al menos 5 caracteres.");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(5).WithMessage("El nombre debe tener al menos 5 caracteres.");
            RuleFor(x => x.ApellidoP)
                .NotEmpty().WithMessage("El apellido paterno es obligatorio.");
            RuleFor(x => x.ApellidoM)
                .NotEmpty().WithMessage("El apellido materno es obligatorio.");
            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no es válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos un número.")
                .Matches(@"[!@#\$%\^&\*\.\-_]").WithMessage("La contraseña debe contener al menos un carácter especial (como . !@# - _ etc).");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("El rol es obligatorio.")
                .Must(role => role == "Admin" || role == "User").WithMessage("El rol debe ser 'Admin' o 'User'.");
        }
    }
}
