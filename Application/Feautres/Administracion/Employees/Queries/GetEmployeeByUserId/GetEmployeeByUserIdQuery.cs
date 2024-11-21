using Application.DTOs.Administracion;
using Application.Feautres.Catalogos.Bancos.Queries.GetAllBancos;
using Application.Interfaces;
using Application.Specifications.Employees;
using Application.Wrappers;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Employees.Queries.GetEmployeeByUserId
{
    public class GetEmployeeByUserIdQuery : IRequest<Response<EmployeeDto>>
    {
        public int UserId { get; set; }

        public class Handler: IRequestHandler<GetEmployeeByUserIdQuery,Response<EmployeeDto>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;

            public Handler(IRepositoryAsync<Employee> repositoryAsyncEmployee, IMapper mapper, IRepositoryAsync<Banco> repositoryAsyncBanco)
            {
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _mapper = mapper;
                _repositoryAsyncBanco = repositoryAsyncBanco;
            }

            public async Task<Response<EmployeeDto>> Handle(GetEmployeeByUserIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await _repositoryAsyncEmployee.FirstOrDefaultAsync(new EmployeeByUserIdSpecification(request.UserId));

                if (employee == null)
                {
                    throw new KeyNotFoundException($"No se ha encontrado ningún empleado con el usuario {request.UserId}");
                }
                else
                {
                    var banco = await _repositoryAsyncBanco.GetByIdAsync(employee.BancoId);

                    EmployeeDto employee_dto = _mapper.Map<EmployeeDto>(employee);
                    if (banco != null)
                    {
                        employee_dto.Banco = banco.Descripcion;
                    }


                    Response<EmployeeDto> respuesta = new Response<EmployeeDto>();
                    respuesta.Succeeded = true;
                    respuesta.Data = employee_dto;
                    return respuesta;
                }

            }
        }
    }
}
