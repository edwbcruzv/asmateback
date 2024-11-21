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

namespace Application.Feautres.MiPortal.MovimientosAhorroVoluntario.Commands.UpdateMovimientoAhorroVoluntario
{
    public class UpdateMovimientoAhorroVoluntarioCommand : IRequest<Response<MovimientoAhorroVoluntarioDTO>>
    {
        public int AhorroVoluntarioId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Interes { get; set; }


        public class Handler : IRequestHandler<UpdateMovimientoAhorroVoluntarioCommand, Response<MovimientoAhorroVoluntarioDTO>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroVoluntario> _repositoryAsyncMovimientoAhorroVoluntario;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroVoluntario> repositoryAsyncMovimientoAhorroVoluntario, IMapper mapper)
            {
                _repositoryAsyncMovimientoAhorroVoluntario = repositoryAsyncMovimientoAhorroVoluntario;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoAhorroVoluntarioDTO>> Handle(UpdateMovimientoAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {
                var mov_ahorro_vol = await _repositoryAsyncMovimientoAhorroVoluntario.GetBySpecAsync(new MovimientoAhorroVoluntarioByCompanyIdAndEmployeeIdAndAhorroVoluntarioIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroVoluntarioId, request.MovimientoId));

                if (mov_ahorro_vol == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId},  empleado_id {request.EmployeeId},  ahorro_id {request.AhorroVoluntarioId}, movimiento_id{request.MovimientoId}");
                }
                else
                {
                    try
                    {
                        mov_ahorro_vol.Periodo = request.Periodo;
                        mov_ahorro_vol.Monto = request.Monto;
                        mov_ahorro_vol.Rendimiento = request.Rendimiento;
                        mov_ahorro_vol.EstadoTransaccion = request.EstadoTransaccion;
                        mov_ahorro_vol.Interes = request.Interes;

                        await _repositoryAsyncMovimientoAhorroVoluntario.UpdateAsync(mov_ahorro_vol);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar Solicitud." + ex.ToString());
                    }

                    var dto = _mapper.Map<MovimientoAhorroVoluntarioDTO>(mov_ahorro_vol);
                    return new Response<MovimientoAhorroVoluntarioDTO>(dto);
                }

            }
        }

    }
}
