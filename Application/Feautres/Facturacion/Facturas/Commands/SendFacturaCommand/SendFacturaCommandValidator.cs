using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Facturacion.Facturas.Commands.SendFacturaCommand
{
    public class SendFacturaCommandValidator : AbstractValidator<SendFacturaCommand>
    {

        public SendFacturaCommandValidator()
        {

        }
    }
}
