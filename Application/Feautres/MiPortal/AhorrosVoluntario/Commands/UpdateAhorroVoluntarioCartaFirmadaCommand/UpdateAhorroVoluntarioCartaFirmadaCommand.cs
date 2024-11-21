using Application.DTOs.MiPortal.Ahorros;
using Application.Interfaces;
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

namespace Application.Feautres.MiPortal.AhorrosVoluntario.Commands.UpdateAhorroVoluntarioCartaFirmadaCommand
{
    public class UpdateAhorroVoluntarioCartaFirmadaCommand: IRequest<Response<AhorroVoluntarioDTO>>
    {
        public int Id { get; set; }
        public IFormFile FileCartaFirmada { get; set; }


         public class Handler : IRequestHandler<UpdateAhorroVoluntarioCartaFirmadaCommand, Response<AhorroVoluntarioDTO>>
        {
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAsyncAhorroVoluntario;
            private readonly IFilesManagerService _filesManagerService;
            private readonly IAhorroVoluntarioService _ahorroVoluntarioService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<AhorroVoluntario> repositoryAsyncAhorroVoluntario, IMapper mapper, IFilesManagerService filesManagerService, IAhorroVoluntarioService ahorroVoluntarioService)
            {
                _repositoryAsyncAhorroVoluntario = repositoryAsyncAhorroVoluntario;
                _mapper = mapper;
                _filesManagerService = filesManagerService;
                _ahorroVoluntarioService = ahorroVoluntarioService;
            }

            public async Task<Response<AhorroVoluntarioDTO>> Handle(UpdateAhorroVoluntarioCartaFirmadaCommand request, CancellationToken cancellationToken)
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
                        if (string.IsNullOrEmpty( ahorro_voluntario.SrcCartaFirmada))
                        {
                            ahorro_voluntario.SrcCartaFirmada = _ahorroVoluntarioService.SaveCartaFirmada(request.FileCartaFirmada, ahorro_voluntario.Id);
                        }
                        else
                        {
                            _filesManagerService.UpdateFile(request.FileCartaFirmada, ahorro_voluntario.SrcCartaFirmada);

                        }

                        await _repositoryAsyncAhorroVoluntario.UpdateAsync(ahorro_voluntario);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }



                    var dto = _mapper.Map<AhorroVoluntarioDTO>(ahorro_voluntario);
                    return new Response<AhorroVoluntarioDTO>(dto, "AhorroVoluntario encontrado con exito.");
                }

            }
        }

    }
}
