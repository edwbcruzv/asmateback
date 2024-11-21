using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feautres.MiPortal.Incidencias.Commands.UpdateIncidenciaCommmand
{
    public class SubirArchivoIncidenciaCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public IFormFile Archivo { get; set; }

        public class Handler : IRequestHandler<SubirArchivoIncidenciaCommand, Response<string>> 
        {

            private readonly IRepositoryAsync<Incidencia> _repositoryAsyncIncidencias;
            private readonly IFilesManagerService _filesManagerService;
            public Handler(IRepositoryAsync<Incidencia> repositoryAsyncIncidencias, IFilesManagerService filesManagerService)
            {
                _repositoryAsyncIncidencias = repositoryAsyncIncidencias;
                _filesManagerService = filesManagerService;
            }

            public async Task<Response<string>> Handle(SubirArchivoIncidenciaCommand request, CancellationToken cancellationToken)
            {
                Incidencia incidencia = await _repositoryAsyncIncidencias.GetByIdAsync(request.Id);
                if (incidencia == null)
                {
                    throw new KeyNotFoundException($"La incidencia con Id {request.Id} no existe");

                }
                else
                {
                    string rutaArchivo = _filesManagerService.saveIncidenciaPDF(request.Archivo,request.Id);

                    incidencia.ArchivoSrc = rutaArchivo;

                    await _repositoryAsyncIncidencias.UpdateAsync(incidencia);

                    Response<string> respuesta = new Response<string>();
                    respuesta.Succeeded = true;
                    respuesta.Message = "El archivo se subió exitosamente";
                    respuesta.Data = rutaArchivo.Split(@"C:\").Last();
                    return respuesta;
                }
            }
        }
    }
}
