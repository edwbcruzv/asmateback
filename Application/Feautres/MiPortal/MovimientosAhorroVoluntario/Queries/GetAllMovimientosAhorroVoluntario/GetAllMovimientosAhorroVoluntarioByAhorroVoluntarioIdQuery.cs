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

namespace Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Queries.GetAllMovimientosAhorroVoluntario
{
    public class GetAllMovimientosAhorroVoluntarioByAhorroVoluntarioIdQuery : IRequest<Response<List<MovimientoAhorroVoluntarioDTO>>>
    {

        public int EmployeeId { get; set; }

        public class Handler : IRequestHandler<GetAllMovimientosAhorroVoluntarioByAhorroVoluntarioIdQuery, Response<List<MovimientoAhorroVoluntarioDTO>>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<MovimientoAhorroVoluntarioDTO>>> Handle(GetAllMovimientosAhorroVoluntarioByAhorroVoluntarioIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new MovimientoAhorroVoluntarioByAhorroVoluntarioIdSpecification(request.EmployeeId));

                var list_dto = _mapper.Map<List<MovimientoAhorroVoluntarioDTO>>(list);

                return new Response<List<MovimientoAhorroVoluntarioDTO>>(list_dto);
            }
        }
    }
}
