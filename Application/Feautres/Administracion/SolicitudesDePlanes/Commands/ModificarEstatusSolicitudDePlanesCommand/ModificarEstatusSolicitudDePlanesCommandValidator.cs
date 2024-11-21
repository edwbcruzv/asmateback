using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.SolicitudesDePlanes.Commands.ModificarEstatusSolicitudDePlanesCommand
{
    public class ModificarEstatusSolicitudDePlanesCommandValidator : AbstractValidator<ModificarEstatusSolicitudDePlanesCommand>
    {

        public readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        public ModificarEstatusSolicitudDePlanesCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany)
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

            RuleFor(f => f.Tipo)
                .NotEmpty().WithMessage("Tipo es obligatorio");

            RuleFor(f => f.Id)
                .NotEmpty().WithMessage("Id es obligatorio");

            RuleFor(f => f.NuevoEstatusId)
                .LessThanOrEqualTo(3).WithMessage("No se reconoce el id del estatus proporcionado")
                .GreaterThanOrEqualTo(0).WithMessage("No se reconoce el id del estatus proporcionado");
        }
    }
}
