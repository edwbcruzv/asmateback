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

namespace Application.Feautres.MiPortal.MovimientosAhorroWise.Queries.GetAllMovimientosAhorroWise
{
    public class GetAllMovimientosAhorroWiseByCompanyIdQuery : IRequest<Response<List<MovimientoAhorroWiseDTO>>>
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }

        public class Handler : IRequestHandler<GetAllMovimientosAhorroWiseByCompanyIdQuery, Response<List<MovimientoAhorroWiseDTO>>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsync;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroWise> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<MovimientoAhorroWiseDTO>>> Handle(GetAllMovimientosAhorroWiseByCompanyIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new MovimientoAhorroWiseByCompanyIdSpecification(request.CompanyId));

                var list_dto = _mapper.Map<List<MovimientoAhorroWiseDTO>>(list);

                return new Response<List<MovimientoAhorroWiseDTO>>(list_dto);
            }
        }
    }
}
