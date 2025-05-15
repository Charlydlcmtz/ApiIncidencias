using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CrearTipoIncidenciaDtoValidator : AbstractValidator<CrearTipoIncidenciaDto>
    {
        public CrearTipoIncidenciaDtoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título de la incidencia es obligatorio.")
                .MaximumLength(100).WithMessage("El título de la incidencia no puede exceder los 100 caracteres.");
            RuleFor(x => x.Descripcion)
                .MaximumLength(250).WithMessage("La descripción de la categoría no puede exceder los 250 caracteres.");
            RuleFor(x => x.CategoriaIncidenciaId)
                .NotEmpty().WithMessage("La categoría de la incidencia es obligatoria.");
        }
    }
}
