using Application.Interfaces;
using Application.Specifications.Companies;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
        private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;
        private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
        private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
        private readonly IRepositoryAsync<Puesto> _repositoryAsyncPuesto;
        private readonly IRepositoryAsync<Estado> _repositoryAsyncEstado;
        private readonly IRepositoryAsync<TipoSolicitudTicket> _repositoryAsyncTipoSolicitudTicket;

        public CreateTicketCommandValidator(IRepositoryAsync<Departamento> repositoryAsyncDepartamento,
                IRepositoryAsync<Company> repositoryAsyncCompany,
                IRepositoryAsync<TipoSolicitudTicket> repositoryAsyncTipoSolicitudTicket,
                IRepositoryAsync<Employee> repositoryAsyncEmployee,
                IRepositoryAsync<Puesto> repositoryAsyncPuesto,
                IRepositoryAsync<Sistema> repositoryAsyncSistema,
                IRepositoryAsync<Estado> repositoryAsyncEstado)
        {
            _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
            _repositoryAsyncSistema = repositoryAsyncSistema;
            _repositoryAsyncCompany = repositoryAsyncCompany;
            _repositoryAsyncEmployee = repositoryAsyncEmployee;
            _repositoryAsyncPuesto = repositoryAsyncPuesto;
            _repositoryAsyncEstado = repositoryAsyncEstado;
            _repositoryAsyncTipoSolicitudTicket = repositoryAsyncTipoSolicitudTicket;

            RuleFor(f => f.TipoSolicitudTicketId)
                .NotEmpty().WithMessage("TipoSolicitudTicketId es obligatorio")
                .MustAsync(async (TipoSolicitudTicketId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncTipoSolicitudTicket.GetByIdAsync(TipoSolicitudTicketId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro TipoSolicitudTicketId no existe");


            RuleFor(f => f.CompanyId)
                .NotEmpty().WithMessage("CompanyId es obligatorio")
                .MustAsync(async (CompanyId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncCompany.GetByIdAsync(CompanyId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro CompanyId no existe");

            RuleFor(f => f.SistemaId)
                .NotEmpty().WithMessage("SistemaId es obligatorio")
                .MustAsync(async (SistemaId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncSistema.GetByIdAsync(SistemaId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro SistemaId no existe");

            RuleFor(f => f.DepartamentoId)
                .NotEmpty().WithMessage("DepartamentoId es obligatorio")
                .MustAsync(async (DepartamentoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncDepartamento.GetByIdAsync(DepartamentoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro DepartamentoId no existe");

            RuleFor(f => f.EmployeeAsignadoId)
                .NotEmpty().WithMessage("EmployeeAsignadoId es obligatorio")
                .MustAsync(async (EmployeeAsignadoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncEmployee.GetByIdAsync(EmployeeAsignadoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro EmployeeAsignadoId no existe");

            RuleFor(f => f.EmployeeCreadorId)
                .NotEmpty().WithMessage("EmployeeCreadorId es obligatorio")
                .MustAsync(async (EmployeeCreadorId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncEmployee.GetByIdAsync(EmployeeCreadorId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro EmployeeCreadorId no existe");

            RuleFor(f => f.EstadoId)
                .NotEmpty().WithMessage("EstadoId es obligatorio")
                .MustAsync(async (EstadoId, cancellationToken) =>
                {
                    var item = await _repositoryAsyncEstado.GetByIdAsync(EstadoId);

                    if (item == null) return false;

                    return true;
                })
                .WithMessage($"Registro EstadoId no existe");


            RuleFor(f => f.OpcionMenu)
                .NotEmpty().WithMessage("OpcionMenu es obligatorio");

            RuleFor(f => f.OpcionMenu)
                .NotEmpty().WithMessage("Prioridad es obligatorio");

            RuleFor(x => x.Prioridad)
                .IsInEnum()
                .WithMessage("La prioridad no es valido");
        }
    }
}
