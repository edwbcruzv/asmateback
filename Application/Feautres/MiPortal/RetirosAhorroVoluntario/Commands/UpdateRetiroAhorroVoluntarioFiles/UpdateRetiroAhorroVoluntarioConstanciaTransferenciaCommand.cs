using Application.DTOs.MiPortal.Ahorros;
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
    public class UpdateRetiroAhorroVoluntarioConstanciaTransferenciaCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int AhorroVoluntarioId { get; set; }
        public IFormFile FileContanciaTransferencia { get; set; }

        public class Handler : IRequestHandler<UpdateRetiroAhorroVoluntarioConstanciaTransferenciaCommand, Response<string>>
        {
            private readonly IRepositoryAsync<RetiroAhorroVoluntario> _repositoryAsyncRetiroAhorroVoluntario;
            private readonly IRepositoryAsync<AhorroVoluntario> _repositoryAhorroVoluntario;
            private readonly IFilesManagerService _filesManagerService;
            private readonly IRetiroAhorroVoluntarioService _retiroAhorroVoluntarioService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<RetiroAhorroVoluntario> repositoryAsyncRetiroAhorroVoluntario,
                IFilesManagerService filesManagerService,
                IRetiroAhorroVoluntarioService retiroAhorroVoluntarioService,
                IMapper mapper,
                IRepositoryAsync<AhorroVoluntario> repositoryAhorroVoluntario)
            {
                _repositoryAsyncRetiroAhorroVoluntario = repositoryAsyncRetiroAhorroVoluntario;
                _filesManagerService = filesManagerService;
                _retiroAhorroVoluntarioService = retiroAhorroVoluntarioService;
                _mapper = mapper;
                _repositoryAhorroVoluntario = repositoryAhorroVoluntario;
            }

            public async Task<Response<string>> Handle(UpdateRetiroAhorroVoluntarioConstanciaTransferenciaCommand request, CancellationToken cancellationToken)
            {
                RetiroAhorroVoluntario retiro_ahorro_voluntario = await _repositoryAsyncRetiroAhorroVoluntario.GetBySpecAsync(new RetiroAhorroVoluntarioByIdAndAhorroVoluntarioIdSpecification(request.Id, request.AhorroVoluntarioId));
                AhorroVoluntario ahorro_voluntario = await _repositoryAhorroVoluntario.GetByIdAsync(request.AhorroVoluntarioId);

                if (retiro_ahorro_voluntario == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    try
                    {
                        if (string.IsNullOrEmpty(retiro_ahorro_voluntario.SrcDocContanciaTransferencia))
                        {
                            retiro_ahorro_voluntario.SrcDocContanciaTransferencia = _retiroAhorroVoluntarioService.SaveConstanciaTransferenciaPDF(request.FileContanciaTransferencia, retiro_ahorro_voluntario.Id, request.AhorroVoluntarioId);

                        }
                        else
                        {
                            _filesManagerService.UpdateFile(request.FileContanciaTransferencia, retiro_ahorro_voluntario.SrcDocContanciaTransferencia);

                        }

                        //await _retiroAhorroVoluntarioService.EnviarCorreosTransferencia(retiro_ahorro_voluntario, ahorro_voluntario);

                        await _repositoryAsyncRetiroAhorroVoluntario.UpdateAsync(retiro_ahorro_voluntario);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }



                    var dto = _mapper.Map<RetiroAhorroVoluntarioDTO>(retiro_ahorro_voluntario);
                    return new Response<string>(dto.SrcDocContanciaTransferencia, null);
                }
            }
        }
    }
}
