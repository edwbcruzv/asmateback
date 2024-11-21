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
    public class GetAllMovimientosAhorroWiseByEmployeeIdQuery : IRequest<Response<List<MovimientoAhorroWiseDTO>>>
    {

        public int EmployeeId { get; set; }

        public class Handler : IRequestHandler<GetAllMovimientosAhorroWiseByEmployeeIdQuery, Response<List<MovimientoAhorroWiseDTO>>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IAhorroWiseService _ahorroWiseService;

            public Handler(
                IRepositoryAsync<MovimientoAhorroWise> repositoryAsync,
                IMapper mapper,
                IAhorroWiseService ahorroWiseService)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _ahorroWiseService = ahorroWiseService;
            }

            public async Task<Response<List<MovimientoAhorroWiseDTO>>> Handle(GetAllMovimientosAhorroWiseByEmployeeIdQuery request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsync.ListAsync(new MovimientoAhorroWiseByEmployeeIdSpecification(request.EmployeeId));

                var list_dto = _mapper.Map<List<MovimientoAhorroWiseDTO>>(list);
                foreach(var item in list_dto)
                {
                    item.SaldoActual = await _ahorroWiseService.CalcularTotalAhorroWise(request.EmployeeId, item.Periodo);
                }

                return new Response<List<MovimientoAhorroWiseDTO>>(list_dto);
            }
        }
    }
}
