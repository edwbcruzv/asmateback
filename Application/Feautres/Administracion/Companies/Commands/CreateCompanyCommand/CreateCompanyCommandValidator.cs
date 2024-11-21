using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Administracion.Companies.Commands.CreateCompanyCommand
{
    public class CreateContractsUserCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public readonly IRepositoryAsync<RegimenFiscal> _repositoryAsyncRegimenFiscal;
        public CreateContractsUserCompanyCommandValidator(IRepositoryAsync<RegimenFiscal> repositoryAsyncRegimenFiscal)
        {
            _repositoryAsyncRegimenFiscal = repositoryAsyncRegimenFiscal;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name es obligatorio");

            RuleFor(c => c.SalaryDays)
                .NotNull().WithMessage("Dias de salario es obligatorio");

            RuleFor(c => c.RegistroPatronal)
                .NotEmpty().WithMessage("RegistroPatronal es obligatorio");

            RuleFor(c => c.PostalCode)
                .NotEmpty().WithMessage("PostalCode es obligatorio");

            RuleFor(f => f.RegimenFiscalId)
                .NotEmpty().WithMessage("RegimenFiscalId es obligatorio")
                .MustAsync(async (RegimenFiscalId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncRegimenFiscal.GetByIdAsync(RegimenFiscalId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro RegimenFiscalId no encontrado en RegimenFiscales");


            RuleFor(c => c.Rfc)
                .Matches(@"^[A-ZÑ&]{4}|[A-ZÑ&]{3}[0-9]{6}").WithMessage("Rfc no cumple con el formato requerido")
                .MaximumLength(13).WithMessage("Rfc no puede ser de más de {MaxLength} caracteres");
        }
    }
}
