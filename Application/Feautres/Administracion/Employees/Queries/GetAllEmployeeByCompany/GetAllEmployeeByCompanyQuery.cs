using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Specifications.Employees;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Feautres.Administracion.Employees.Queries.GetAllEmployeeByCompany
{
    public class GetAllEmployeeByCompanyQuery : IRequest<Response<List<EmployeeDto>>>
    {
        public int CompanyId { set; get; }

        public class Handler : IRequestHandler<GetAllEmployeeByCompanyQuery, Response<List<EmployeeDto>>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IRepositoryAsync<Puesto> _repositoryAsyncPuesto;
            private readonly IRepositoryAsync<Departamento> _repositoryAsyncDepartamento;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;

            public Handler(
                IRepositoryAsync<Employee> repositoryAsyncEmployee,
                IRepositoryAsync<Puesto> repositoryAsyncPuesto,
                IRepositoryAsync<Departamento> repositoryAsyncDepartamento,
                IMapper mapper,
                IRepositoryAsync<Banco> repositoryAsyncBanco)
            {
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _repositoryAsyncPuesto = repositoryAsyncPuesto;
                _repositoryAsyncDepartamento = repositoryAsyncDepartamento;
                _mapper = mapper;
                _repositoryAsyncBanco = repositoryAsyncBanco;
            }

            public async Task<Response<List<EmployeeDto>>> Handle(GetAllEmployeeByCompanyQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsyncEmployee.ListAsync(new EmployeeByCompanySpecification(request.CompanyId));

                if (list == null)
                {
                    throw new KeyNotFoundException($"No se encontró la compañia con Id {request.CompanyId}");
                }

                var bancos = await _repositoryAsyncBanco.ListAsync();
                Dictionary<int, string> diccionario_bancos = new Dictionary<int, string>();
                diccionario_bancos = bancos.ToDictionary(x => x.Id, x => x.Descripcion.TrimEnd());

                var EmployeesDto = _mapper.Map<List<EmployeeDto>>(list);

                foreach(EmployeeDto employee_dto in EmployeesDto)
                {
                    if (employee_dto.BancoId != null)
                    {
                        employee_dto.Banco = diccionario_bancos[(int)employee_dto.BancoId];
                    }
                }

                var copyEmployeesDto = new List<EmployeeDto>();
                // var result = new List<(EmployeeDto Employee, string DescripcionDepartamento)>();

                foreach (var employee in EmployeesDto)
                {
                    if (employee.PuestoId != null)
                    {
                        employee.Departamento = await ObtenerDescripcionDepartamento(employee.PuestoId);
                    }

                    if (employee.BancoId != null)
                    {
                        employee.Banco = diccionario_bancos[(int)employee.BancoId];
                    }
                }
                return new Response<List<EmployeeDto>>(EmployeesDto);

            }

            private async Task<string> ObtenerDescripcionDepartamento(int? puestoId)
            {
                var puesto = await _repositoryAsyncPuesto.GetByIdAsync(puestoId);
                var departamento = await _repositoryAsyncDepartamento.GetByIdAsync(puesto.DepartamentoId);
                return departamento.Descripcion;
            }
        }
    }
}
