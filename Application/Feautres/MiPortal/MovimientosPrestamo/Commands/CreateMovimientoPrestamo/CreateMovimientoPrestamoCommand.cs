using Application.DTOs.MiPortal.Prestamos;
using Application.Interfaces;
using Application.Specifications.MiPortal.Prestamos;
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

namespace Application.Feautres.MiPortal.MovimientosPrestamo.Commands.CreateMovimientoPrestamo
{
    public class CreateMovimientoPrestamoCommand : IRequest<Response<MovimientoPrestamoDTO>>
    {
        public int PrestamoId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        //public int MovimientoId { get; set; } // se asigna automaticamente
        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Capital { get; set; }
        public float FondoGarantia { get; set; }
        public float SaldoActual { get; set; }
        public float Interes { get; set; }
        public float Moratorio { get; set; }

        public class Handler : IRequestHandler<CreateMovimientoPrestamoCommand, Response<MovimientoPrestamoDTO>>
        {
            private readonly IRepositoryAsync<MovimientoPrestamo> _repositoryAsyncMovimientoPrestamo;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoPrestamo> repositoryAsyncMovimientoPrestamo, IMapper mapper)
            {
                _repositoryAsyncMovimientoPrestamo = repositoryAsyncMovimientoPrestamo;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoPrestamoDTO>> Handle(CreateMovimientoPrestamoCommand request, CancellationToken cancellationToken)
            {
                var list = await _repositoryAsyncMovimientoPrestamo.ListAsync(new MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdSpecification(request.CompanyId, request.EmployeeId, request.PrestamoId));

                // Calcular el siguiente MovimientoId
                var ultimoMovimientoId = list.Any() ? list.Max(m => m.MovimientoId) : 0;
                var siguienteMovimientoId = ultimoMovimientoId + 1;

                
                var mov_prestamo = _mapper.Map<MovimientoPrestamo>(request);
                mov_prestamo.MovimientoId = siguienteMovimientoId;


                var data = await _repositoryAsyncMovimientoPrestamo.AddAsync(mov_prestamo);
                var dto = _mapper.Map<MovimientoPrestamoDTO>(data);
                return new Response<MovimientoPrestamoDTO>(dto);
            }
        }
    }
}
