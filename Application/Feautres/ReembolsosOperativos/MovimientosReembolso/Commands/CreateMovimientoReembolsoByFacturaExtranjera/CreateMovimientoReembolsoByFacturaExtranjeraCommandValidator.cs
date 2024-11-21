using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaExtrangera
{
    public class CreateMovimientoReembolsoByFacturaExtranjeraCommandValidator : AbstractValidator<CreateMovimientoReembolsoByFacturaExtranjeraCommand>
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;
        private readonly IRepositoryAsync<TipoImpuesto> _repositoryAsyncTipoImpuesto;
        public CreateMovimientoReembolsoByFacturaExtranjeraCommandValidator(
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

            RuleFor(x => x.PDFMovReembolso)
                .NotNull()
                .WithMessage("El archivo PDF de reembolso es requerido.");

            RuleFor(r => r.EmisorRFC)
                .NotEmpty()
                .WithMessage("El campo EmisorRFC es obligatorio.")
                .MaximumLength(100)
                .WithMessage("Se superaron los 100 caracteres permitidos.");

            RuleFor(r => r.LugarExpedicion)
                .NotEmpty()
                .WithMessage("El campo LugarExpedicion es obligatorio.")
                .Length(5).WithMessage("El campo LugarExpedicion debe tener 5 caracteres.");

            RuleFor(r => r.ReceptorRFC)
                .NotEmpty()
                .WithMessage("El campo ReceptorRFC es obligatorio.")
                .MaximumLength(100)
                .WithMessage("Se superaron los 100 caracteres permitidos.");

            RuleFor(r => r.Subtotal)
                .NotEmpty()
                .WithMessage("El campo Subtotal es obligatorio.");

            RuleFor(r => r.Total)
                .NotEmpty()
                .WithMessage("El campo Total es obligatorio.");

            RuleFor(r => r.ReceptorNombre)
                .NotEmpty()
                .WithMessage("El campo ReceptorNombre es obligatorio.");

            RuleFor(r => r.Uuid)
                .NotEmpty()
                .WithMessage("El campo Uuid es obligatorio.")
                .MaximumLength(100)
                .WithMessage("Se superaron los 100 caracteres permitidos.")
                .Matches(@"^[0-9a-fA-F]{8}-(?:[0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}$")
                .WithMessage("El campo Uuid debe tener el formato válido.");

            RuleFor(r => r.EmisorNombre)
                .NotEmpty()
                .WithMessage("El campo EmisorNombre es obligatorio.")
                .MaximumLength(200)
                .WithMessage("Se superaron los 200 caracteres permitidos.");

            RuleFor(r => r.TipoComprobanteId)
                .NotEmpty()
                .WithMessage("El campo TipoComprobanteId es obligatorio.");

            RuleFor(r => r.FechaTimbrado)
                .NotEmpty()
                .WithMessage("El campo FechaTimbrado es obligatorio.");
        }
    }
}
