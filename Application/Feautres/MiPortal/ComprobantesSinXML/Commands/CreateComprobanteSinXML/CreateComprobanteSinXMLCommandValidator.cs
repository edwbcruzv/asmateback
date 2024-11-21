using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.ComprobantesSinXML.Commands.CreateComprobanteSinXML
{
    public class CreateComprobanteSinXMLCommandValidator : AbstractValidator<CreateComprobanteSinXMLCommand>
    {
        private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
        private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncTipoMoneda;
        private readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;

        public CreateComprobanteSinXMLCommandValidator(
                IRepositoryAsync<Viatico> repositoryAsyncViatico, 
                IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda,
                IRepositoryAsync<FormaPago> repositoryAsyncFormaPago,
                IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago)
        {
            _repositoryAsyncViatico = repositoryAsyncViatico;
            _repositoryAsyncTipoMoneda = repositoryAsyncTipoMoneda;
            _repositoryAsyncFormaPago = repositoryAsyncFormaPago;
            _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;


            RuleFor(x => x.ViaticoId)
                .NotEmpty().WithMessage("El viatico es oblicatorio es obligatorio")
                .MustAsync(async (ViaticoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncViatico.GetByIdAsync(ViaticoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El viatico no existe no existe");

            RuleFor(x => x.Total)
                .NotEmpty().WithMessage("La fecha es obligatorio");

            RuleFor(x => x.FechaMovimiento)
                .NotEmpty().WithMessage("El estatus es obligatorio");

            RuleFor(x => x.Concepto)
                .NotEmpty().WithMessage("El monto es obligatorio");

            RuleFor(x => x.PDF)
                .NotNull()
                .WithMessage("El archivo PDF no puede ser nulo.");


            ///////////////////////////////////////////////////////////////////////////

            RuleFor(x => x.Uuid)
               .NotEmpty().WithMessage("El campo Uuid es obligatorio.")
               .Matches(@"^[0-9a-fA-F]{8}-(?:[0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}$")
               .WithMessage("El campo Uuid debe tener el formato válido.");

            RuleFor(x => x.Concepto)
                .NotEmpty()
                .WithMessage("El campo Concepto es obligatorio.")
                .MaximumLength(200)
                .WithMessage("El campo Concepto debe tener un máximo de 200 caracteres.");


            RuleFor(x => x.EmisorRFC)
                .NotEmpty().WithMessage("El campo EmisorRFC es obligatorio.")
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("El campo EmisorRFC debe tener un formato válido.");


            RuleFor(x => x.EmisorNombre)
                .NotEmpty().WithMessage("El campo EmisorNombre es obligatorio.");

            RuleFor(x => x.ReceptorRFC)
                .NotEmpty().WithMessage("El campo ReceptorRFC es obligatorio.")
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("El campo ReceptorRFC debe tener un formato válido.");

            RuleFor(x => x.ReceptorNombre)
                .NotEmpty().WithMessage("El campo ReceptorNombre es obligatorio.");

            RuleFor(x => x.TipoMonedaId)
               .NotNull().WithMessage("El campo TipoMonedaId es obligatorio.")
               .MustAsync(async (TipoMonedaId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncTipoMoneda.GetByIdAsync(TipoMonedaId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage("El campo TipoMonedaId no es valido.");

            RuleFor(x => x.FormaPagoId)
                .NotNull().WithMessage("El campo FormaPagoId es obligatorio.")
                .MustAsync(async (FormaPagoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncFormaPago.GetByIdAsync(FormaPagoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo FormaPagoId no es valido.");

            RuleFor(x => x.SubTotal)
                .NotNull().WithMessage("El campo Subtotal es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo Subtotal no puede ser negativo.");

            RuleFor(x => x.Total)
                .NotNull().WithMessage("El campo Total es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo Total no puede ser negativo.");
        }
    }
}
