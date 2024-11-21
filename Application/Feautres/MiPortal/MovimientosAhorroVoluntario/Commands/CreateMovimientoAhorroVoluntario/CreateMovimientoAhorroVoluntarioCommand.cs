using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
using Application.Specifications.MiPortal.AhorrosVoluntario;
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

namespace Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.CreateMovimientoAhorroVoluntario
{
    public class CreateMovimientoAhorroVoluntarioCommand : IRequest<Response<MovimientoAhorroVoluntarioDTO>>
    {
        public int AhorroVoluntarioId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        //public int MovimientoId { get; set; } // se asigna automaticamente
        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Interes { get; set; }

        public class Handler : IRequestHandler<CreateMovimientoAhorroVoluntarioCommand, Response<MovimientoAhorroVoluntarioDTO>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsyncMovimientoAhorroVoluntario;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsyncMovimientoAhorroVoluntario, IMapper mapper)
            {
                _repositoryAsyncMovimientoAhorroVoluntario = repositoryAsyncMovimientoAhorroVoluntario;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoAhorroVoluntarioDTO>> Handle(CreateMovimientoAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {

                var list = await _repositoryAsyncMovimientoAhorroVoluntario.ListAsync(new MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdSpecification( request.CompanyId, request.EmployeeId, request.AhorroVoluntarioId));

                // Calcular el siguiente MovimientoId
                var ultimoMovimientoId = list.Any() ? list.Max(m => m.MovimientoId) : 0;
                var siguienteMovimientoId = ultimoMovimientoId + 1;

                var mov_ahorro_vol = _mapper.Map<MovimientoAhorroVoluntario>(request);
                mov_ahorro_vol.MovimientoId = siguienteMovimientoId;

                var data = await _repositoryAsyncMovimientoAhorroVoluntario.AddAsync(mov_ahorro_vol);
                var dto = _mapper.Map<MovimientoAhorroVoluntarioDTO>(data);
                return new Response<MovimientoAhorroVoluntarioDTO>(dto);
            }
        }
    }
}
