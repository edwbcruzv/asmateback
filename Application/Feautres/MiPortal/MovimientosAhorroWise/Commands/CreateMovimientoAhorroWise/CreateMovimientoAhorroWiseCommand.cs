using Application.DTOs.MiPortal.Ahorros;
using Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.CreateMovimientoAhorroWise;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosWise;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.CreateMovimientoAhorroWise
{
    public class CreateMovimientoAhorroWiseCommand : IRequest<Response<MovimientoAhorroWiseDTO>>
    {
        public int AhorroWiseId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        //public int MovimientoId { get; set; } // se asigna automaticamente
        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Interes { get; set; }

        public class Handler : IRequestHandler<CreateMovimientoAhorroWiseCommand, Response<MovimientoAhorroWiseDTO>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsyncMovimientoAhorroWise;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroWise> repositoryAsyncMovimientoAhorroWise, IMapper mapper)
            {
                _repositoryAsyncMovimientoAhorroWise = repositoryAsyncMovimientoAhorroWise;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoAhorroWiseDTO>> Handle(CreateMovimientoAhorroWiseCommand request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsyncMovimientoAhorroWise.ListAsync(new MovimientoAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroWiseId));

                // Calcular el siguiente MovimientoId
                var ultimoMovimientoId = list.Any() ? list.Max(m => m.MovimientoId) : 0;
                var siguienteMovimientoId = ultimoMovimientoId + 1;

                var mov_ahorro_wise = _mapper.Map<MovimientoAhorroWise>(request);
                mov_ahorro_wise.MovimientoId = siguienteMovimientoId;

                var data = await _repositoryAsyncMovimientoAhorroWise.AddAsync(mov_ahorro_wise);
                var dto = _mapper.Map<MovimientoAhorroWiseDTO>(data);
                return new Response<MovimientoAhorroWiseDTO>(dto);
            }
        }
    }
}
