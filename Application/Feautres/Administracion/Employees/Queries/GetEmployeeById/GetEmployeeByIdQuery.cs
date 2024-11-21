using Application.DTOs.Administracion;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feautres.Administracion.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<Response<EmployeeDto>>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDto>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IRepositoryAsync<Banco> _repositoryAsyncBanco;

            public Handler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper, IRepositoryAsync<Banco> repositoryAsyncBanco)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncBanco = repositoryAsyncBanco;
            }

            public async Task<Response<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var item = await _repositoryAsync.GetByIdAsync(request.Id);

                if (item == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var banco = await _repositoryAsyncBanco.GetByIdAsync(item.BancoId);

                    var dto = _mapper.Map<EmployeeDto>(item);
                    if (banco != null)
                    {
                        dto.Banco = banco.Descripcion;
                    }
                    return new Response<EmployeeDto>(dto);
                }

            }
        }
    }
}
