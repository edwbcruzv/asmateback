using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosPrestamo.Commands.CreateMovimientoPrestamo
{
    public class CreateMovimientoPrestamoCommandValidator : AbstractValidator<CreateMovimientoPrestamoCommand>
    {
        private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        public CreateMovimientoPrestamoCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Prestamo> repositoryAsyncPrestamo)
        {
            _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;

            RuleFor(x => x.PrestamoId)
                .NotEmpty().WithMessage("El PrestamoId es obligatorio")
                .MustAsync(async (PrestamoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncPrestamo.GetByIdAsync(PrestamoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"El PrestamoId no existe");

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

            RuleFor(x => x.Capital)
                .NotEmpty().WithMessage("El Capital es obligatorio");

            RuleFor(x => x.FondoGarantia)
                .NotEmpty().WithMessage("El FondoGarantia es obligatorio");

            RuleFor(x => x.SaldoActual)
                .NotEmpty().WithMessage("El SaldoActual es obligatorio");

            RuleFor(x => x.Interes)
                .NotEmpty().WithMessage("El Interes es obligatorio");

            RuleFor(x => x.Moratorio)
                .NotEmpty().WithMessage("El Moratorio es obligatorio");
        }
    }
}
