using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Facturacion.Nominas.Commands.CalcularAguinaldoByEmployeeCommand
{
    public class CalcularAguinaldoByEmployeeCommandValidator : AbstractValidator <CalcularAguinaldoByEmployeeCommand>
    {
        public CalcularAguinaldoByEmployeeCommandValidator()
        {
            RuleFor(x => x.PeriodoID).NotEmpty().WithMessage("PeriodoID es obligatorio");
            RuleFor(x => x.EmployeeID).NotEmpty().WithMessage("EmployeeID es obligatorio");
        }
    }
}
