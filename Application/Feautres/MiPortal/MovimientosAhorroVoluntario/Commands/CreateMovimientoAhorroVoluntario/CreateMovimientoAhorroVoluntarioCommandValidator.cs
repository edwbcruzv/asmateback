using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.CreateMovimientoAhorroVoluntario
{
    public class CreateMovimientoAhorroVoluntarioCommandValidator : AbstractValidator<CreateMovimientoAhorroVoluntarioCommand>
    {
        private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorrovoluntario;
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        public CreateMovimientoAhorroVoluntarioCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorrovoluntario)
        {
            _repositoryAsyncAhorrovoluntario = repositoryAsyncAhorrovoluntario;
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;

            RuleFor(x => x.AhorroVoluntarioId)
                .NotEmpty().WithMessage("El AhorroVoluntarioId es obligatorio")
                .MustAsync(async (AhorroVoluntarioId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncAhorrovoluntario.GetByIdAsync(AhorroVoluntarioId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El AhorroVoluntarioId no existe");

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

            RuleFor(x => x.EstadoTransaccion)
                .IsInEnum()
                .WithMessage("El EstadoTransaccion no es valido");
        }
    }
}
