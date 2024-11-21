using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosWise.Commands.CreateAhorroWiseCommand
{
    public class CreateAhorroWiseCommandValidator : AbstractValidator<CreateAhorroWiseCommand>
    {
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        public CreateAhorroWiseCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("El empleado es obligatorio")
                .MustAsync(async (EmployeeId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncEmployee.GetByIdAsync(EmployeeId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El empleado no existe");

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("La Compania es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncCompany.GetByIdAsync(CompanyId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"La Compania no existe");

            RuleFor(x => x.FechaInicio)
                .NotEmpty().WithMessage("El FechaInicio es obligatorio");

            //RuleFor(x => x.FechaFinal)
            //    .NotEmpty().WithMessage("El FechaFinal es obligatorio");

            RuleFor(x => x.PeriodoInicial)
                .NotEmpty().WithMessage("El PeriodoInicial es obligatorio");

            //RuleFor(x => x.PeriodoFinal)
            //    .NotEmpty().WithMessage("El PeriodoFinal es obligatorio");

            RuleFor(x => x.Estatus)
                .IsInEnum()
                .WithMessage("El estatus no es valido");

            //RuleFor(x => x.Rendimiento)
            //    .NotEmpty().WithMessage("El Rendimiento es obligatorio");
        }
    }
}
