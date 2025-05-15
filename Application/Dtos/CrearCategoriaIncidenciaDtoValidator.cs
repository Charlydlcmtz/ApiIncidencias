using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CrearCategoriaIncidenciaDtoValidator : AbstractValidator<CrearCategoriaIncidenciaDto>
    {
        public CrearCategoriaIncidenciaDtoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título de la categoría es obligatorio.")
                .MinimumLength(5).WithMessage("El título de la categoría debe tener al menos 5 caracteres.");
            RuleFor(x => x.Descripcion)
                .MaximumLength(250).WithMessage("La descripción de la categoría no puede exceder los 250 caracteres.");
        }
    }
}
