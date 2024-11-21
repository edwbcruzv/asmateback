using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Nominas.Commands.CalcularAguinaldoCommand
{
    public class CalcularAguinaldoCommandValidator : AbstractValidator<CalcularAguinaldoCommand>
    {
        public CalcularAguinaldoCommandValidator()
        {
            RuleFor(x => x.PeriodoId).NotEmpty().WithMessage("PeriodoID es obligatorio");
        }
    }
}
