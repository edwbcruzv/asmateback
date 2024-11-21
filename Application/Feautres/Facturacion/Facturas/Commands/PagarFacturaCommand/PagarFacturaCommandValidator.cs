using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Facturacion.Facturas.Commands.PagarFacturaCommand
{
    public class PagarFacturaCommandValidator : AbstractValidator<PagarFacturaCommand>
    {

        public PagarFacturaCommandValidator()
        {

            RuleFor(c => c.ComprobantePago)
                .NotEmpty().WithMessage("ComprobantePago es obligatorio");

            RuleFor(c => c.FechaPago)
                .NotEmpty().WithMessage("FechaPago es obligatorio");

        }
    }
}
