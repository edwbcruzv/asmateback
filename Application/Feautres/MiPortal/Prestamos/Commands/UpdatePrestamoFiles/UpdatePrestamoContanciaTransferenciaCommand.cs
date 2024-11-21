using Application.DTOs.MiPortal.Prestamos;
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

namespace Application.Feautres.MiPortal.Prestamos.Commands.UpdatePrestamoFiles
{
    public class UpdatePrestamoContanciaTransferenciaCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public IFormFile FileContanciaTransferencia { get; set; }

         public class Handler : IRequestHandler<UpdatePrestamoContanciaTransferenciaCommand, Response<string>>
        {
            private readonly IRepositoryAsync<Prestamo> _repositoryAsyncPrestamo;
            private readonly IFilesManagerService _filesManagerService;
            private readonly IPrestamoService _prestamoService;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<Prestamo> repositoryAsyncPrestamo, IMapper mapper, IFilesManagerService filesManagerService, IPrestamoService prestamoService)
            {
                _repositoryAsyncPrestamo = repositoryAsyncPrestamo;
                _mapper = mapper;
                _filesManagerService = filesManagerService;
                _prestamoService = prestamoService;
            }

            public async Task<Response<string>> Handle(UpdatePrestamoContanciaTransferenciaCommand request, CancellationToken cancellationToken)
            {
                var prestamo = await _repositoryAsyncPrestamo.GetByIdAsync(request.Id);

                if (prestamo == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {

                    try
                    {
                        if (string.IsNullOrEmpty( prestamo.SrcDocContanciaTransferencia))
                        {
                            prestamo.SrcDocContanciaTransferencia = _prestamoService.SaveConstanciaTransferenciaPDF(request.FileContanciaTransferencia, prestamo.Id);
                            

                        }
                        else
                        {
                            _filesManagerService.UpdateFile(request.FileContanciaTransferencia, prestamo.SrcDocContanciaTransferencia);

                        }

                        await _prestamoService.EnviarCorreosTransferencia(prestamo.Id);

                        await _repositoryAsyncPrestamo.UpdateAsync(prestamo);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }



                    var dto = _mapper.Map<PrestamoDTO>(prestamo);
                    return new Response<string>(dto.SrcDocContanciaTransferencia, null);
                }

            }
        }

    }
}
