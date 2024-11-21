using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Feautres.ReembolsosOperativos.MovimientosReembolso.Commands.CreateMovimientoReembolsoByFacturaSinXML
{
    public class CreateMovimientoReembolsoByFacturaSinXMLCommandValidator : AbstractValidator<CreateMovimientoReembolsoByFacturaSinXMLCommand>
    {
        private readonly IRepositoryAsync<Reembolso> _repositoryAsyncReembolso;
        private readonly IRepositoryAsync<TipoReembolso> _repositoryAsyncTipoReembolso;
        private readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncTipoMoneda;
        private readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
        private readonly IRepositoryAsync<TipoComprobante> _repositoryAsyncTipoComprobante;
        private readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
        private readonly IRepositoryAsync<MetodoPago> _repositoryAsyncMetodoPago;


        public CreateMovimientoReembolsoByFacturaSinXMLCommandValidator(
                    IRepositoryAsync<Reembolso> repositoryAsyncReembolso,
                    IRepositoryAsync<TipoReembolso> repositoryAsyncTipoReembolso,
                    IRepositoryAsync<TipoMoneda> repositoryAsyncTipoMoneda,
                    IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal,
                    IRepositoryAsync<TipoComprobante> repositoryAsyncTipoComprobante,
                    IRepositoryAsync<FormaPago> repositoryAsyncFormaPago,
                    IRepositoryAsync<MetodoPago> repositoryAsyncMetodoPago
            )
        {
            _repositoryAsyncReembolso = repositoryAsyncReembolso;
            _repositoryAsyncTipoReembolso = repositoryAsyncTipoReembolso;
            _repositoryAsyncTipoMoneda = repositoryAsyncTipoMoneda;
            _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;
            _repositoryAsyncTipoComprobante = repositoryAsyncTipoComprobante;
            _repositoryAsyncFormaPago = repositoryAsyncFormaPago;
            _repositoryAsyncMetodoPago = repositoryAsyncMetodoPago;



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

            RuleFor(x => x.LugarExpedicion)
                .NotEmpty().WithMessage("El campo LugarExpedicion es obligatorio.")
                .Length(5).WithMessage("El campo LugarExpedicion debe tener 5 caracteres.");

            RuleFor(x => x.FechaTimbrado)
                .NotEmpty().WithMessage("El campo FechaTimbrado es obligatorio.");

            RuleFor(x => x.IVATrasladados)
                .NotNull().WithMessage("El campo IVATrasladados es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo IVATrasladados no puede ser negativo.");

            RuleFor(x => x.IVARetenidos)
                .NotNull().WithMessage("El campo IVARetenido es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo IVARetenido no puede ser negativo.");

            RuleFor(x => x.ISR)
                .NotNull().WithMessage("El campo ISR es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo ISR no puede ser negativo.");

            RuleFor(x => x.IEPS)
                .NotNull().WithMessage("El campo IEPS es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo IEPS no puede ser negativo.");

            RuleFor(x => x.ISH)
                .NotNull().WithMessage("El campo ISH es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo ISH no puede ser negativo.");

            RuleFor(x => x.TipoMonedaId)
                .NotNull().WithMessage("El campo TipoMonedaId es obligatorio.")
                .MustAsync(async (TipoMonedaId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncTipoMoneda.GetByIdAsync(TipoMonedaId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo TipoMonedaId no es valido.");

            RuleFor(x => x.RegimenFiscalId)
                .NotNull().WithMessage("El campo RegimenFiscalId es obligatorio.")
                .MustAsync(async (RegimenFiscalId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncRegimenFiscal.GetByIdAsync(RegimenFiscalId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo RegimenFiscalId no es valido.");

            RuleFor(x => x.ReembolsoId)
                .NotNull().WithMessage("El campo ReembolsoId es obligatorio.")
                .MustAsync(async (ReembolsoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncReembolso.GetByIdAsync(ReembolsoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo ReembolsoId no es valido.");

            RuleFor(x => x.TipoComprobanteId)
                .NotNull().WithMessage("El campo TipoComprobanteId es obligatorio.")
                .MustAsync(async (TipoComprobanteId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncTipoComprobante.GetByIdAsync(TipoComprobanteId);

                    if (item == null) return false;

                    

                    return true;
                })
                .WithMessage("El campo TipoComprobanteId no es valido.");

            RuleFor(x => x.FormaPagoId)
                .NotNull().WithMessage("El campo FormaPagoId es obligatorio.")
                .MustAsync(async (FormaPagoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncFormaPago.GetByIdAsync(FormaPagoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo FormaPagoId no es valido.");

            RuleFor(x => x.MetodoPagoId)
                .NotNull().WithMessage("El campo MetodoPagoId es obligatorio.")
                .MustAsync(async (MetodoPagoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncMetodoPago.GetByIdAsync(MetodoPagoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage("El campo MetodoPagoId no es valido.");

            RuleFor(x => x.Subtotal)
                .NotNull().WithMessage("El campo Subtotal es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo Subtotal no puede ser negativo.");

            RuleFor(x => x.Total)
                .NotNull().WithMessage("El campo Total es obligatorio.")
                .GreaterThanOrEqualTo(0).WithMessage("El campo Total no puede ser negativo.");

            RuleFor(x => x.PDFMovReembolso)
                .NotNull()
                .WithMessage("El archivo PDF de reembolso es requerido.");
            
        }

        
    }
}
