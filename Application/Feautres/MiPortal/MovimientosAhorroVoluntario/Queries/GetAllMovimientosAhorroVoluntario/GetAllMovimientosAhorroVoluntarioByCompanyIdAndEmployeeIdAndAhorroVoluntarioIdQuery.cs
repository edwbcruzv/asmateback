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
    public class GetAllMovimientosAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdQuery : IRequest<Response<List<MovimientoAhorroVoluntarioDTO>>>
    {
        public int AhorroVoluntarioId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<GetAllMovimientosAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdQuery, Response<List<MovimientoAhorroVoluntarioDTO>>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<MovimientoAhorroVoluntarioDTO>>> Handle(GetAllMovimientosAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroVoluntarioId));

                var list_dto = _mapper.Map<List<MovimientoAhorroVoluntarioDTO>>(list);

                return new Response<List<MovimientoAhorroVoluntarioDTO>>(list_dto);
            }
        }
    }
}
