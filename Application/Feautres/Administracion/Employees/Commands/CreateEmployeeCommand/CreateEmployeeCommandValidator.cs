using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Administracion.Employees.Commands.CreateEmployeeCommand
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        public readonly IRepositoryAsync<TipoContrato> _repositoryAsyncTipoContrato;
        public readonly IRepositoryAsync<TipoRegimen> _repositoryAsyncTipoRegimen;
        public readonly IRepositoryAsync<TipoRiesgoTrabajo> _repositoryAsyncTipoRiesgoTrabajo;
        public readonly IRepositoryAsync<TipoIncapacidad> _repositoryAsyncTipoIncapacidad;
        public readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
        public readonly IRepositoryAsync<User> _repositoryAsyncUser;
        public readonly IRepositoryAsync<Puesto> _repositoryAsyncPuesto;
        public readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;
        public readonly IRepositoryAsync<TipoJornada> _repositoryAsyncTipoJornada;
        public readonly IRepositoryAsync<FormaPago> _repositoryAsyncFormaPago;
        public readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsyncTipoPeriocidadPago;

        public CreateEmployeeCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany,
            IRepositoryAsync<User> repositoryAsyncUser,
            IRepositoryAsync<TipoContrato> repositoryAsyncTipoContrato,
            IRepositoryAsync<TipoRegimen> repositoryAsyncTipoRegimen,
            IRepositoryAsync<TipoRiesgoTrabajo> repositoryAsyncTipoRiesgoTrabajo,
            IRepositoryAsync<TipoIncapacidad> repositoryAsyncTipoIncapacidad,
            IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal,
            IRepositoryAsync<Puesto> repositoryAsyncPuesto,
            IRepositoryAsync<Banco> repositoryAsyncBanco,
            IRepositoryAsync<TipoJornada> repositoryAsyncTipoJornada,
            IRepositoryAsync<FormaPago> repositoryAsyncFormaPago,
            IRepositoryAsync<TipoPeriocidadPago> repositoryAsyncTipoPeriocidadPago)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncUser = repositoryAsyncUser;
            _repositoryAsyncTipoContrato = repositoryAsyncTipoContrato;
            _repositoryAsyncTipoRegimen = repositoryAsyncTipoRegimen;
            _repositoryAsyncTipoRiesgoTrabajo = repositoryAsyncTipoRiesgoTrabajo;
            _repositoryAsyncTipoIncapacidad = repositoryAsyncTipoIncapacidad;
            _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;
            _repositoryAsyncPuesto = repositoryAsyncPuesto;
            _repositoryAsyncBanco = repositoryAsyncBanco;
            _repositoryAsyncTipoJornada = repositoryAsyncTipoJornada;
            _repositoryAsyncFormaPago = repositoryAsyncFormaPago;
            _repositoryAsyncTipoPeriocidadPago = repositoryAsyncTipoPeriocidadPago;

            RuleFor(f => f.CompanyId)
                .NotEmpty().WithMessage("CompanyId es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var company = await _repositoryAsyncCompany.GetByIdAsync(CompanyId);

                    if (company == null) return false;

                    return true;
                })
                .WithMessage($"Registro CompanyId no encontrado en companies");

            RuleFor(c => c.Rfc)
                .Matches(@"^$|^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("Rfc no cumple con el formato requerido")
                .MaximumLength(13).WithMessage("Rfc no puede ser de más de {MaxLength} caracteres");

            RuleFor(c => c.Nss)
                .Matches(@"^$|^[0-9]{11}").WithMessage("Nss no cumple con el formato requerido")
                .MaximumLength(11).WithMessage("Nss no puede ser de más de {MaxLength} caracteres");

            RuleFor(c => c.Curp)
                .Matches(@"^$|^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$").WithMessage("Curp no cumple con el formato requerido")
                .MaximumLength(18).WithMessage("Curp no puede ser de más de {MaxLength} caracteres");

            RuleFor(f => f.UserId)
                .MustAsync(async (UserId, cancellationToken) =>
                {
                    if (UserId != null)
                    {

                        var item = await _repositoryAsyncUser.GetByIdAsync(UserId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro UserId no encontrado en usuarios");

            RuleFor(f => f.BancoId)
                .MustAsync(async (BancoId, cancellationToken) =>
                {
                    if (BancoId != null)
                    {

                        var item = await _repositoryAsyncBanco.GetByIdAsync(BancoId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro BancoId no encontrado en Bancos");

            RuleFor(f => f.TipoContratoId)
                .MustAsync(async (TipoContratoId, cancellationToken) =>
                {
                    if (TipoContratoId != null)
                    {

                        var item = await _repositoryAsyncTipoContrato.GetByIdAsync(TipoContratoId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro TipoContratoId no encontrado en TipoContratos");

            RuleFor(f => f.TipoJornadaId)
                .MustAsync(async (TipoJornadaId, cancellationToken) =>
                {
                    if (TipoJornadaId != null)
                    {

                        var item = await _repositoryAsyncTipoJornada.GetByIdAsync(TipoJornadaId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro TipoJornadaId no encontrado en TipoJornadas");

            RuleFor(f => f.TipoPeriocidadPagoId)
                .MustAsync(async (TipoPeriocidadPagoId, cancellationToken) =>
                {
                    if (TipoPeriocidadPagoId != null)
                    {

                        var item = await _repositoryAsyncTipoPeriocidadPago.GetByIdAsync(TipoPeriocidadPagoId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro TipoPeriocidadPagoId no encontrado en TipoPeriocidadPagos");

            RuleFor(f => f.FormaPagoId)
                .MustAsync(async (FormaPagoId, cancellationToken) =>
                {
                    if (FormaPagoId != null)
                    {

                        var item = await _repositoryAsyncFormaPago.GetByIdAsync(FormaPagoId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro FormaPagoId no encontrado en FormaPago");

            RuleFor(f => f.TipoPrevicionSocial)
                .MustAsync(async (TipoPrevicionSocial, cancellationToken) =>
                {
                    if (TipoPrevicionSocial != null)
                    {

                        switch (TipoPrevicionSocial)
                        {
                            case 1:
                                return true;
                            case 2:
                                return true;
                            default:
                                return false;

                        }

                    }
                    return true;
                })

                .WithMessage($"Registro TipoPrevicionSocial solo es 1: Por salarios mínimos o 2: Por porcentaje o null");

            RuleFor(f => f.TipoRegimenId)
                .MustAsync(async (TipoRegimenId, cancellationToken) =>
                {
                    if (TipoRegimenId != null)
                    {

                        var item = await _repositoryAsyncTipoRegimen.GetByIdAsync(TipoRegimenId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro TipoRegimenId no encontrado en TipoRegimens");

            RuleFor(f => f.TipoRiesgoTrabajoId)
                .MustAsync(async (TipoRiesgoTrabajoId, cancellationToken) =>
                {
                    if (TipoRiesgoTrabajoId != null)
                    {

                        var item = await _repositoryAsyncTipoRiesgoTrabajo.GetByIdAsync(TipoRiesgoTrabajoId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro TipoRiesgoTrabajoId no encontrado en TipoRiesgoTrabajos");

            RuleFor(f => f.TipoIncapacidadId)
                .MustAsync(async (TipoIncapacidadId, cancellationToken) =>
                {
                    if (TipoIncapacidadId != null)
                    {

                        var item = await _repositoryAsyncTipoIncapacidad.GetByIdAsync(TipoIncapacidadId);
                        if (item == null) return false;

                    }
                    return true;
                })
                .WithMessage($"Registro TipoIncapacidadId no encontrado en TipoIncapacidads");

            RuleFor(f => f.PuestoId)
                .MustAsync(async (PuestoId, cancellationToken) =>
                {
                    if (PuestoId != null)
                    {

                        var item = await _repositoryAsyncPuesto.GetByIdAsync(PuestoId);
                        if (item == null) return false;

                    }
                    return true;
                })
                .WithMessage($"Registro PuestoId no encontrado en Puestos");


            RuleFor(f => f.RegimenFiscalId)
                .MustAsync(async (RegimenFiscalId, cancellationToken) =>
                {
                    if (RegimenFiscalId != null)
                    {

                        var item = await _repositoryAsyncRegimenFiscal.GetByIdAsync(RegimenFiscalId);
                        if (item == null) return false;

                    }
                    return true;
                })

                .WithMessage($"Registro RegimenFiscalId no encontrado en RegimenFiscals");

        }



    }
}
