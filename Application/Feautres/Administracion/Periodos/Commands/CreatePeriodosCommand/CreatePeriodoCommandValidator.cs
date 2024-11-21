using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Administracion.Periodos.Commands.CreatePeriodosCommand
{
    public class CreatePeriodoCommandValidator : AbstractValidator<CreatePeriodoCommand>
    {
        public readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        public readonly IRepositoryAsync<TipoPeriocidadPago> _repositoryAsyncTipoPeriocidadPago;

        public CreatePeriodoCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany, 
            IRepositoryAsync<TipoPeriocidadPago> repositoryAsyncTipoPeriocidadPago)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;
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

            RuleFor(f => f.TipoPeriocidadId)
                .NotEmpty().WithMessage("TipoPeriocidadId es obligatorio")
                .MustAsync(async (TipoPeriocidadId, cancellationToken) =>
                {
                    var company = await repositoryAsyncTipoPeriocidadPago.GetByIdAsync(TipoPeriocidadId);

                    if (company == null) return false;

                    return true;
                })
                .WithMessage($"Registro TipoPeriocidadId no encontrado en TipoPeriocidadPagos");

        }
    }
}
