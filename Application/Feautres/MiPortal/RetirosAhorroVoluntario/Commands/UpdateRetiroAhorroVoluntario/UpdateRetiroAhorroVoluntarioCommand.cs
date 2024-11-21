using Application.Interfaces;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.UpdateRetiroAhorroVoluntario
{
    public class UpdateRetiroAhorroVoluntarioCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int AhorroVoluntarioId { get; set; }
        public EstatusRetiro Estatus { get; set; }
        public double Cantidad { get; set; }
        public double Porcentaje { get; set; }
        public bool SeguirAhorrando { get; set; }
        public string? SrcDocSolicitudFirmado { get; set; }


        public class Handler : IRequestHandler<UpdateRetiroAhorroVoluntarioCommand, Response<int>>
        {
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsyncRetiroAhorroVoluntario;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsyncRetiroAhorroVoluntario, IMapper mapper)
            {
                _repositoryAsyncRetiroAhorroVoluntario = repositoryAsyncRetiroAhorroVoluntario;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateRetiroAhorroVoluntarioCommand request, CancellationToken cancellationToken)
            {
                var retiro_ahorro_voluntario = await _repositoryAsyncRetiroAhorroVoluntario.GetBySpecAsync(new RetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdSpecification(request.Id, request.AhorroVoluntarioId));

                if (retiro_ahorro_voluntario == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    try
                    {
                        retiro_ahorro_voluntario.AhorroVoluntarioId = request.AhorroVoluntarioId;
                        retiro_ahorro_voluntario.Estatus = request.Estatus;
                        retiro_ahorro_voluntario.Cantidad = request.Cantidad;
                        retiro_ahorro_voluntario.Porcentaje = request.Porcentaje;
                        retiro_ahorro_voluntario.SrcDocSolicitudFirmado = request.SrcDocSolicitudFirmado;

                        await _repositoryAsyncRetiroAhorroVoluntario.UpdateAsync(retiro_ahorro_voluntario);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }

                    return new Response<int>(retiro_ahorro_voluntario.Id);
                }

            }
        }
    }
}
