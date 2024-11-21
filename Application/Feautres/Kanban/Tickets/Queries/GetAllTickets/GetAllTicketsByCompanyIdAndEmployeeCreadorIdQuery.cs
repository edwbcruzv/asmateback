using Application.DTOs.Kanban.Tickets;
using Application.Interfaces;
using Application.Specifications.Kanban.Tickets;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Kanban.Tickets.Queries.GetAllTickets
{
    public class GetAllTicketsByCompanyIdAndEmployeeCreadorIdQuery : IRequest<Response<List<TicketDTO>>>
    {

        public int CompanyId { get; set; }
        public int EmployeeCreadorId { get; set; }

        public class Handler : IRequestHandler<GetAllTicketsByCompanyIdAndEmployeeCreadorIdQuery, Response<List<TicketDTO>>>
        {
            private readonly IRepositoryAsync<Ticket> _repositoryAsync;
            private readonly IRepositoryAsync<Company> _repositoryAsyncCompany;
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IRepositoryAsync<Sistema> _repositoryAsyncSistema;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Ticket> repositoryAsync, IMapper mapper, IRepositoryAsync<Company> repositoryAsyncCompany, IRepositoryAsync<Departamento> repositoryAsyncDepartamento, IRepositoryAsync<Sistema> repositoryAsyncSistema, IRepositoryAsync<Employee> repositoryAsyncEmployee)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncCompany = repositoryAsyncCompany;
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
                _repositoryAsyncSistema = repositoryAsyncSistema;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
            }

            public async Task<Response<List<TicketDTO>>> Handle(GetAllTicketsByCompanyIdAndEmployeeCreadorIdQuery request, CancellationToken cancellationToken)
            {
                var list_ticket = await _repositoryAsync.ListAsync(new TicketByCompanyIdAndEmployeeCreadorIdSpecification(request.CompanyId,request.EmployeeCreadorId));

                var list_ticket_dto = new List<TicketDTO>();

                var list_company = await _repositoryAsyncCompany.ListAsync();
                var list_departamento = await _repositoryAsyncDepartamento.ListAsync();
                var list_sistema = await _repositoryAsyncSistema.ListAsync();
                var list_employee = await _repositoryAsyncEmployee.ListAsync();

                Dictionary<int, string> dicc_company = list_company.ToDictionary(e => e.Id, e => e.Name);
                Dictionary<int, string> dicc_departamento = list_departamento.ToDictionary(e => e.Id, e => e.Descripcion);
                Dictionary<int, string> dicc_sistema = list_sistema.ToDictionary(e => e.Id, e => e.Nombre);
                Dictionary<int, string> dicc_employee = list_employee.ToDictionary(e => e.Id, e => e.Nombre);
                Dictionary<int, string> dicc_employee_compl = list_employee.ToDictionary(e => e.Id, e => e.NombreCompletoOrdenado());

                foreach (var elem in list_ticket)
                {
                    var dto = _mapper.Map<TicketDTO>(elem);
                    dto.Company = dicc_company[elem.CompanyId];
                    dto.Departamento = dicc_departamento[elem.DepartamentoId];
                    dto.Sistema = dicc_sistema[elem.SistemaId];
                    dto.EmployeeAsignado = dicc_employee[elem.EmployeeAsignadoId];
                    dto.EmployeeAsignadoCompl = dicc_employee_compl[elem.EmployeeAsignadoId];
                    dto.EmployeeCreador = dicc_employee[elem.EmployeeCreadorId];
                    dto.EmployeeCreadorCompl = dicc_employee_compl[elem.EmployeeCreadorId];

                    list_ticket_dto.Add(dto);
                }


                return new Response<List<TicketDTO>>(list_ticket_dto);
            }
        }
    }
}
