using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosVoluntario;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetAhorroVoluntarioById
{
    public class GetAhorroVoluntarioByIdQuery : IRequest<Response<AhorroVoluntarioDTO>>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAhorroVoluntarioByIdQuery, Response<AhorroVoluntarioDTO>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsyncMovimientoAhorroVoluntario;
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsync;
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroVoluntario> repositoryAsync, IMapper mapper, IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsyncMovimientoAhorroVoluntario, IRepositoryAsync<Employee> repositoryAsyncEmployee)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _repositoryAsyncMovimientoAhorroVoluntario = repositoryAsyncMovimientoAhorroVoluntario;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
            }

            public async Task<Response<AhorroVoluntarioDTO>> Handle(GetAhorroVoluntarioByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetByIdAsync(request.Id);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }

                var employee = await _repositoryAsyncEmployee.GetByIdAsync(elem.EmployeeId);

                if (employee == null)
                {
                    throw new KeyNotFoundException($"Empleado no encontrado no encontrado con el id {elem.EmployeeId}");
                }

                var list = await _repositoryAsyncMovimientoAhorroVoluntario.ListAsync(new MovimientoAhorroVoluntarioByAhorroVoluntarioIdSpecification(request.Id));

                var list_dto = _mapper.Map<List<MovimientoAhorroVoluntarioDTO>>(list);

                elem.Movimientos = list;
                elem.Employee = employee;

                var dto = _mapper.Map<AhorroVoluntarioDTO>(elem);
                dto.Movimientos = list_dto;
                return new Response<AhorroVoluntarioDTO>(dto, "AhorroVoluntario encontrado con exito.");
            }
        }
    }
}
