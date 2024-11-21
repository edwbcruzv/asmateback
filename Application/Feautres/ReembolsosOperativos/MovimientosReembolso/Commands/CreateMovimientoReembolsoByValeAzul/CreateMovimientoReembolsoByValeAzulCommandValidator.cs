using Application.Interfaces;
using Application.Specifications.ReembolsosOperativos.MovimientoReembolsos;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByValeAzul
{
    public class CreateMovimientoReembolsoByValeAzulCommandValidator : AbstractValidator<CreateMovimientoReembolsoByValeAzulCommand>
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;
        private readonly IRepositoryAsync<MovimientoReembolso> _repositoryAsyncMovimientoReembolso;

        public CreateMovimientoReembolsoByValeAzulCommandValidator(
            IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
            IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago,
            IRepositoryAsync<MovimientoReembolso> repositoryAsyncMovimientoReembolso)
        {
            _repositoryAsyncReembolso = repositoryAsyncReembolso;
            _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;
            _repositoryAsyncMovimientoReembolso = repositoryAsyncMovimientoReembolso;

            RuleFor(x => x.FechaMovimiento)// validacion
               .NotEmpty().WithMessage("El campo FechaMovimiento es obligatorio.");

            RuleFor(x => x.EmisorNombre)
              .NotEmpty().WithMessage("El campo EmisorNombre es obligatorio.");

            RuleFor(x => x.Total) // validacion
               .NotNull().WithMessage("El campo Monto es obligatorio.")
               .GreaterThanOrEqualTo(0).WithMessage("El campo Monto no puede ser negativo.");

            RuleFor(x => x)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var item = await _repositoryAsyncMovimientoReembolso.FirstOrDefaultAsync(new MovimientoReembolsoByFechaMovAndTotalSpecification(x.FechaMovimiento, x.Total));

                    if (item != null) return false;

                    return true;


                })
                .WithMessage("El Movimiento Reembolso ya esta registrado");


            RuleFor(x => x.Concepto)
               .NotEmpty()
               .WithMessage("El campo Concepto es obligatorio.")
               .MaximumLength(200)
               .WithMessage("El campo Concepto debe tener un máximo de 200 caracteres.");

            RuleFor(x => x.ReembolsoId)
               .MustAsync(async (ReembolsoId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncReembolso.GetByIdAsync(ReembolsoId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage("El campo ReembolsoId no es valido.")
               .NotEmpty();

            RuleFor(x => x.MetodoPagoId)
               .MustAsync(async (MetodoPagoId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncMetodoPago.GetByIdAsync(MetodoPagoId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage("El campo MetodoPagoId no es valido.");

        }
    }
}
