using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Facturacion.ComplementoPagos.Commands.UpdateComplementoPagoCommand
{
    public class UpdateComplementoPagoCommandValidator : AbstractValidator<UpdateComplementoPagoCommand>
    {
        public readonly IRepositoryAsync<Company> _repositoryAsync;
        public readonly IRepositoryAsync<UsoCfdi> _repositoryAsyncUsoCfdi;
        public readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
        public readonly IRepositoryAsync<TipoMoneda> _repositoryAsyncUsoTipoMoneda;
        public readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
        public readonly IRepositoryAsync<MetodoPago> _repositoryAsyncUsoMetodoPago;
        public readonly IRepositoryAsync<TipoComprobante> _repositoryAsyncTipoComprobante;
        public readonly IRepositoryAsync<Client> _repositoryAsyncClient;
        public UpdateComplementoPagoCommandValidator(IRepositoryAsync<Company> repositoryAsync, IRepositoryAsync<UsoCfdi> repositoryAsyncUsoCfdi,
            IRepositoryAsync<FormaPago> repositoryAsyncUsoFormaPago, IRepositoryAsync<TipoMoneda> repositoryAsyncUsoTipoMoneda,
            IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal, IRepositoryAsync<MetodoPago> repositoryAsyncUsoMetodoPago,
            IRepositoryAsync<TipoComprobante> repositoryAsyncTipoComprobante, IRepositoryAsync<Client> repositoryAsyncClient)
        {

            _repositoryAsync = repositoryAsync;
            _repositoryAsyncUsoCfdi = repositoryAsyncUsoCfdi;
            _repositoryAsyncFormaPago = repositoryAsyncUsoFormaPago;
            _repositoryAsyncUsoTipoMoneda = repositoryAsyncUsoTipoMoneda;
            _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;
            _repositoryAsyncUsoMetodoPago = repositoryAsyncUsoMetodoPago;
            _repositoryAsyncTipoComprobante = repositoryAsyncTipoComprobante;
            _repositoryAsyncClient = repositoryAsyncClient;

            RuleFor(f => f.CompanyId)
                .NotEmpty().WithMessage("CompanyId es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var company = await _repositoryAsync.GetByIdAsync(CompanyId);

                    if (company == null) return false;

                    return true;
                })
                .WithMessage($"Registro CompanyId no encontrado en companies");

            RuleFor(f => f.ClientId)
                .NotEmpty().WithMessage("ClientId es obligatorio")
                .MustAsync(async (ClientId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncClient.GetByIdAsync(ClientId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro ClientId no encontrado en Clients");

            RuleFor(c => c.ReceptorRfc)
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("EmisorRfc no cumple con el formato requerido")
                .MaximumLength(13).WithMessage("ReceptorRfc no puede ser de más de {MaxLength} caracteres")
                .NotEmpty().WithMessage("ReceptorRfc es requerido");

            RuleFor(c => c.EmisorRfc)
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("EmisorRfc no cumple con el formato requerido")
                .MaximumLength(13).WithMessage("EmisorRfc no puede ser de más de {MaxLength} caracteres")
                .NotEmpty().WithMessage("ReceptorRfc es requerido");

            RuleFor(c => c.ReceptorRazonSocial)
                .NotEmpty().WithMessage("ReceptorRazonSocial es obligatorio");

            RuleFor(c => c.EmisorRazonSocial)
                .NotEmpty().WithMessage("EmisorRazonSocial es obligatorio");

            RuleFor(c => c.LugarExpedicion)
                .NotEmpty().WithMessage("LugarExpedicion es obligatorio");

            RuleFor(c => c.ReceptorDomicilioFiscal)
                .NotEmpty().WithMessage("ReceptorDomicilioFiscal es obligatorio");

            RuleFor(f => f.FormaPagoId)
                .NotEmpty().WithMessage("FormaPagoId es obligatorio")
                .MustAsync(async (FormaPagoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncFormaPago.GetByIdAsync(FormaPagoId);

                    if (item == null) return false;

                    return true;
                }).WithMessage($"Registro FormaPagoId no encontrado en FormaPagos");

            RuleFor(f => f.TipoMonedaId)
                .NotEmpty().WithMessage("TipoMonedaId es obligatorio")
                .MustAsync(async (TipoMonedaId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncUsoTipoMoneda.GetByIdAsync(TipoMonedaId);

                    if (item == null) return false;

                    return true;
                }).WithMessage($"Registro TipoMonedaId no encontrado en TipoMonedas");

            RuleFor(f => f.EmisorRegimenFiscalId)
                .NotEmpty().WithMessage("EmisorRegimenFiscalId es obligatorio")
                .MustAsync(async (EmisorRegimenFiscalId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncRegimenFiscal.GetByIdAsync(EmisorRegimenFiscalId);

                    if (item == null) return false;

                    return true;
                }).WithMessage($"Registro EmisorRegimenFiscalId no encontrado en RegimenFiscales");

            RuleFor(f => f.ReceptorRegimenFiscalId)
                .NotEmpty().WithMessage("ReceptorRegimenFiscalId es obligatorio")
                .MustAsync(async (ReceptorRegimenFiscalId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncRegimenFiscal.GetByIdAsync(ReceptorRegimenFiscalId);

                    if (item == null) return false;

                    return true;
                }).WithMessage($"Registro ReceptorDomicilioFiscal no encontrado en TipoComprobantes");

            RuleFor(c => c.FechaPago)
                .NotEmpty().WithMessage("FechaPago es obligatorio");

        }
    }
}
