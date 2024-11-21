using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.Administracion;
using Application.Specifications.Employees;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Employees.Queries.GetAllEmployeesByDepartamentoId
{
    public class GetAllEmployeesByDepartamentoIdQuery : IRequest<Response<List<EmployeeDto>>>
    {

        public int DepartamentoId { get; set; }

        public class Handler : IRequestHandler<GetAllEmployeesByDepartamentoIdQuery, Response<List<EmployeeDto>>>
        {
            private readonly IRepositoryAsync<Puesto> _repositoryAsyncPuesto;
            private readonly IRepositoryAsync<Employee> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper, IRepositoryAsync<Puesto> repositoryAsyncPuesto)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncPuesto = repositoryAsyncPuesto;
            }

            public async Task<Response<List<EmployeeDto>>> Handle(GetAllEmployeesByDepartamentoIdQuery request, CancellationToken cancellationToken)
            {
                List<Puesto> list_puestos = await _repositoryAsyncPuesto.ListAsync(new PuestoByDepartamentoSpecification(request.DepartamentoId));

                List<Employee> empleados = new List<Employee>();
                foreach (Puesto item in list_puestos)
                {
                    var list_aux = await _repositoryAsync.ListAsync(new EmployeesByPuestoIdSpecification(item.Id), cancellationToken);
                    empleados.AddRange(list_aux);
                }

                var list_dto = _mapper.Map<List<EmployeeDto>>(empleados);

                return new Response<List<EmployeeDto>>(list_dto);
            }
        }
    }
}
