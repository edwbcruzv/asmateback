using Application.DTOs.MiPortal.Ahorros;
using Application.DTOs.MiPortal.Prestamos;
using Application.Interfaces;
using Application.Specifications.MiPortal.RetirosAhorroVoluntario;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.RetirosAhorroVoluntario.Commands.UpdateRetiroAhorroVoluntarioFiles
{
    public class UpdateRetiroAhorroVoluntarioSolicitudFirmadoCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int AhorroVoluntarioId { get; set; }
        public IFormFile FileSolicitudFirmado { get; set; }

        public class Handler : IRequestHandler<UpdateRetiroAhorroVoluntarioSolicitudFirmadoCommand, Response<string>>
        {
            public IRepositoryAsync<RetiroAhorroVoluntario> _repositoryRetiroAhorroVoluntario;
            private readonly IFilesManagerService _filesManagerService;
            private readonly IRetiroAhorroVoluntarioService _retiroAhorarioService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<RetiroAhorroVoluntario> repositoryRetiroAhorroVoluntario, IFilesManagerService filesManagerService, IRetiroAhorroVoluntarioService retiroAhorarioService, IMapper mapper)
            {
                _repositoryRetiroAhorroVoluntario = repositoryRetiroAhorroVoluntario;
                _filesManagerService = filesManagerService;
                _retiroAhorarioService = retiroAhorarioService;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateRetiroAhorroVoluntarioSolicitudFirmadoCommand request, CancellationToken cancellationToken)
            {
                var retiro_ahorro = await _repositoryRetiroAhorroVoluntario.GetBySpecAsync(new RetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdSpecification(request.Id, request.AhorroVoluntarioId));

                if (retiro_ahorro == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    try
                    {
                        if (string.IsNullOrEmpty(retiro_ahorro.SrcDocSolicitudFirmado))
                        {
                            retiro_ahorro.SrcDocSolicitudFirmado = _retiroAhorarioService.SaveSolicitudFirmada(request.FileSolicitudFirmado, retiro_ahorro.Id, request.AhorroVoluntarioId);
                        }
                        else
                        {
                            _filesManagerService.UpdateFile(request.FileSolicitudFirmado, retiro_ahorro.SrcDocSolicitudFirmado);

                        }

                        await _repositoryRetiroAhorroVoluntario.UpdateAsync(retiro_ahorro);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }



                    var dto = _mapper.Map<RetiroAhorroVoluntarioDTO>(retiro_ahorro);
                    return new Response<string>(dto.SrcDocSolicitudFirmado, null);
                }
            }
        }

    }
}
