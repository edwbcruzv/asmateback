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

namespace Application.Feautres.MiPortal.MovimientosPrestamo.Commands.UpdateMovimientoPrestamo
{
    public class UpdateMovimientoPrestamoCommand : IRequest<Response<MovimientoPrestamoDTO>>
    {
        public int PrestamoId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Capital { get; set; }
        public float FondoGarantia { get; set; }
        public float SaldoActual { get; set; }
        public float Interes { get; set; }
        public float Moratorio { get; set; }


        public class Handler : IRequestHandler<UpdateMovimientoPrestamoCommand, Response<MovimientoPrestamoDTO>>
        {
            private readonly IRepositoryAsync<MovimientoPrestamo> _repositoryAsyncMovimientoPrestamo;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoPrestamo> repositoryAsyncMovimientoPrestamo, IMapper mapper)
            {
                _repositoryAsyncMovimientoPrestamo = repositoryAsyncMovimientoPrestamo;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoPrestamoDTO>> Handle(UpdateMovimientoPrestamoCommand request, CancellationToken cancellationToken)
            {
                var mov_prestamo = await _repositoryAsyncMovimientoPrestamo.GetBySpecAsync(new MovimientoPrestamoByCompanyIdAndEmployeeIdAndPrestamoIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.PrestamoId, request.MovimientoId));

                if (mov_prestamo == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId}, empleado_id {request.EmployeeId}, prestamo_id {request.PrestamoId}, movimiento_id{request.MovimientoId}");
                }
                else
                {
                    try
                    {
                        mov_prestamo.Periodo = request.Periodo;
                        mov_prestamo.Monto = request.Monto;
                        mov_prestamo.Rendimiento = request.Rendimiento;
                        mov_prestamo.EstadoTransaccion = request.EstadoTransaccion;
                        mov_prestamo.Capital = request.Capital;
                        mov_prestamo.FondoGarantia = request.FondoGarantia;
                        mov_prestamo.SaldoActual = request.SaldoActual;
                        mov_prestamo.Interes = request.Interes;
                        mov_prestamo.Moratorio = request.Moratorio;

                        await _repositoryAsyncMovimientoPrestamo.UpdateAsync(mov_prestamo);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar Solicitud." + ex.ToString());
                    }

                    var dto = _mapper.Map<MovimientoPrestamoDTO> (mov_prestamo);
                    return new Response<MovimientoPrestamoDTO>(dto);
                }

            }
        }

    }
}
