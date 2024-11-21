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
    public class UpdatePrestamoPagareCommand: IRequest<Response<string>>
    {
        public int Id { get; set; }
        public IFormFile FilePagare { get; set; }


         public class Handler : IRequestHandler<UpdatePrestamoPagareCommand, Response<string>>
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

            public async Task<Response<string>> Handle(UpdatePrestamoPagareCommand request, CancellationToken cancellationToken)
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
                        if (string.IsNullOrEmpty( prestamo.SrcDocPagare))
                        {
                            prestamo.SrcDocPagare = _prestamoService.SavePagarePDF(request.FilePagare, prestamo.Id);
                        }
                        else
                        {
                            _filesManagerService.UpdateFile(request.FilePagare, prestamo.SrcDocPagare);

                        }

                        await _repositoryAsyncPrestamo.UpdateAsync(prestamo);
                    }
                    catch (Exception ex)
                    {
                        throw new KeyNotFoundException($"Error al actualizar su solicitud." + ex.ToString());
                    }



                    var dto = _mapper.Map<PrestamoDTO>(prestamo);
                    return new Response<string>(dto.SrcDocPagare,null);
                }

            }
        }

    }
}
