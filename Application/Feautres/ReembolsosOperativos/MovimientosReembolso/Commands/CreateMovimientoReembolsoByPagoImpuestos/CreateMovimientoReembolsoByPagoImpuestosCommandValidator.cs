using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByPagoImpuestos
{
    public class CreateMovimientoReembolsoByPagoImpuestosCommandValidator : AbstractValidator<CreateMovimientoReembolsoByPagoImpuestosCommand>
    {

        private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;
        private readonly IRepositoryAsync<TipoImpuesto> _repositoryAsyncTipoImpuesto;

        public CreateMovimientoReembolsoByPagoImpuestosCommandValidator(
            IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
            IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago,
            IRepositoryAsync<TipoImpuesto> repositoryAsyncTipoImpuesto
            )
        {
            _repositoryAsyncReembolso = repositoryAsyncReembolso;
            _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;
            _repositoryAsyncTipoImpuesto = repositoryAsyncTipoImpuesto;


            RuleFor(x => x.EmisorNombre)
               .NotEmpty().WithMessage("El campo EmisorNombre es obligatorio.");

            RuleFor(x => x.Total)
               .NotNull().WithMessage("El campo Monto es obligatorio.")
               .GreaterThanOrEqualTo(0).WithMessage("El campo Monto no puede ser negativo.");

            RuleFor(x => x.FechaMovimiento)
                .NotEmpty().WithMessage("El campo FechaMovimiento es obligatorio.");

            RuleFor(x => x.AnoyMes)
                .NotEmpty().WithMessage("El campo AnoyMes es obligatorio.")
                .Must(anoyMes => Regex.IsMatch(anoyMes.ToString(), @"^\d{4}\d{2}$"))
                .WithMessage("El campo AnoMes debe tener 4 dígitos de año seguidos de 2 dígitos de mes.");

            RuleFor(x => x.Concepto)
               .NotEmpty()
               .WithMessage("El campo Concepto es obligatorio.")
               .MaximumLength(200)
               .WithMessage("El campo Concepto debe tener un máximo de 200 caracteres.");


            RuleFor(x => x.ReembolsoId)
                .NotNull().WithMessage("El campo ReembolsoId es obligatorio.")
                .MustAsync(async (ReembolsoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncReembolso.GetByIdAsync(ReembolsoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo ReembolsoId no es valido.");

            RuleFor(x => x.MetodoPagoId)
                .NotNull().WithMessage("El campo MetodoPagoId es obligatorio.")
               .MustAsync(async (MetodoPagoId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncMetodoPago.GetByIdAsync(MetodoPagoId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage("El campo MetodoPagoId no es valido.");

            RuleFor(x => x.LineaCaptura)
                .NotEmpty()
                .WithMessage("El campo LineaCaptura no debe estar vacío.");

            RuleFor(x => x.PDFMovReembolso)
                .NotNull()
                .WithMessage("El archivo PDF de reembolso es requerido.");

        }
    }
}
