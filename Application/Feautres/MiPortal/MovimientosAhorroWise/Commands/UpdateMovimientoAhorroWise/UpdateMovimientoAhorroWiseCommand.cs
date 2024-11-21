using Application.DTOs.MiPortal.Ahorros;
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

namespace Application.Feautres.MiPortal.MovimientosAhorroWise.Commands.UpdateMovimientoAhorroWise
{
    public class UpdateMovimientoAhorroWiseCommand : IRequest<Response<MovimientoAhorroWiseDTO>>
    {
        public int AhorroWiseId { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int MovimientoId { get; set; }

        public int Periodo { get; set; }
        public float Monto { get; set; }
        public float Rendimiento { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }

        public float Interes { get; set; }


        public class Handler : IRequestHandler<UpdateMovimientoAhorroWiseCommand, Response<MovimientoAhorroWiseDTO>>
        {
            private readonly IRepositoryAsync<MovimientoAhorroWise> _repositoryAsyncMovimientoAhorroWise;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<MovimientoAhorroWise> repositoryAsyncMovimientoAhorroWise, IMapper mapper)
            {
                _repositoryAsyncMovimientoAhorroWise = repositoryAsyncMovimientoAhorroWise;
                _mapper = mapper;
            }

            public async Task<Response<MovimientoAhorroWiseDTO>> Handle(UpdateMovimientoAhorroWiseCommand request, CancellationToken cancellationToken)
            {
                var mov_ahorro_wise = await _repositoryAsyncMovimientoAhorroWise.GetBySpecAsync(new MovimientoAhorroWiseByCompanyIdAndEmployeeIdAndAhorroWiseIdAndMovimientoIdSpecification(request.CompanyId, request.EmployeeId, request.AhorroWiseId, request.MovimientoId));

                if (mov_ahorro_wise == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el company_id {request.CompanyId}, empleado_id {request.EmployeeId}, ahorro_id {request.AhorroWiseId}, movimiento_id{request.MovimientoId}");
                }
                else
                {
                    try
                    {
                        mov_ahorro_wise.Periodo = request.Periodo;
                        mov_ahorro_wise.Monto = request.Monto;
                        mov_ahorro_wise.Rendimiento = request.Rendimiento;
                        mov_ahorro_wise.EstadoTransaccion = request.EstadoTransaccion;
                        mov_ahorro_wise.Interes = request.Interes;

                        await _repositoryAsyncMovimientoAhorroWise.UpdateAsync(mov_ahorro_wise);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar Solicitud." + ex.ToString());
                    }

                    var dto = _mapper.Map<MovimientoAhorroWiseDTO>(mov_ahorro_wise);
                    return new Response<MovimientoAhorroWiseDTO>(dto);
                }

            }
        }

    }
}
