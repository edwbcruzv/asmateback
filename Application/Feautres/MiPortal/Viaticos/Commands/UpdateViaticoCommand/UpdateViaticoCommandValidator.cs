using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Viaticos.Commands.UpdateViaticoCommand
{
    public class UpdateViaticoCommandValidator : AbstractValidator<UpdateViaticoCommand>
    {
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
        private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;

        public UpdateViaticoCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Banco> repositoryAsyncBanco)
        {
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncBanco = repositoryAsyncBanco;

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

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es obligatorio");

            RuleFor(x => x.Estatus)
                .NotEmpty().WithMessage("El estatus es obligatorio");

            RuleFor(x => x.Monto)
                .NotEmpty().WithMessage("El monto es obligatorio");

            RuleFor(x => x.BancoId)
                .NotEmpty().WithMessage("La Compania es obligatorio")
                .MustAsync(async (BancoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncBanco.GetByIdAsync(BancoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El banco no existe");

            //RuleFor(x => x.Tipo)
            //    .NotEmpty().WithMessage("El tipo es obligatorio");
        }

    }
}
