using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.ComprobantesSinXML.Commands.UpdateComprobanteSinXML
{
    public class UpdateComprobanteSinXMLCommandValidator :AbstractValidator<UpdateComprobanteSinXMLCommand>
    {
        private readonly IRepositoryAsync<Viatico> _repositoryAsyncViatico;
        private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncTipoMoneda;
        private readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;

        public UpdateComprobanteSinXMLCommandValidator(IRepositoryAsync<Viatico> repositoryAsyncViatico, IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda, IRepositoryAsync<FormaPago> repositoryAsyncFormaPago, IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago)
        {
            _repositoryAsyncViatico = repositoryAsyncViatico;
            _repositoryAsyncTipoMoneda = repositoryAsyncTipoMoneda;
            _repositoryAsyncFormaPago = repositoryAsyncFormaPago;
            _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;

            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("El Id es obligatorio");

            RuleFor(x => x.ViaticoId)
                .NotEmpty().WithMessage("El viatico es oblicatorio es obligatorio")
                .When(x => x.ViaticoId.HasValue)
                .MustAsync(async (ViaticoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncViatico.GetByIdAsync(ViaticoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El viatico no existe no existe");

            RuleFor(x => x.Uuid)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Matches(@"^[0-9a-fA-F]{8}-(?:[0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}$")
                .When(x => x.Uuid != null)
                .WithMessage("El campo Uuid debe tener el formato válido.")
                .When(x => x.Uuid == null)
                .WithMessage("El campo Uuid es opcional.");

            RuleFor(x => x.Concepto)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(200)
                .WithMessage("El campo Concepto debe tener un máximo de 200 caracteres.");


            RuleFor(x => x.EmisorRFC)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("El campo EmisorRFC debe tener un formato válido.");


            RuleFor(x => x.ReceptorRFC)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("El campo ReceptorRFC debe tener un formato válido.");

            RuleFor(x => x.TipoMonedaId)
               .NotEmpty().WithMessage("El tipo de moneda es oblicatorio es obligatorio")
               .When(x => x.TipoMonedaId.HasValue)
               .MustAsync(async (TipoMonedaId, cancellationToken) =>
               {
                   var item = await _repositoryAsyncTipoMoneda.GetByIdAsync(TipoMonedaId);

                   if (item == null) return false;

                   return true;
               })
               .WithMessage("El campo TipoMonedaId no es valido.");

            RuleFor(x => x.FormaPagoId)
                .NotEmpty().WithMessage("El forma de pago es oblicatorio es obligatorio")
                .When(x => x.FormaPagoId.HasValue)
                .MustAsync(async (FormaPagoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncFormaPago.GetByIdAsync(FormaPagoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo FormaPagoId no es valido.");

            RuleFor(x => x.SubTotal)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(0).WithMessage("El campo Subtotal no puede ser negativo.");

            RuleFor(x => x.Total)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(0).WithMessage("El campo Total no puede ser negativo.");


        }
    }
}
