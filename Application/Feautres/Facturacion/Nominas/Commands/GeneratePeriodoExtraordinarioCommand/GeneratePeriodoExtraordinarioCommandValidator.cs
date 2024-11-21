using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Feautres.Facturacion.Facturas.Commands.CreateFacturaCommand;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Feautres.Facturacion.Nominas.Commands.GeneratePeriodoExtraordinarioCommand
{
    public class GeneratePeriodoExtraordinarioCommandValidator : AbstractValidator<GeneratePeriodoExtraordinarioCommand>
    {
        public readonly IRepositoryAsync<Company> _repositoryAsyncCompany;

        public GeneratePeriodoExtraordinarioCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;

            RuleFor(p => p.CompanyId).NotEmpty().WithMessage("CompanyId es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var company = await _repositoryAsyncCompany.GetByIdAsync(CompanyId);

                    if (company == null) return false;

                    return true;
                })
                .WithMessage($"Registro CompanyId no encontrado en companies");

        }
    }
}
