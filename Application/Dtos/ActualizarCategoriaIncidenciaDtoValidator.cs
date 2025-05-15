using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ActualizarCategoriaIncidenciaDtoValidator : AbstractValidator<ActualizarCategoriaIncidenciaDto>
    {
        public ActualizarCategoriaIncidenciaDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id de la categoria debe ser mayor que 0.")
                .WithMessage("El Id no puede estar vacío.");
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("El Título no puede estar vacío.")
                .MinimumLength(5).WithMessage("El Titulo debe tener al menos 5 caracteres.")
                .MaximumLength(100)
                .WithMessage("El Título no puede exceder los 100 caracteres.");
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La Descripción no puede estar vacía.")
                .MaximumLength(250)
                .WithMessage("La Descripción no puede exceder los 500 caracteres.");
        }
    }
}
