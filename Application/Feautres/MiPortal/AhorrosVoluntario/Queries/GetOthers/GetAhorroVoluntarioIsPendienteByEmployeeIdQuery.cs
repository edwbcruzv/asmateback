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

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Queries.GetOthers
{
    public class GetAhorroVoluntarioIsPendienteByEmployeeIdQuery : IRequest<Response<AhorroVoluntarioDTO>>
    {
        public int EmployeeId { get; set; }

        public class Handler : IRequestHandler<GetAhorroVoluntarioIsPendienteByEmployeeIdQuery, Response<AhorroVoluntarioDTO>>
        {
            private readonly IRepositoryAsync<Employee> _repositoryAsyncEmployee;
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IAhorroVoluntarioService _serviceAhorroVoluntario;
            private readonly IMapper _mapper;

            public Handler(IAhorroVoluntarioService serviceAhorroVoluntario, IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario, IRepositoryAsync<Employee> repositoryAsyncEmployee, IMapper mapper)
            {
                _serviceAhorroVoluntario = serviceAhorroVoluntario;
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
                _repositoryAsyncEmployee = repositoryAsyncEmployee;
                _mapper = mapper;
            }

            public async Task<Response<AhorroVoluntarioDTO>> Handle(GetAhorroVoluntarioIsPendienteByEmployeeIdQuery request, CancellationToken cancellationToken)
            {

                var elem = await _repositoryAsyncEmployee.GetByIdAsync(request.EmployeeId);

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Empledado no encontrado con el id {request.EmployeeId}");
                }

                var ahorro_voluntario = await _repositoryAsyncAhorroVoluntario.FirstOrDefaultAsync(new AhorroVoluntarioByEmployeeIdAndIsPendienteSpecification(request.EmployeeId));

                if (ahorro_voluntario == null)
                {
                    throw new KeyNotFoundException($"Empleado no cuenta con ahorros pendientes.");
                }
                    var dto = _mapper.Map<AhorroVoluntarioDTO>(ahorro_voluntario);

                return new Response<AhorroVoluntarioDTO>(dto, "AhorroVoluntario encontrado con exito.");
            }
        }
    }
}
