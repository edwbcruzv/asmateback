using Application.DTOs.Kanban.Tickets;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<Response<TicketDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetTicketByIdQuery, Response<TicketDTO>>
        {
            private readonly IRepositoryAsync<Ticket> _repositoryAsync;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;

            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Ticket> repositoryAsync, IMapper mapper, IRepositoryAsync<Employee> repositoryAsyncEmployee, IRepositoryAsync<Sistema> repositoryAsyncSistema, IRepositoryAsync<Departamento> repositoryAsyncDepartamento, IRepositoryAsync<Company> repositoryAsyncCompany)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _repositoryAsyncSistema = repositoryAsyncSistema;
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
                _repositoryAsyncCompany = repositoryAsyncCompany;
            }

            public async Task<Response<TicketDTO>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var dto = _mapper.Map<TicketDTO>(elem);

                var empleado_asignado = await _repositoryAsyncEmployee.GetByIdAsync(elem.EmployeeAsignadoId);
                dto.EmployeeAsignado = empleado_asignado.NombreCompletoOrdenado();

                var empleado_creador = await _repositoryAsyncEmployee.GetByIdAsync(elem.EmployeeCreadorId);
                dto.EmployeeCreador = empleado_creador.NombreCompletoOrdenado();

                var sistema = await _repositoryAsyncSistema.GetByIdAsync(elem.SistemaId);
                dto.Sistema = sistema.Nombre;

                var company = await _repositoryAsyncCompany.GetByIdAsync(elem.CompanyId);
                dto.Company = company.Name;

                var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(elem.DepartamentoId);
                dto.Departamento = departamento.Descripcion;

                return new Response<TicketDTO>(dto);

            }
        }

    }
}
