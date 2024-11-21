using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Prestamos.Commands.CreatePrestamoCommand
{
    public class CreatePrestamoCommandValidator : AbstractValidator<CreatePrestamoCommand>
    {
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

        public CreatePrestamoCommandValidator(IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Employee> repositoryAsyncEmployee)
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
                .NotEmpty().WithMessage("La fecha de inicio es obligatorio");

            RuleFor(x => x.FondoGarantia)
                .NotEmpty().WithMessage("El fondo de garantia es obligatorio");

            RuleFor(x => x.TazaFondoGarantia)
                .NotEmpty().WithMessage("La taza del fondo de garantia es obligatorio");

            RuleFor(x => x.Plazo)
                .NotEmpty().WithMessage("El plazo es obligatorio");
            
            RuleFor(x => x.Monto)
                .NotEmpty().WithMessage("El monto es obligatorio");

            RuleFor(x => x.Interes)
                .NotEmpty().WithMessage("El interes es obligatorio");
            
            RuleFor(x => x.TazaInteres)
                .NotEmpty().WithMessage("La taza de interes es obligatorio");

            RuleFor(x => x.Descuento)
                .NotEmpty().WithMessage("El descuento es obligatorio");

            RuleFor(x => x.Tipo)
                .IsInEnum()
                .WithMessage("El tipo del prestamo no es valido");
        }   

    }
}
