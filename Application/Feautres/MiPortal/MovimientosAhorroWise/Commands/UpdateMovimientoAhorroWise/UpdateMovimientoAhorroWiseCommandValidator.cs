using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.UpdateMovimientoAhorroWise
{
    public class UpdateMovimientoAhorroWiseCommandValidator : AbstractValidator<UpdateMovimientoAhorroWiseCommand>
    {
        private readonly IRepositoryAsync<AhorroWise> _repositoryAsyncAhorroWise;
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        public UpdateMovimientoAhorroWiseCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<AhorroWise> repositoryAsyncAhorroWise)
        {
            _repositoryAsyncAhorroWise = repositoryAsyncAhorroWise;
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;

            RuleFor(x => x.AhorroWiseId)
                .NotEmpty().WithMessage("El AhorroWiseId es obligatorio")
                .MustAsync(async (AhorroWiseId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncAhorroWise.GetByIdAsync(AhorroWiseId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El AhorroWiseId no existe");

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

            RuleFor(x => x.Periodo)
                .NotEmpty().WithMessage("El Periodo es obligatorio");

            RuleFor(x => x.Monto)
                .NotEmpty().WithMessage("El Monto es obligatorio");

            RuleFor(x => x.Rendimiento)
                .NotEmpty().WithMessage("El Rendimiento es obligatorio");

            RuleFor(x => x.EstadoTransaccion)
                .IsInEnum()
                .WithMessage("El EstadoTransaccion no es valido");

            RuleFor(x => x.Interes)
                .NotEmpty().WithMessage("El Interes es obligatorio");
        }
    }
}
