using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CrearIncidenciaDtoValidator : AbstractValidator<CrearIncidenciaDto>
    {
        public CrearIncidenciaDtoValidator()
        {
            RuleFor(x => x.CategoriaIncidenciaId)
                .NotEmpty().WithMessage("La categoria de la incidencia es obligatoria.");
            RuleFor(x => x.TipoIncidenciaId)
                .NotEmpty().WithMessage("El tipo de incidencia es obligatorio.");
            RuleFor(x => x.Descripcion)
                .MinimumLength(25).WithMessage("La descripción debe tener al menos 25 caracteres.")
                .MaximumLength(250).WithMessage("La descripción no puede exceder los 250 caracteres.");
            RuleFor(x => x.Estado)
                .IsInEnum()
                .WithMessage("El estatus de la incidencia debe ser válido (Abierta, EnProceso o Cerrada).");
            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("El Usuario de la incidencia es obligatorio.");
        }
    }
}
