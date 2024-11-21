using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosWise;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosAhorroWise.Queries.GetMovimientoAhorroWiseById
{
    public class GetMovimientoAhorroWiseByIdQuery : IRequest<Response<MovimientoAhorroWiseDTO>>
    {
        public int AhorroWiseId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public class Handler : IRequestHandler<GetMovimientoAhorroWiseByIdQuery, Response<MovimientoAhorroWiseDTO>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroWise> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoAhorroWiseDTO>> Handle(GetMovimientoAhorroWiseByIdQuery request, CancellationToken cancellationToken)
            {
                var elem = await _repositoryAsync.GetBySpecAsync(new MovimientoAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroWiseId, request.MovimientoId));

                if (elem == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId}, empleado_id {request.EmployeeId},  ahorro_id {request.AhorroWiseId}, movimiento_id{request.MovimientoId}");
                }

                var dto = _mapper.Map<MovimientoAhorroWiseDTO>(elem);
                return new Response<MovimientoAhorroWiseDTO>(dto, "MovimientoAhorroWise encontrado con exito.");
            }
        }
    }
}
