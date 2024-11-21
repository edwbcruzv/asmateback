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

namespace Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Queries.GetMovimientoAhorroVoluntario
{
    public class GetMovimientoAhorroVoluntarioByIdQuery : IRequest<Response<MovimientoAhorroVoluntarioDTO>>
    {
        public int AhorroVoluntarioId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public class Handler : IRequestHandler<GetMovimientoAhorroVoluntarioByIdQuery, Response<MovimientoAhorroVoluntarioDTO>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoAhorroVoluntarioDTO>> Handle(GetMovimientoAhorroVoluntarioByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetBySpecAsync(new MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroVoluntarioId, request.MovimientoId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId}, empleado_id {request.EmployeeId},  ahorro_id {request.AhorroVoluntarioId}, movimiento_id{request.MovimientoId}");
                }

                var dto = _mapper.Map<MovimientoAhorroVoluntarioDTO>(elem);
                return new Response<MovimientoAhorroVoluntarioDTO>(dto, "MovimientoAhorroVoluntario encontrado con exito.");
            }
        }
    }
}
