using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UsuarioRegistroDtoValidator : AbstractValidator<UsuarioRegistroDto>
    {
        public UsuarioRegistroDtoValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El username de usuario es obligatorio.")
                .MinimumLength(5).WithMessage("El nombre de usuario debe tener al menos 5 caracteres.");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(5).WithMessage("El nombre debe tener al menos 5 caracteres.");
            RuleFor(x => x.ApellidoP)
                .NotEmpty().WithMessage("El apellido paterno es obligatorio.")
                .MinimumLength(5).WithMessage("El apellido paterno debe tener al menos 5 caracteres.");
            RuleFor(x => x.ApellidoM)
                .NotEmpty().WithMessage("El apellido materno es obligatorio.")
                .MinimumLength(5).WithMessage("El apellido materno debe tener al menos 5 caracteres.");
            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no es válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos 1 letra mayúscula.")
                .Matches(@"[0-9]").WithMessage("La contraseña debe contener al menos 1 número.")
                .Matches(@"[!@#\$%\^&\*\.\-_]").WithMessage("La contraseña debe contener al menos un carácter especial.");
        }
    }
}
