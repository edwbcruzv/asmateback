using Application.Interfaces;
using Application.Specifications.MiPortal.Prestamos;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Prestamos.Commands.CreatePrestamoCommand
{
    public class CreatePrestamoCommand : IRequest<Response<int>>
    {
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; } 
        public DateTime? FechaFinal { get; set; } //
        //public int PeriodoInicial { get; set; } //
        //public int? PeriodoFinal { get; set; } //
        //public EstatusOperacion Estatus { get; set; }

        //public DateTime FechaEstatusPendiente { get; set; }
        //public DateTime? FechaEstatusActivo { get; set; }
        //public DateTime? FechaEstatusInactivo { get; set; }
        //public DateTime? FechaEstatusFiniquitado { get; set; }

        public int Plazo { get; set; } //
        public float Monto { get; set; }
        public float MontoPagado { get; set; }
        public float Interes { get; set; }
        public float TazaInteres { get; set; }
        public float PlazoInteres { get; set; }
        public float FondoGarantia { get; set; }
        public float TazaFondoGarantia { get; set; }
        public DateTime? FechaTransferencia { get; set; }
        public float Descuento { get; set; }
        public float Total { get; set; }
        public TipoPrestamo Tipo { get; set; }

        //public string? SrcDocAcuseFirmado { get; set; }
        //public string? SrcDocPagare { get; set; }
        //public string? SrcDocConstanciaRetiro { get; set; }
        //public string? SrcDocSolicitudRetiro { get; set; }

        public class Handler : IRequestHandler<CreatePrestamoCommand, Response<int>>
        {
            private readonly IPrestamoService _prestamoService;
            private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
            private readonly IPeriodosService _periodosService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsyncPrestamo, IMapper mapper, IPeriodosService periodosService, IPrestamoService prestamoService)
            {
                _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
                _mapper = mapper;
                _periodosService = periodosService;
                _prestamoService = prestamoService;
            }

            public async Task<Response<int>> Handle(CreatePrestamoCommand request, CancellationToken cancellationToken)
            {

                List<Prestamo> list = await _repositoryAsyncPrestamo.ListAsync(new PrestamoByEmployeeIdAndIsActivoSpecification(request.EmployeeId));

                if (list.Count != 0)
                {
                    return new Response<int>(-1,"El Usuario tiene un prestamo activo.");
                }


                var prestamo = _mapper.Map<Prestamo>(request);

                prestamo.Tipo = TipoPrestamo.Prestamo;
                prestamo.PeriodoInicial = _periodosService.DateTimeToQuincena(prestamo.FechaInicio);
                prestamo.PeriodoFinal = _periodosService.GetQuincenaFinalByPlazoQuincenal(prestamo.PeriodoInicial, prestamo.Plazo);
                prestamo.FechaEstatusPendiente = DateTime.Now;
                prestamo.Estatus = EstatusOperacion.Pendiente;


                var data = await _repositoryAsyncPrestamo.AddAsync(prestamo);
                await _prestamoService.EnviarCorreoEstatus(data);
                return new Response<int>(data.Id);
            }
        }
    }
}
