using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Catalogos.CveProductos.Commands.UpdateCveProductosCommand
{
    public class UpdateCveProductosCommandValidator : AbstractValidator<UpdateCveProductosCommand>
    {
        public UpdateCveProductosCommandValidator()
        {
            RuleFor(f => f.Estatus)
                .NotNull()
                .WithMessage("El estatus es obligatorio")
                .Must(e => e == true || e == false)
                .WithMessage("El estatus debe ser true o false");
        }


    }
}
