using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.SendComplementoPagoCommand
{
    public class SendComplementoPagoCommandValidator : AbstractValidator<SendComplementoPagoCommand>
    {

        public SendComplementoPagoCommandValidator()
        {

        }
    }
}
