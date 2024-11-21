using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Facturacion.ComplementoPagoFacturas.Commands.UpdateComplementoPagoFacturaCommand
{
    public class UpdateComplementoPagoCommandValidator : AbstractValidator<UpdateComplementoPagoFacturaCommand>
    {

        public readonly IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago;
        public readonly IRepositoryAsync<Factura> _repositoryAsyncFactura;

        public UpdateComplementoPagoCommandValidator(
            IRepositoryAsync<ComplementoPago> _repositoryAsyncComplementoPago,
            IRepositoryAsync<Factura> _repositoryAsyncFactura
        )
        {

            RuleFor(f => f.ComplementoPagoId)
                .NotEmpty().WithMessage("ComplementoPagoId es obligatorio")
                .MustAsync(async (ComplementoPagoId, cancellationToken) =>
                {
                    var ComplementoPago = await _repositoryAsyncComplementoPago.GetByIdAsync(ComplementoPagoId);

                    if (ComplementoPago == null) return false;

                    return true;
                })
                .WithMessage($"Registro ComplementoPagoId no encontrado en companies");



            RuleFor(f => f.FacturaId)
                .NotEmpty().WithMessage("FacturaId es obligatorio")
                .MustAsync(async (FacturaId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncFactura.GetByIdAsync(FacturaId);

                    if (item == null) return false;

                    return true;
                }).WithMessage($"Registro FacturaId no encontrado en FormaPagos");

        }
    }
}
