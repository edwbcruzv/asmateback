using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Administracion.Clientes.Commands.UpdateClienteCommand
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        public readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
        public UpdateClientCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany,
            IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;

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
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("RFC no cumple con el formato requerido")
                .MaximumLength(13).WithMessage("Rfc no puede ser de más de {MaxLength} caracteres");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} es obligatorio");

            RuleFor(c => c.RazonSocial)
                .NotEmpty().WithMessage("RazonSocial es obligatorio");
            //RuleFor(c => c.RegimenFiscalId)


            RuleFor(f => f.RegimenFiscalId)
                .NotEmpty().WithMessage("EmisorRegimenFiscalId es obligatorio")
                .MustAsync(async (EmisorRegimenFiscalId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncRegimenFiscal.GetByIdAsync(EmisorRegimenFiscalId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro EmisorRegimenFiscalId no encontrado en RegimenFiscales");

        }
    }
}
