using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.SolicitudesDePlanes.Queries.GetPrestamosYAhorrosPorCompania
{
    public class GetPrestamosYAhorrosPorCompaniaCommandValidator : AbstractValidator<GetPrestamosYAhorrosPorCompaniaCommand>
    {
        public readonly IRepositoryAsync<Company> _repositoryAsyncCompany;

        public GetPrestamosYAhorrosPorCompaniaCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;


            RuleFor(f => f.CompanyId)
                .NotEmpty().WithMessage("CompanyId es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var company = await _repositoryAsyncCompany.GetByIdAsync(CompanyId);

                    if (company == null) return false;

                    return true;
                })
                .WithMessage($"No se ha encontrado la compañía con el Id proporcionado");
        }


    }
}
