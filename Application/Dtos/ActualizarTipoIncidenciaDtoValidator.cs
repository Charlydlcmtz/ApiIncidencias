using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ActualizarTipoIncidenciaDtoValidator : AbstractValidator<ActualizarTipoIncidenciaDto>
    {
        public ActualizarTipoIncidenciaDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id del tipo de incidencia debe ser mayor que 0.");
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("El Título no puede estar vacío.")
                .MinimumLength(5).WithMessage("El Título debe tener al menos 5 caracteres.")
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
