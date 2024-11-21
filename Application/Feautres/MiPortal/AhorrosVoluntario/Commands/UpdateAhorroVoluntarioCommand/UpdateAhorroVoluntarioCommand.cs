using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Commands.UpdateAhorroVoluntarioCommand
{
    public class UpdateAhorroVoluntarioCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int PeriodoInicial { get; set; }
        public int PeriodoFinal { get; set; }
        public EstatusOperacion Estatus { get; set; }
        public string SrcCartaFirmada { get; set; }

        public float Rendimiento { get; set; }
        public float Descuento { get; set; }

        public class Handler : IRequestHandler<UpdateAhorroVoluntarioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario, IMapper mapper)
            {
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {
                var ahorro_voluntario = await _repositoryAsyncAhorroVoluntario.GetByIdAsync(request.Id);

                if (ahorro_voluntario == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        ahorro_voluntario.EmployeeId = request.EmployeeId;
                        ahorro_voluntario.CompanyId = request.CompanyId;
                        ahorro_voluntario.FechaInicio = request.FechaInicio;
                        ahorro_voluntario.FechaFinal = request.FechaFinal;
                        ahorro_voluntario.PeriodoInicial = request.PeriodoInicial;
                        ahorro_voluntario.PeriodoFinal = request.PeriodoFinal;
                        ahorro_voluntario.Estatus = request.Estatus;
                        ahorro_voluntario.Rendimiento = request.Rendimiento;
                        ahorro_voluntario.Descuento = request.Descuento;

                        await _repositoryAsyncAhorroVoluntario.UpdateAsync(ahorro_voluntario);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }

                    return new Response<int>(ahorro_voluntario.Id);
                }

            }
        }
    }
}
