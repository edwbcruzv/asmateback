using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Catalogos.UnidadMedidas.Commands.UpdateUnidadMedidasCommand
{
    public class UpdateUnidadMedidasCommandValidator : AbstractValidator<UpdateUnidadMedidasCommand>
    {
        public UpdateUnidadMedidasCommandValidator()
        {
            RuleFor(f => f.Estatus)
                .NotNull()
                .WithMessage("El estatus es obligatorio")
                .Must(e => e == true || e == false)
                .WithMessage("El estatus debe ser true o false");

        }

    }
}
