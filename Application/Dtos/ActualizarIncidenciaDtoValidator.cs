using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ActualizarIncidenciaDtoValidator : AbstractValidator<ActualizarIncidenciaDto>
    {
        public ActualizarIncidenciaDtoValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El ID de la incidencia debe ser mayor a 0.");
            RuleFor(x => x.CategoriaIncidenciaId)
                .GreaterThan(0)
                .WithMessage("El ID de la categoría debe ser mayor a 0.");
            RuleFor(x => x.TipoIncidenciaId)
                .GreaterThan(0)
                .WithMessage("El ID del tipo de incidencia debe ser mayor a 0.");
            RuleFor(x => x.Descripcion)
                .MaximumLength(250)
                .WithMessage("La descripción no puede exceder los 250 caracteres.");
            RuleFor(x => x.Estado)
                .IsInEnum()
                .WithMessage("El estatus de la incidencia debe ser 'Abierta', 'EnProceso' o 'Cerrada'.");
            RuleFor(x => x.UsuarioId)
                .NotEmpty()
                .WithMessage("El ID del usuario que reportó la incidencia es obligatorio.");
        }
    }
}
